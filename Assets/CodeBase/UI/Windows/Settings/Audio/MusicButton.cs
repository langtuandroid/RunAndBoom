using UnityEngine;

namespace CodeBase.UI.Windows.Settings.Audio
{
    public class MusicButton : AudioButton
    {
        public new void Construct(Transform heroTransform) =>
            base.Construct(heroTransform);

        protected override void SwitchAudio()
        {
            _settingsData.SetMusicSwitch(!_isTurnedOn);
            _isTurnedOn = !_isTurnedOn;
            _saveLoadService.SaveMusicOn(_isTurnedOn);
        }

        protected override void SetSelection() =>
            _isTurnedOn = _settingsData.MusicOn;
    }
}