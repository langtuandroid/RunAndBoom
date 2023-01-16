using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Error;
using CodeBase.UI.Windows.Finish;
using CodeBase.UI.Windows.Settings;
using UnityEngine;

namespace CodeBase.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IStaticDataService _staticData;
        private readonly IPlayerProgressService _progressService;
        private readonly IUIFactory _uiFactory;

        public WindowService(IStaticDataService staticData, IPlayerProgressService progressService, IUIFactory uiFactory)
        {
            _staticData = staticData;
            _progressService = progressService;
            _uiFactory = uiFactory;
        }

        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.Settings:
                    CreateWindow<SettingsWindow>(WindowId.Settings);
                    break;
                case WindowId.Finish:
                    CreateWindow<FinishWindow>(WindowId.Finish);
                    break;
                case WindowId.Error:
                    CreateWindow<ErrorWindow>(WindowId.Error);
                    break;
            }
        }

        private void CreateWindow<T>(WindowId windowId) where T : WindowBase
        {
            WindowStaticData windowData = _staticData.ForWindow(windowId);
            GameObject gameObject = Object.Instantiate(windowData.Prefab, _uiFactory.GetUIRoot());
            T window = gameObject.GetComponent<T>();
            window?.Construct(_progressService);
        }
    }
}