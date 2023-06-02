using CodeBase.Services;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Settings;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class OpenSettingsWindow : MonoBehaviour
    {
        private IWindowService _windowService;

        private void Awake() =>
            _windowService = AllServices.Container.Single<IWindowService>();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                _windowService.Show<SettingsWindow>(WindowId.Settings);
        }
    }
}