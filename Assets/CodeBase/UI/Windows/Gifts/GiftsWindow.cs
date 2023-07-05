using System.Collections;
using CodeBase.Data;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData.Levels;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Gifts
{
    public class GiftsWindow : WindowBase
    {
        [SerializeField] private Button _addCoinsButton;
        [SerializeField] private Button _toNextLevelButton;
        [SerializeField] private ItemsGeneratorBase _generator;
        [SerializeField] private int _coinsCount;

        private Scene _nextScene;

        private void Awake()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _addCoinsButton.enabled = false;
#endif
            GenerateItems();
            AdsService.OnInitializeSuccess += EnableAddCoinsButton;
            InitializeAdsSDK();
        }

        private void Start() =>
            Cursor.lockState = CursorLockMode.Confined;

        private void OnEnable()
        {
            _addCoinsButton.onClick.AddListener(ShowAds);
            _toNextLevelButton.onClick.AddListener(ToNextLevel);
            AdsService.OnInitializeSuccess += EnableAddCoinsButton;
            AdsService.OnRewarded += AddCoins;
            AdsService.OnError += ShowError;
            AdsService.OnClosedRewarded += ShowClosed;
        }

        private void OnDisable()
        {
            _addCoinsButton.onClick.RemoveListener(ShowAds);
            _toNextLevelButton.onClick.RemoveListener(ToNextLevel);
            AdsService.OnInitializeSuccess -= EnableAddCoinsButton;
            AdsService.OnRewarded -= AddCoins;
            AdsService.OnError -= ShowError;
            AdsService.OnClosedRewarded -= ShowClosed;
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.Gifts);

        private void InitializeAdsSDK()
        {
            if (IsAdsSDKInitialized())
                StartCoroutine(CoroutineInitializeAdsSDK());
            else 
                EnableAddCoinsButton();
        }

        private bool IsAdsSDKInitialized() =>
            AdsService.IsInitialized();

        private IEnumerator CoroutineInitializeAdsSDK()
        {
            yield return AdsService.Initialize();
        }

        private void ShowError(string message) =>
            Debug.Log($"OnErrorRewarded: {message}");

        private void ShowClosed() =>
            Debug.Log("OnClosedRewarded");

        private void EnableAddCoinsButton() =>
            _addCoinsButton.enabled = true;

        private void ToNextLevel()
        {
            LevelStaticData levelStaticData = StaticDataService.ForLevel(_nextScene.ToString());
            WindowService.HideAll();
            Progress.Stats.StartNewLevel(_nextScene, levelStaticData.TargetPlayTime,
                levelStaticData.EnemySpawners.Count);
            Progress.WorldData.LevelNameData.ChangeLevel(_nextScene.ToString());
            SaveLoadService.SaveProgress();
            GameStateMachine.Enter<LoadSceneState, Scene>(_nextScene);
            Close();
        }

        private void Close()
        {
            Hide();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void ShowAds()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            AddCoins();
            return;
#endif
            AdsService.ShowRewardedAd();
        }

        private void AddCoins() =>
            Progress.Stats.AllMoney.AddMoney(_coinsCount);

        private void GenerateItems() =>
            _generator.Generate();

        protected override void PlayOpenSound()
        {
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.VictoryMusic), transform: transform,
                Volume, AudioSource);
            SoundInstance.StopRandomMusic(false);
        }
    }
}