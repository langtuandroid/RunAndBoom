namespace CodeBase.UI.Windows.Settings
{
    public class SoundButton : AudioButton
    {
        protected override void SwitchAudio()
        {
            Progress.SettingsData.SetSoundSwitch(!IsSelected);
            IsSelected = !IsSelected;
        }

        protected override void SetSelection() =>
            IsSelected = Progress.SettingsData.SoundOn;
    }
}