using CodeBase.Infrastructure.States;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Settings
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _closeButton;

        private void Start()
        {
            _restartButton.onClick.AddListener(Restart);
            _closeButton.onClick.AddListener(Close);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Close();
        }

        private void Close()
        {
            // SaveLoadService.SaveProgress();
            Hide();
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.Settings);

        private void Restart()
        {
            WindowService.HideAll();
            SoundInstance.StopRandomMusic();
            GameStateMachine.Enter<LoadPlayerProgressState>();
        }
    }
}