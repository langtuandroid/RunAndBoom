using CodeBase.Services;
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

            if (Application.isMobilePlatform)
            {
                _settingsButton.gameObject.SetActive(true);
                _settingsButton.onClick.AddListener(ShowSettingsWindow);
            }
            else
            {
                _settingsButton.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                ShowSettingsWindow();
        }

        private void ShowSettingsWindow() =>
            _windowService.Show<SettingsWindow>(WindowId.Settings);
    }
}