using CodeBase.UI.Elements.Hud;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Settings
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _closeButton;

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
            {
                Debug.Log("SettingsWindow Close");
                Close();
            }
        }

        public void Construct(GameObject hero, OpenSettings openSettings) =>
            base.Construct(hero, WindowId.Settings, openSettings);

        private void Restart() =>
            RestartLevel();

        private void Close() =>
            Hide();
    }
}