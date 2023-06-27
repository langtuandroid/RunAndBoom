using System.Collections;
using CodeBase.Data;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.Services.Ads;
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
        private IAdsService _adsService;

        private void Awake()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _addCoinsButton.enabled = false;
#endif
            _adsService = AllServices.Container.Single<IAdsService>();
            StartCoroutine(InitializeYandexSDK());
        }

        private void Start() =>
            Cursor.lockState = CursorLockMode.Confined;

        private void OnEnable()
        {
            _addCoinsButton.onClick.AddListener(ShowAds);
            _toNextLevelButton.onClick.AddListener(ToNextLevel);
            _adsService.OnInitializeSuccess += EnableAddCoinsButton;
            _adsService.OnRewarded += AddCoins;
            _adsService.OnErrorRewarded += ShowError;
            _adsService.OnClosedRewarded += ShowClosed;
        }

        private void OnDisable()
        {
            _addCoinsButton.onClick.RemoveListener(ShowAds);
            _toNextLevelButton.onClick.RemoveListener(ToNextLevel);
            _adsService.OnInitializeSuccess -= EnableAddCoinsButton;
            _adsService.OnRewarded -= AddCoins;
            _adsService.OnErrorRewarded -= ShowError;
            _adsService.OnClosedRewarded -= ShowClosed;
        }

        private void ShowError(string message) =>
            Debug.Log($"OnErrorRewarded: {message}");

        private void ShowClosed() =>
            Debug.Log("OnClosedRewarded");

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.Gifts);

        private IEnumerator InitializeYandexSDK()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif
            yield return _adsService.Initialize();
        }

        private void EnableAddCoinsButton() =>
            _addCoinsButton.enabled = true;


        public void AddNextScene(Scene nextScene)
        {
            _nextScene = nextScene;
            GenerateItems();
        }

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
            _adsService.ShowRewardedAd();
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