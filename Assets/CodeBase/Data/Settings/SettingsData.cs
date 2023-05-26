using System;

namespace CodeBase.Data.Settings
{
    [Serializable]
    public class SettingsData
    {
        private const float DefaultMusicVolume = 0.5f;
        private const float DefaultSoundVolume = 1f;

        public float MusicVolume { get; private set; }
        public float SoundVolume { get; private set; }
        public bool MusicOn { get; private set; }
        public bool SoundOn { get; private set; }
        public Language Language { get; private set; }

        public event Action MusicVolumeChanged;
        public event Action SoundVolumeChanged;
        public event Action MusicSwitchChanged;
        public event Action SoundSwitchChanged;

        public SettingsData(Language language)
        {
            MusicVolume = DefaultMusicVolume;
            SoundVolume = DefaultSoundVolume;
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
            // LanguageChanged?.Invoke();
        }
    }
}