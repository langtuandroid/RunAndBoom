using UnityEngine;

namespace CodeBase.UI.Windows.Settings.Audio
{
    public class SoundButton : AudioButton
    {
        public new void Construct(Transform heroTransform) =>
            base.Construct(heroTransform);

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