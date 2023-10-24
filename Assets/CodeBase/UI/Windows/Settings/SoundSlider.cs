namespace CodeBase.UI.Windows.Settings
{
    public class SoundSlider : AudioSlider
    {
        protected override void ChangeValue(float value)
        {
            if (!IsSwitched)
                SettingsData.SetSoundVolume(value);
            else
                IsSwitched = false;
        }

        protected override void Subscribe()
        {
            SettingsData.SoundVolumeChanged += VolumeChanged;
            SettingsData.SoundSwitchChanged += SwitchChanged;
        }

        protected override void Unsubscribe()
        {
            SettingsData.SoundVolumeChanged += VolumeChanged;
            SettingsData.SoundSwitchChanged += SwitchChanged;
        }

        protected override void VolumeChanged()
        {
            IsSwitched = false;
            ChangeVolume(SettingsData.SoundVolume);
        }

        protected override void SwitchChanged()
        {
            IsSwitched = true;
            ChangeVolume(SettingsData.SoundOn ? SettingsData.SoundVolume : Constants.Zero);
        }

        protected override void ChangeVolume(float value)
        {
            PreviousVolume = Volume;
            Volume = value;
            Slider.value = Volume;
        }
    }
}