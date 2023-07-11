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

        private void OnEnable()
        {
            if (!Application.isEditor)
                _addCoinsButton.enabled = false;

            _addCoinsButton.onClick.AddListener(ShowAds);
            Cursor.lockState = CursorLockMode.Confined;
            GenerateItems();

            if (Application.isEditor)
                return;

            if (AdsService == null)
                return;

            AdsService.OnInitializeSuccess += AdsServiceInitializedSuccess;
            AdsService.OnRewarded += AddCoins;
            AdsService.OnShowRewardedAdError += ShowError;
            AdsService.OnClosedRewarded += ShowClosed;
            InitializeAdsSDK();
        }

        private void OnDisable()
        {
            _addCoinsButton.onClick.RemoveListener(ShowAds);

            if (AdsService == null)
                return;

            AdsService.OnInitializeSuccess -= AdsServiceInitializedSuccess;
            AdsService.OnRewarded -= AddCoins;
            AdsService.OnShowRewardedAdError -= ShowError;
            AdsService.OnClosedRewarded -= ShowClosed;
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.Gifts);

        public void AddData(Scene nextLevel)
        {
            _nextScene = nextLevel;
            _toNextLevelButton.onClick.AddListener(ToNextLevel);
        }

        protected override void AdsServiceInitializedSuccess() =>
            _addCoinsButton.enabled = true;

        private void ShowError(string message)
        {
            Debug.Log($"OnErrorRewarded: {message}");
            AddCoins();
        }

        private void ShowClosed() =>
            Debug.Log("OnClosedRewarded");

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
            if (Application.isEditor)
            {
                AddCoins();
                return;
            }

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