namespace CodeBase.UI.Windows.Settings.Audio
{
    public class MusicButton : AudioButton
    {
        protected override void SwitchAudio()
        {
            SettingsData.SetMusicSwitch(!IsTurnedOn);
            IsTurnedOn = !IsTurnedOn;
            SaveLoadService.SaveMusicOn(IsTurnedOn);
        }

        protected override void SetSelection() =>
            IsTurnedOn = SettingsData.MusicOn;
    }
}