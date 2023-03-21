using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.UI.Elements.ShopPanel;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Settings;
using UnityEngine;

namespace CodeBase.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        protected Dictionary<WindowId, GameObject> _windows;

        public WindowService()
        {
            _windows = new Dictionary<WindowId, GameObject>(DataExtensions.GetValues<WindowId>().Count());
        }

        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.Settings:
                    ShowWindow<SettingsWindow>(WindowId.Settings);
                    break;
                case WindowId.Shop:
                    ShowWindow<ShopWindow>(WindowId.Shop);
                    break;
            }
        }

        public void AddWindow(WindowId windowId, GameObject window) =>
            _windows.Add(windowId, window);

        private void ShowWindow<T>(WindowId windowId) where T : WindowBase
        {
            _windows.TryGetValue(windowId, out GameObject windowGameObject);
            T window = windowGameObject.GetComponent<T>();
            window?.Show();
        }
    }
}