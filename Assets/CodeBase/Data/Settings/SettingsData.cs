using System;

namespace CodeBase.Data.Settings
{
    [Serializable]
    public class SettingsData
    {
        public float MusicVolume;
        public float SoundVolume;
        public bool MusicOn;
        public bool SoundOn;
        public Language Language;
        public float AimVerticalSensitiveMultiplier = 2f;
        public float AimHorizontalSensitiveMultiplier = 2f;

        public event Action MusicVolumeChanged;
        public event Action SoundVolumeChanged;
        public event Action MusicSwitchChanged;
        public event Action SoundSwitchChanged;
        public event Action AimVerticalSensitiveMultiplierChanged;
        public event Action AimHorizontalSensitiveMultiplierChanged;

        public SettingsData(Language language)
        {
            SetMusicVolume(Constants.InitialMusicVolume);
            SetSoundVolume(Constants.InitialSoundVolume);
            SetMusicSwitch(true);
            SetSoundSwitch(true);
            SetAimVerticalSensitiveMultiplier(Constants.InitialAimSliderValue);
            SetAimHorizontalSensitiveMultiplier(Constants.InitialAimSliderValue);
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

        public void SetAimVerticalSensitiveMultiplier(float value)
        {
            if (AimVerticalSensitiveMultiplier == value)
                return;

            AimVerticalSensitiveMultiplier = value;
            AimVerticalSensitiveMultiplierChanged?.Invoke();
        }

        public void SetAimHorizontalSensitiveMultiplier(float value)
        {
            if (AimHorizontalSensitiveMultiplier == value)
                return;

            AimHorizontalSensitiveMultiplier = value;
            AimHorizontalSensitiveMultiplierChanged?.Invoke();
        }
    }
}