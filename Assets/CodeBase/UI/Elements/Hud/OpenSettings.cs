using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud
{
    public class OpenSettings : MonoBehaviour
    {
        [SerializeField] private Button _settingsButton;

        private IWindowService _windowService;

        private void Start()
        {
            _windowService = AllServices.Container.Single<IWindowService>();

            if (AllServices.Container.Single<IInputService>() is DesktopInputService)
            {
                _settingsButton.gameObject.SetActive(false);
            }
            else
            {
                _settingsButton.gameObject.SetActive(true);
                _settingsButton.onClick.AddListener(ShowSettingsWindow);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("OpenSettings ShowSettingsWindow");
                ShowSettingsWindow();
            }
        }

        private void ShowSettingsWindow() =>
            _windowService.Show<SettingsWindow>(WindowId.Settings);
    }
}