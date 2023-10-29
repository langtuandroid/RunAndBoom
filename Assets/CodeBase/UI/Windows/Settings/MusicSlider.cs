using Plugins.SoundInstance.Core.Static;

namespace CodeBase.UI.Windows.Settings
{
    public class MusicSlider : AudioSlider
    {
        protected override void ChangeValue(float value)
        {
            if (!IsSwitched)
                SettingsData.SetMusicVolume(value);
            else
                IsSwitched = false;
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
            IsSwitched = false;

            if (SettingsData != null)
                ChangeVolume(SettingsData.MusicVolume);
        }

        protected override void SwitchChanged()
        {
            IsSwitched = true;

            if (SettingsData != null)
                ChangeVolume(SettingsData.MusicOn ? SettingsData.MusicVolume : Constants.Zero);
        }

        protected override void ChangeVolume(float value)
        {
            PreviousVolume = Volume;
            Volume = value;
            SoundInstance.musicVolume = Volume;
            SoundInstance.GetMusicSource().volume = Volume;

            if (Volume == Constants.Zero && PreviousVolume != Constants.Zero)
                SoundInstance.PauseMusic();
            else if (Volume != Constants.Zero && PreviousVolume == Constants.Zero)
                SoundInstance.ResumeMusic();

            Slider.value = Volume;
        }
    }
}