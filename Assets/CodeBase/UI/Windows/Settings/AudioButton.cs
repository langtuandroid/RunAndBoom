using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Settings
{
    public abstract class AudioButton : MonoBehaviour, IProgressReader
    {
        [SerializeField] private Button _button;
        [SerializeField] protected Image ImageSelected;
        [SerializeField] protected Image ImageUnselected;

        protected PlayerProgress Progress;
        protected bool IsSelected;
        protected float Volume;
        private AudioSource _audioSource;

        private void Awake()
        {
            _button.onClick.AddListener(ButtonPressed);
            _audioSource = GetComponent<AudioSource>();
        }

        private void ButtonPressed()
        {
            ButtonClickAudio();
            SwitchAudio();
            ButtonClickAudio();
            ChangeImage();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Progress = progress;
            Progress.SettingsData.SoundSwitchChanged += SwitchChanged;
            Progress.SettingsData.SoundVolumeChanged += VolumeChanged;
            VolumeChanged();
            SwitchChanged();
            SetSelection();
            ChangeImage();
        }

        private void VolumeChanged() =>
            Volume = Progress.SettingsData.SoundVolume;

        private void SwitchChanged() =>
            Volume = Progress.SettingsData.SoundOn ? Progress.SettingsData.SoundVolume : Constants.Zero;

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
            if (Volume != Constants.Zero)
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.CheckboxClick), transform: transform,
                    Volume, _audioSource);
        }

        protected abstract void SwitchAudio();
        protected abstract void SetSelection();
    }
}