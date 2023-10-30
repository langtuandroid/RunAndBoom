using Plugins.SoundInstance.Core.Static;

namespace CodeBase.UI.Windows.Settings.Audio
{
    public class MusicSlider : AudioSlider
    {
        protected override void ChangeValue(float value)
        {
            if (IsTurnedOn)
            {
                SettingsData.SetMusicVolume(value);
                SaveLoadService.SaveMusicVolume(value);
            }
            else
            {
                IsTurnedOn = true;
            }
        }

        protected override void Subscribe()
        {
            if (SettingsData == null)
                return;

            SettingsData.MusicVolumeChanged += VolumeChanged;
            SettingsData.MusicSwitchChanged += SwitchChanged;
        }

        protected override void Unsubscribe()
        {
            if (SettingsData == null)
                return;

            SettingsData.MusicVolumeChanged -= VolumeChanged;
            SettingsData.MusicSwitchChanged -= SwitchChanged;
        }

        protected override void VolumeChanged()
        {
            IsTurnedOn = true;

            if (SettingsData != null)
                ChangeVolume(SettingsData.MusicVolume);
        }

        protected override void SwitchChanged()
        {
            IsTurnedOn = SettingsData.MusicOn;

            if (SettingsData != null)
                ChangeVolume(SettingsData.MusicOn ? SettingsData.MusicVolume : Constants.Zero);
        }

        protected override void ChangeVolume(float value)
        {
            Volume = value;

            if (SettingsData.MusicOn == false)
            {
                SoundInstance.musicVolume = Constants.Zero;
                SoundInstance.GetMusicSource().volume = Constants.Zero;
                SoundInstance.PauseMusic();
            }
            else
            {
                if (Volume == Constants.Zero)
                {
                    SoundInstance.musicVolume = Constants.Zero;
                    SoundInstance.GetMusicSource().volume = Constants.Zero;
                    SoundInstance.PauseMusic();
                }
                else if (Volume != Constants.Zero)
                {
                    SoundInstance.musicVolume = Volume;
                    SoundInstance.GetMusicSource().volume = Volume;
                    SoundInstance.ResumeMusic();
                }
            }

            Slider.value = Volume;
        }
    }
}