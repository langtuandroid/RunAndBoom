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
            IsSwitched = false;

            if (SettingsData != null)
                ChangeVolume(SettingsData.SoundVolume);
        }

        protected override void SwitchChanged()
        {
            IsSwitched = true;

            if (SettingsData != null)
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