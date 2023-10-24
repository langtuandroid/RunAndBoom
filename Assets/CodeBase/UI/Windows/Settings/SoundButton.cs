namespace CodeBase.UI.Windows.Settings
{
    public class SoundButton : AudioButton
    {
        protected override void SwitchAudio()
        {
            SettingsData.SetSoundSwitch(!IsSelected);
            IsSelected = !IsSelected;
        }

        protected override void SetSelection() =>
            IsSelected = SettingsData.SoundOn;
    }
}