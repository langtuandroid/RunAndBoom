using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Settings
{
    public abstract class AudioButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] protected Image ImageSelected;
        [SerializeField] protected Image ImageUnselected;

        protected SettingsData SettingsData;
        protected bool IsSelected;
        private float _volume;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            SettingsData = AllServices.Container.Single<IPlayerProgressService>().SettingsData;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ButtonPressed);
            SettingsData.SoundSwitchChanged += SwitchChanged;
            SettingsData.SoundVolumeChanged += VolumeChanged;
            VolumeChanged();
            SwitchChanged();
            SetSelection();
            ChangeImage();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ButtonPressed);
            SettingsData.SoundSwitchChanged -= SwitchChanged;
            SettingsData.SoundVolumeChanged -= VolumeChanged;
        }

        private void ButtonPressed()
        {
            ButtonClickAudio();
            SwitchAudio();
            ChangeImage();
        }

        private void VolumeChanged() =>
            _volume = SettingsData.SoundVolume;

        private void SwitchChanged() =>
            _volume = SettingsData.SoundOn ? SettingsData.SoundVolume : Constants.Zero;

        private void ChangeImage()
        {
            if (IsSelected)
            {
                ImageSelected.ChangeImageAlpha(Constants.Visible);
                ImageUnselected.ChangeImageAlpha(Constants.Invisible);
            }
            else
            {
                ImageUnselected.ChangeImageAlpha(Constants.Visible);
                ImageSelected.ChangeImageAlpha(Constants.Invisible);
            }
        }

        private void ButtonClickAudio()
        {
            if (_volume != Constants.Zero)
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.CheckboxClick), transform: transform,
                    _volume, _audioSource);
        }

        protected abstract void SwitchAudio();
        protected abstract void SetSelection();
    }
}