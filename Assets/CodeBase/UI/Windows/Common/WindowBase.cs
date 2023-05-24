using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.Services.Audio;
using CodeBase.Services.PersistentProgress;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.UI.Windows.Common
{
    [RequireComponent(typeof(AudioSource))]
    public class WindowBase : MonoBehaviour, IProgressReader
    {
        private AudioSource _audioSource;

        private GameObject _hero;
        private PlayerProgress _progress;
        private float _volume;

        private void Awake() =>
            _audioSource = GetComponent<AudioSource>();

        protected void Construct(GameObject hero)
        {
            _hero = hero;
            Hide();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _hero.GetComponent<HeroShooting>().TurnOn();
            _hero.GetComponent<HeroMovement>().TurnOn();
            _hero.GetComponent<HeroRotating>().TurnOn();
            _hero.GetComponent<HeroReloading>().TurnOn();
            _hero.GetComponentInChildren<HeroWeaponSelection>().TurnOn();
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.MenuClose), transform: transform,
                _volume, _audioSource);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _hero.GetComponent<HeroShooting>().TurnOff();
            _hero.GetComponent<HeroReloading>().TurnOff();
            _hero.GetComponent<HeroMovement>().TurnOff();
            _hero.GetComponent<HeroRotating>().TurnOff();
            _hero.GetComponentInChildren<HeroWeaponSelection>().TurnOff();
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.MenuOpen), transform: transform,
                _volume, _audioSource);
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
            _volume = _progress.SettingsData.SoundVolume;

        private void SwitchChanged() =>
            _volume = _progress.SettingsData.SoundOn ? _progress.SettingsData.SoundVolume : Constants.Zero;
    }
}