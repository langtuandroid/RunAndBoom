namespace CodeBase.UI.Windows.Settings
{
    public class SoundSlider : AudioSlider
    {
        protected override void ChangeValue(float value)
        {
            if (!IsSwitched)
                Progress.SettingsData.SetSoundVolume(value);
            else
                IsSwitched = false;
        }

        protected override void SetListeners()
        {
            Progress.SettingsData.SoundVolumeChanged += VolumeChanged;
            Progress.SettingsData.SoundSwitchChanged += SwitchChanged;
        }

        protected override void VolumeChanged() =>
            ChangeVolume(Progress.SettingsData.SoundVolume);

        protected override void SwitchChanged() =>
            ChangeVolume(Progress.SettingsData.SoundOn ? Progress.SettingsData.SoundVolume : Constants.Zero);

        protected override void ChangeVolume(float value)
        {
            PreviousVolume = Volume;
            Volume = value;
            IsSwitched = true;
            Slider.value = Volume;
        }
    }
}