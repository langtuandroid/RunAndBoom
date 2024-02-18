using UnityEngine;

namespace CodeBase.UI.Windows.Settings.Audio
{
    public class SoundButton : AudioButton
    {
        public new void Construct(Transform heroTransform) =>
            base.Construct(heroTransform);

        protected override void SwitchAudio()
        {
            _settingsData.SetSoundSwitch(!_isTurnedOn);
            _isTurnedOn = !_isTurnedOn;
            _saveLoadService.SaveSoundOn(_isTurnedOn);
        }

        protected override void SetSelection() =>
            _isTurnedOn = _settingsData.SoundOn;
    }
}