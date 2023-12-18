using System.Collections;
using CodeBase.Data;
using CodeBase.Data.Progress;
using CodeBase.Data.Progress.Stats;
using CodeBase.Data.Settings;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.Ads;
using CodeBase.Services.Input;
using CodeBase.Services.LeaderBoard;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Windows;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.UI.Windows.Common
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class WindowBase : MonoBehaviour, IProgressReader
    {
        protected IWindowService WindowService;
        protected ISaveLoadService SaveLoadService;
        protected IGameStateMachine GameStateMachine;
        protected IStaticDataService StaticDataService;
        protected IAdsService AdsService;
        protected ILeaderboardService LeaderBoardService;
        protected AudioSource AudioSource;
        protected GameObject Hero;
        protected float Volume;
        protected ProgressData ProgressData;
        private SettingsData _settingsData;
        private WindowId _windowId;
        protected LevelStats LevelStats;
        protected SceneId CurrentLevel;

        private void Awake() =>
            _settingsData = AllServices.Container.Single<IPlayerProgressService>().SettingsData;

        private void OnEnable()
        {
            if (WindowService == null)
                WindowService = AllServices.Container.Single<IWindowService>();

            if (SaveLoadService == null)
                SaveLoadService = AllServices.Container.Single<ISaveLoadService>();

            if (GameStateMachine == null)
                GameStateMachine = AllServices.Container.Single<IGameStateMachine>();

            if (StaticDataService == null)
                StaticDataService = AllServices.Container.Single<IStaticDataService>();

            if (AdsService == null)
                AdsService = AllServices.Container.Single<IAdsService>();

            if (LeaderBoardService == null)
                LeaderBoardService = AllServices.Container.Single<ILeaderboardService>();

            if (AudioSource == null)
                AudioSource = GetComponent<AudioSource>();

            _settingsData.SoundSwitchChanged += SwitchChanged;
            _settingsData.SoundVolumeChanged += VolumeChanged;
            VolumeChanged();
            SwitchChanged();
        }

        private void OnDisable()
        {
            _settingsData.SoundSwitchChanged -= SwitchChanged;
            _settingsData.SoundVolumeChanged -= VolumeChanged;
        }

        protected void Construct(GameObject hero, WindowId windowId)
        {
            WindowService = AllServices.Container.Single<IWindowService>();
            SaveLoadService = AllServices.Container.Single<ISaveLoadService>();
            GameStateMachine = AllServices.Container.Single<IGameStateMachine>();
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
            AdsService = AllServices.Container.Single<IAdsService>();
            LeaderBoardService = AllServices.Container.Single<ILeaderboardService>();
            AudioSource = GetComponent<AudioSource>();
            Hero = hero;
            _windowId = windowId;
        }

        protected void Hide()
        {
            gameObject.SetActive(false);
            PlayCloseSound();

            if (!WindowService.IsAnotherActive(_windowId))
            {
                if (AllServices.Container.Single<IInputService>() is DesktopInputService)
                    Cursor.lockState = CursorLockMode.Locked;

                Hero.ResumeHero();
            }
        }

        public void Show(bool showCursor = true)
        {
            gameObject.SetActive(true);
            Hero.StopHero();

            if (AllServices.Container.Single<IInputService>() is DesktopInputService)
                ShowCursor(showCursor);

            PlayOpenSound();
        }

        private void ShowCursor(bool showCursor) =>
            Cursor.lockState = showCursor ? CursorLockMode.Confined : CursorLockMode.Locked;

        protected virtual void PlayCloseSound()
        {
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.MenuClose), transform: transform,
                Volume, AudioSource);
        }

        protected virtual void PlayOpenSound()
        {
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.MenuOpen), transform: transform,
                Volume, AudioSource);
        }

        public void LoadProgressData(ProgressData progressData)
        {
            ProgressData = progressData;
            LevelStats = ProgressData.AllStats.CurrentLevelStats;
        }

        private void VolumeChanged() =>
            Volume = _settingsData.SoundVolume;

        private void SwitchChanged() =>
            Volume = _settingsData.SoundOn ? _settingsData.SoundVolume : Constants.Zero;

        protected void RestartLevel()
        {
            WindowService.ClearAll();
            ProgressData.AllStats.RestartedLevel();
            SoundInstance.StopRandomMusic();
            AllServices.Container.Single<IGameStateMachine>()
                .Enter<LoadGameDataState, bool>(ProgressData.IsHardMode);
        }

        protected void InitializeAdsSDK()
        {
            Debug.Log("InitializeAdsSDK");
            if (AdsService.IsInitialized())
                AdsServiceInitializedSuccess();
            else
                StartCoroutine(AdsService.Initialize());
        }

        protected virtual void AdsServiceInitializedSuccess() =>
            AdsService.OnInitializeSuccess -= AdsServiceInitializedSuccess;

        protected void InitializeLeaderBoard()
        {
            if (LeaderBoardService.IsInitialized())
                RequestLeaderBoard();
            else
                StartCoroutine(CoroutineInitializeLeaderBoard());
        }

        protected virtual void RequestLeaderBoard() =>
            LeaderBoardService.OnInitializeSuccess -= RequestLeaderBoard;

        private IEnumerator CoroutineInitializeLeaderBoard()
        {
            yield return LeaderBoardService.Initialize();
        }

        protected void ShowSetValueError(string error)
        {
            Debug.Log($"ShowSetValueError {error}");
            LeaderBoardService.OnSetValueError -= ShowSetValueError;
        }

        protected void AddLevelResult()
        {
            Debug.Log($"AddLevelResult {LevelStats.SceneId} {LevelStats.Score}");
            LeaderBoardService.OnSetValueError += ShowSetValueError;
            SubscribeSetValueSuccess();
            LeaderBoardService.SetValue(CurrentLevel.GetLeaderBoardName(ProgressData.IsHardMode),
                LevelStats.Score);
        }

        protected void SuccessSetValue()
        {
            Debug.Log("SuccessSetValue");
            LeaderBoardService.OnSetValueSuccess -= SuccessSetValue;
        }

        protected virtual void SubscribeSetValueSuccess() =>
            LeaderBoardService.OnSetValueSuccess += SuccessSetValue;
    }
}