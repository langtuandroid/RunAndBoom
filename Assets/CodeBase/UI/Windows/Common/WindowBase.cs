using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Services.Windows;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.UI.Windows.Common
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class WindowBase : MonoBehaviour, IProgressReader
    {
        protected IWindowService WindowService;
        protected IPlayerProgressService ProgressService;
        protected ISaveLoadService SaveLoadService;
        protected IGameStateMachine GameStateMachine;
        protected AudioSource AudioSource;
        private GameObject _hero;
        private PlayerProgress _progress;
        protected float Volume;
        private WindowId _windowId;

        private void Awake()
        {
            ProgressService = AllServices.Container.Single<IPlayerProgressService>();
            SaveLoadService = AllServices.Container.Single<ISaveLoadService>();
            GameStateMachine = AllServices.Container.Single<IGameStateMachine>();
            AudioSource = GetComponent<AudioSource>();
        }

        protected void Construct(GameObject hero, WindowId windowId)
        {
            WindowService = AllServices.Container.Single<IWindowService>();
            _hero = hero;
            _windowId = windowId;
            Hide();
        }

        protected void Hide()
        {
            gameObject.SetActive(false);
            PlayCloseSound();

            if (!WindowService.IsAnotherActive(_windowId))
            {
                Cursor.lockState = CursorLockMode.Locked;
                _hero.GetComponent<HeroShooting>().TurnOn();
                _hero.GetComponent<HeroMovement>().TurnOn();
                _hero.GetComponent<HeroRotating>().TurnOn();
                _hero.GetComponent<HeroReloading>().TurnOn();
                _hero.GetComponentInChildren<HeroWeaponSelection>().TurnOn();
                Time.timeScale = 1;
            }
        }

        public void Show(bool showCursor)
        {
            gameObject.SetActive(true);
            _hero.GetComponent<HeroShooting>().TurnOff();
            _hero.GetComponent<HeroReloading>().TurnOff();
            _hero.GetComponent<HeroMovement>().TurnOff();
            _hero.GetComponent<HeroRotating>().TurnOff();
            _hero.GetComponentInChildren<HeroWeaponSelection>().TurnOff();
            Time.timeScale = 0;
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
            _progress = progress;
            _progress.SettingsData.SoundSwitchChanged += SwitchChanged;
            _progress.SettingsData.SoundVolumeChanged += VolumeChanged;
            VolumeChanged();
            SwitchChanged();
        }

        private void VolumeChanged() =>
            Volume = _progress.SettingsData.SoundVolume;

        private void SwitchChanged() =>
            Volume = _progress.SettingsData.SoundOn ? _progress.SettingsData.SoundVolume : Constants.Zero;
    }
}