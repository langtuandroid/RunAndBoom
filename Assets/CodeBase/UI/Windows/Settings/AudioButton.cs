using CodeBase.Data.Progress;
using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
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

        protected SettingsData SettingsData;
        protected ISaveLoadService SaveLoadService;
        protected bool IsTurnedOn;
        private float _volume;
        private AudioSource _audioSource;

        private void Awake()
        {
            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (SettingsData == null)
                SettingsData = AllServices.Container.Single<IPlayerProgressService>().SettingsData;

            if (SaveLoadService == null)
                SaveLoadService = AllServices.Container.Single<ISaveLoadService>();
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
            if (IsTurnedOn)
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

        public void LoadProgressData(ProgressData progressData)
        {
            _button.onClick.AddListener(ButtonPressed);

            if (SettingsData == null)
                return;

            SettingsData.SoundSwitchChanged += SwitchChanged;
            SettingsData.SoundVolumeChanged += VolumeChanged;
            VolumeChanged();
            SwitchChanged();
            SetSelection();
            ChangeImage();
        }

        protected abstract void SwitchAudio();

        protected abstract void SetSelection();
    }
}