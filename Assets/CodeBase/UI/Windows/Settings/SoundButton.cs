namespace CodeBase.UI.Windows.Settings
{
    public class SoundButton : AudioButton
    {
        protected override void SwitchAudio()
        {
            SettingsData.SetSoundSwitch(!IsTurnedOn);
            IsTurnedOn = !IsTurnedOn;
            SaveLoadService.SaveSoundOn(IsTurnedOn);
        }

        protected override void SetSelection() =>
            IsTurnedOn = SettingsData.SoundOn;
    }
}