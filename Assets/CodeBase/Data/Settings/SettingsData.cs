using System;

namespace CodeBase.Data.Settings
{
    [Serializable]
    public class SettingsData
    {
        private const float DefaultMusicVolume = 0.5f;
        private const float DefaultSoundVolume = 1f;

        public float MusicVolume;
        public float SoundVolume;
        public bool MusicOn;
        public bool SoundOn;
        public Language Language;
        public bool ShowTraining;

        public event Action MusicVolumeChanged;
        public event Action SoundVolumeChanged;
        public event Action MusicSwitchChanged;
        public event Action SoundSwitchChanged;
        public event Action ShowTrainingSwitchChanged;

        public SettingsData(Language language)
        {
            MusicVolume = DefaultMusicVolume;
            SoundVolume = DefaultSoundVolume;
            MusicOn = true;
            SoundOn = true;
            ShowTraining = true;
            SetLanguage(language);
        }

        public void SetMusicVolume(float volume)
        {
            if (MusicVolume == volume)
                return;

            MusicVolume = volume;
            MusicVolumeChanged?.Invoke();
        }

        public void SetSoundVolume(float volume)
        {
            if (SoundVolume == volume)
                return;

            SoundVolume = volume;
            SoundVolumeChanged?.Invoke();
        }

        public void SetMusicSwitch(bool switcher)
        {
            if (MusicOn == switcher)
                return;

            MusicOn = switcher;
            MusicSwitchChanged?.Invoke();
        }

        public void SetSoundSwitch(bool switcher)
        {
            if (SoundOn == switcher)
                return;

            SoundOn = switcher;
            SoundSwitchChanged?.Invoke();
        }

        public void SetLanguage(Language language)
        {
            if (Language == language)
                return;

            Language = language;
        }

        public void ChangeTrainingSwitch()
        {
            ShowTraining = !ShowTraining;
            ShowTrainingSwitchChanged?.Invoke();
        }
    }
}