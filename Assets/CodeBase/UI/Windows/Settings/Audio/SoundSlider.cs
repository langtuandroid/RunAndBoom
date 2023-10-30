namespace CodeBase.UI.Windows.Settings.Audio
{
    public class SoundSlider : AudioSlider
    {
        protected override void ChangeValue(float value)
        {
            if (IsTurnedOn)
            {
                SettingsData.SetSoundVolume(value);
                SaveLoadService.SaveSoundVolume(value);
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

            SettingsData.SoundVolumeChanged += VolumeChanged;
            SettingsData.SoundSwitchChanged += SwitchChanged;
        }

        protected override void Unsubscribe()
        {
            if (SettingsData == null)
                return;

            SettingsData.SoundVolumeChanged += VolumeChanged;
            SettingsData.SoundSwitchChanged += SwitchChanged;
        }

        protected override void VolumeChanged()
        {
            IsTurnedOn = true;

            if (SettingsData != null)
                ChangeVolume(SettingsData.SoundVolume);
        }

        protected override void SwitchChanged()
        {
            IsTurnedOn = SettingsData.SoundOn;

            if (SettingsData != null)
                ChangeVolume(SettingsData.SoundOn ? SettingsData.SoundVolume : Constants.Zero);
        }

        protected override void ChangeVolume(float value)
        {
            Volume = value;
            Slider.value = Volume;
        }
    }
}