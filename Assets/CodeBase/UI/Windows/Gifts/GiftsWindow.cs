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
            _addCoinsButton.enabled = Application.isEditor;

            _addCoinsButton.onClick.AddListener(ShowAds);
            Cursor.lockState = CursorLockMode.Confined;
            GenerateItems();

            if (Application.isEditor)
                return;

            if (AdsService == null)
                return;

            AdsService.OnInitializeSuccess += AdsServiceInitializedSuccess;
            AdsService.OnShowFullScreenAdError += ShowError;
            AdsService.OnClosedFullScreenAd += ShowClosed;
            AdsService.OnOfflineFullScreenAd += ShowOffline;
            InitializeAdsSDK();
        }

        private void OnDisable()
        {
            _addCoinsButton.onClick.RemoveListener(ShowAds);

            if (AdsService == null)
                return;

            AdsService.OnInitializeSuccess -= AdsServiceInitializedSuccess;
            AdsService.OnShowFullScreenAdError -= ShowError;
            AdsService.OnClosedFullScreenAd -= ShowClosed;
            AdsService.OnOfflineFullScreenAd -= ShowOffline;
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

        private void ShowError(string message) =>
            Debug.Log($"OnErrorFullScreenAd: {message}");

        private void ShowClosed(bool closed)
        {
            Debug.Log("OnClosedFullScreenAd");
            if (closed)
                AddCoins();
        }

        private void ShowOffline() =>
            Debug.Log("OnOfflineFullScreen");

        private void ToNextLevel()
        {
            LevelStaticData levelStaticData = StaticDataService.ForLevel(_nextScene);
            Progress.WorldData.LevelNameData.ChangeLevel(_nextScene.ToString());
            Progress.AllStats.StartNewLevel(_nextScene, levelStaticData.TargetPlayTime,
                levelStaticData.EnemySpawners.Count);
            SaveLoadService.SaveProgress();
            WindowService.HideAll();
            Close();
            GameStateMachine.Enter<LoadSceneState, Scene>(_nextScene);
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
                _addCoinsButton.gameObject.SetActive(false);
                return;
            }

            AdsService.ShowFullScreenAd();
        }

        private void AddCoins()
        {
            Progress.AllStats.AddMoney(_coinsCount);
            _addCoinsButton.gameObject.SetActive(false);
        }

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