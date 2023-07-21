using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.Infrastructure.States;
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
        protected PlayerProgress Progress;
        protected float Volume;
        private WindowId _windowId;

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
            Hide();
        }

        protected void Hide()
        {
            gameObject.SetActive(false);
            PlayCloseSound();

            if (!WindowService.IsAnotherActive(_windowId))
            {
                if (AllServices.Container.Single<IInputService>() is DesktopInputService)
                    Cursor.lockState = CursorLockMode.Locked;

                Hero.GetComponent<HeroShooting>().TurnOn();
                Hero.GetComponent<HeroMovement>().TurnOn();
                Hero.GetComponent<HeroRotating>().TurnOn();
                Hero.GetComponent<HeroReloading>().TurnOn();
                Hero.GetComponentInChildren<HeroWeaponSelection>().TurnOn();
                Hero.GetComponentInChildren<PlayTimer>().enabled = true;
                Time.timeScale = 1;
            }
        }

        public void Show(bool showCursor = true)
        {
            gameObject.SetActive(true);
            Hero.GetComponent<HeroShooting>().TurnOff();
            Hero.GetComponent<HeroReloading>().TurnOff();
            Hero.GetComponent<HeroMovement>().TurnOff();
            Hero.GetComponent<HeroRotating>().TurnOff();
            Hero.GetComponentInChildren<HeroWeaponSelection>().TurnOff();
            Hero.GetComponentInChildren<PlayTimer>().enabled = false;
            Time.timeScale = 0;

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

        public void LoadProgress(PlayerProgress progress)
        {
            Progress = progress;
            Progress.SettingsData.SoundSwitchChanged += SwitchChanged;
            Progress.SettingsData.SoundVolumeChanged += VolumeChanged;
            VolumeChanged();
            SwitchChanged();
        }

        private void VolumeChanged() =>
            Volume = Progress.SettingsData.SoundVolume;

        private void SwitchChanged() =>
            Volume = Progress.SettingsData.SoundOn ? Progress.SettingsData.SoundVolume : Constants.Zero;

        protected void RestartLevel()
        {
            WindowService.HideAll();
            Progress.AllStats.RestartedLevel();
            SoundInstance.StopRandomMusic();
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadPlayerProgressState, bool>(Progress.IsHardMode);
        }

        protected void InitializeAdsSDK()
        {
            Debug.Log("InitializeAdsSDK");
            if (AdsService.IsInitialized())
                AdsServiceInitializedSuccess();
            else
                StartCoroutine(AdsService.Initialize());
        }

        protected virtual void AdsServiceInitializedSuccess()
        {
        }
    }
}