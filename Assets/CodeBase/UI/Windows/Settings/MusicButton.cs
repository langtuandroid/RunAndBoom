namespace CodeBase.UI.Windows.Settings
{
    public class MusicButton : AudioButton
    {
        protected override void SwitchAudio()
        {
            Progress.SettingsData.SetMusicSwitch(!IsSelected);
            IsSelected = !IsSelected;
        }

        protected override void SetSelection() =>
            IsSelected = Progress.SettingsData.MusicOn;
    }
}