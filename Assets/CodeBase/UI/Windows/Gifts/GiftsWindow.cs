using CodeBase.Data.Progress;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData.Levels;
using CodeBase.UI.Elements.Hud;
using CodeBase.UI.Elements.Hud.MobileInputPanel;
using CodeBase.UI.Elements.Hud.MobileInputPanel.Joysticks;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
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

        private SceneId _nextScene;

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
            AdsService.OnShowVideoAdError += ShowError;
            AdsService.OnClosedVideoAd += ShowClosed;
            AdsService.OnRewardedAd += AddCoinsAfterAds;
            InitializeAdsSDK();
        }

        private void OnDisable()
        {
            _addCoinsButton.onClick.RemoveListener(ShowAds);

            if (AdsService == null)
                return;

            AdsService.OnInitializeSuccess -= AdsServiceInitializedSuccess;
            AdsService.OnShowVideoAdError -= ShowError;
            AdsService.OnClosedVideoAd -= ShowClosed;
            AdsService.OnRewardedAd -= AddCoinsAfterAds;
        }

        public void Construct(GameObject hero, OpenSettings openSettings, MobileInput mobileInput,
            MoveJoystick moveJoystick, LookJoystick lookJoystick) =>
            base.Construct(hero, WindowId.Gifts, openSettings, mobileInput, moveJoystick, lookJoystick);

        public void AddData(SceneId nextLevel)
        {
            _nextScene = nextLevel;
            _toNextLevelButton.onClick.AddListener(ToNextLevel);
        }

        private void GenerateItems() =>
            _generator.Generate();

        protected override void AdsServiceInitializedSuccess()
        {
            base.AdsServiceInitializedSuccess();
            _addCoinsButton.enabled = true;
        }

        private void ShowAds()
        {
            if (Application.isEditor)
            {
                AddCoins();
                return;
            }

            Debug.Log("ShowAds");
            // OffMusic();
            AdsService.ShowVideoAd();
        }

        private void ShowClosed()
        {
            AdsService.OnClosedVideoAd -= ShowClosed;
            Debug.Log("ShowClosed");
            // OnMusic();
        }

        private void ShowError(string message)
        {
            Debug.Log($"OnErrorFullScreenAd: {message}");
            AdsService.OnShowVideoAdError -= ShowError;
            Debug.Log("ShowError");
            // OnMusic();
        }

        private void AddCoinsAfterAds()
        {
            AdsService.OnRewardedAd -= AddCoinsAfterAds;
            // OnMusic();
            AddCoins();
            Debug.Log("AddCoinsAfterAds");
        }

        private void AddCoins()
        {
            ProgressData.AllStats.AddMoney(_coinsCount);
            _addCoinsButton.enabled = false;
        }

        // protected override void PlayOpenSound()
        // {
        //     SoundInstance.InstantiateOnTransform(
        //         audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.VictoryMusic), transform: Hero.transform,
        //         Volume, AudioSource);
        // }

        private void ToNextLevel()
        {
            LevelStaticData levelStaticData = StaticDataService.ForLevel(_nextScene);
            ProgressData.WorldData.LevelNameData.ChangeLevel(_nextScene.ToString());

            if (ProgressData.IsAsianMode)
                ProgressData.AllStats.StartNewLevel(_nextScene, levelStaticData.TargetPlayTime,
                    levelStaticData.MaxStarsScoreAsian, levelStaticData.EnemySpawners.Count);
            else
                ProgressData.AllStats.StartNewLevel(_nextScene, levelStaticData.TargetPlayTime,
                    levelStaticData.MaxStarsScoreStandard, levelStaticData.EnemySpawners.Count);

            ProgressData.WorldData.ShowAdOnLevelStart = true;
            SaveLoadService.SaveProgressData();
            SaveLoadService.SaveSettingsData();
            WindowService.ClearAll();
            Close();
            GameStateMachine.Enter<LoadSceneState, SceneId>(_nextScene);
        }

        private void Close()
        {
            Hide();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}