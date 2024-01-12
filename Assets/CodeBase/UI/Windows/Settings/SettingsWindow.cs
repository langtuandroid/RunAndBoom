using CodeBase.UI.Elements.Hud;
using CodeBase.UI.Elements.Hud.MobileInputPanel;
using CodeBase.UI.Elements.Hud.MobileInputPanel.Joysticks;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using CodeBase.UI.Windows.Settings.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Settings
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _closeButton;

        private SoundButton _soundButton;
        private MusicButton _musicButton;
        private Transform _heroTransform;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(Restart);
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(Restart);
            _closeButton.onClick.RemoveListener(Close);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Close();
        }

        public void Construct(GameObject hero, OpenSettings openSettings, MobileInput mobileInput,
            MoveJoystick moveJoystick, LookJoystick lookJoystick)
        {
            base.Construct(hero, WindowId.Settings, openSettings, mobileInput, moveJoystick, lookJoystick);

            if (_heroTransform == null)
                _heroTransform = hero.transform;

            if (_soundButton == null)
                _soundButton = gameObject.GetComponentInChildren<SoundButton>();

            _soundButton.Construct(hero.transform);

            if (_musicButton == null)
                _musicButton = gameObject.GetComponentInChildren<MusicButton>();

            _musicButton.Construct(hero.transform);
        }

        private void Restart() =>
            RestartLevel();

        private void Close() =>
            Hide();
    }
}