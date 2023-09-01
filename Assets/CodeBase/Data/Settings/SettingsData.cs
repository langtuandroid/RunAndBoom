using System;

namespace CodeBase.Data.Settings
{
    [Serializable]
    public class SettingsData
    {
        private const float InitialMusicVolume = 0.5f;
        private const float InitialSoundVolume = 0.5f;

        public float MusicVolume;
        public float SoundVolume;
        public bool MusicOn;
        public bool SoundOn;
        public Language Language;

        public event Action MusicVolumeChanged;
        public event Action SoundVolumeChanged;
        public event Action MusicSwitchChanged;
        public event Action SoundSwitchChanged;

        public SettingsData(Language language)
        {
            MusicVolume = InitialMusicVolume;
            SoundVolume = InitialSoundVolume;
            MusicOn = true;
            SoundOn = true;
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
    }
}