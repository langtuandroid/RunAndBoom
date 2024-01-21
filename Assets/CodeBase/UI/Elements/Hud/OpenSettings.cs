using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud
{
    public class OpenSettings : MonoBehaviour
    {
        [SerializeField] private Button _settingsButton;

        private IWindowService _windowService;
        private IInputService _inputService;
        private PlayerInput _playerInput;

        private void Awake() =>
            _playerInput = new PlayerInput();

        private void Start()
        {
            _windowService = AllServices.Container.Single<IWindowService>();
            _inputService = AllServices.Container.Single<IInputService>();
            _settingsButton.gameObject.SetActive(_inputService is not DesktopInputService);
        }

        private void ShowSettingsWindow(InputAction.CallbackContext callbackContext)
        {
            enabled = false;
            _windowService.Show<SettingsWindow>(WindowId.Settings);
        }

        public void On()
        {
            _playerInput.Player.ESC.performed += ShowSettingsWindow;
            _playerInput.Enable();
        }

        public void Off()
        {
            _playerInput.Player.ESC.performed -= ShowSettingsWindow;
            _playerInput.Disable();
        }
    }
}