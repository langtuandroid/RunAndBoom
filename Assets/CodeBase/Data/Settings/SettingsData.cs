﻿using System;
using Plugins.SoundInstance.Core.Static;

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
        public float AimVerticalSensitive = 0.5f;
        public float AimHorizontalSensitive = 0.5f;

        public event Action MusicVolumeChanged;
        public event Action SoundVolumeChanged;
        public event Action MusicSwitchChanged;
        public event Action SoundSwitchChanged;

        public SettingsData(Language language)
        {
            SetMusicVolume(InitialMusicVolume);
            SetSoundVolume(InitialSoundVolume);
            SetMusicSwitch(true);
            SetSoundSwitch(true);
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

            // if (switcher == false)
            // {
            //     SoundInstance.musicVolume = Constants.Zero;
            //     SoundInstance.GetMusicSource().volume = Constants.Zero;
            // }

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