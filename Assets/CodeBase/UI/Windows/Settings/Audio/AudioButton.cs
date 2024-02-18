using CodeBase.Data.Progress;
using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.Audio;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Settings.Audio
{
    public abstract class AudioButton : MonoBehaviour, IProgressReader
    {
        [SerializeField] private Button _button;
        [SerializeField] protected Image ImageSelected;
        [SerializeField] protected Image ImageUnselected;

        protected SettingsData _settingsData;
        protected ISaveLoadService _saveLoadService;
        protected IAudioService _audioService;
        protected bool _isTurnedOn;
        private float _volume;
        private AudioSource _audioSource;
        private Transform _heroTransform;

        protected void Construct(Transform heroTransform) =>
            _heroTransform = heroTransform;

        private void Awake()
        {
            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (_settingsData == null)
                _settingsData = AllServices.Container.Single<IPlayerProgressService>().SettingsData;

            if (_saveLoadService == null)
                _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

            if (_audioService == null)
                _audioService = AllServices.Container.Single<IAudioService>();
        }

        private void ButtonPressed()
        {
            ButtonClickAudio();
            SwitchAudio();
            ChangeImage();
        }

        private void VolumeChanged() =>
            _volume = _settingsData.SoundVolume;

        private void SwitchChanged() =>
            _volume = _settingsData.SoundOn ? _settingsData.SoundVolume : Constants.Zero;

        private void ChangeImage()
        {
            if (_isTurnedOn)
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

        private void ButtonClickAudio() =>
            _audioService.LaunchUIActionSound(UIActionSoundId.CheckboxClick, _heroTransform, _audioSource);

        public void LoadProgressData(ProgressData progressData)
        {
            _button.onClick.AddListener(ButtonPressed);

            if (_settingsData == null)
                return;

            _settingsData.SoundSwitchChanged += SwitchChanged;
            _settingsData.SoundVolumeChanged += VolumeChanged;
            VolumeChanged();
            SwitchChanged();
            SetSelection();
            ChangeImage();
        }

        protected abstract void SwitchAudio();

        protected abstract void SetSelection();
    }
}