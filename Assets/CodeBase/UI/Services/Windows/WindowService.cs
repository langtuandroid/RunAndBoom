using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.UI.Elements.ShopPanel;
using CodeBase.UI.Windows;
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

        public WindowBase? Open<TWindowBase>(WindowId windowId)
        {
            WindowBase? window = null;

            switch (windowId)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.Settings:
                    window = ShowWindow<SettingsWindow>(WindowId.Settings);
                    break;
                case WindowId.Shop:
                    window = ShowWindow<ShopWindow>(WindowId.Shop);
                    break;
                case WindowId.Death:
                    window = ShowWindow<DeathWindow>(WindowId.Death);
                    break;
            }

            return window;
        }

        public void AddWindow(WindowId windowId, GameObject window)
        {
            if (!_windows.ContainsKey(windowId))
                _windows.Add(windowId, window);
        }

        private T ShowWindow<T>(WindowId windowId) where T : WindowBase
        {
            _windows.TryGetValue(windowId, out GameObject windowGameObject);
            T window = windowGameObject.GetComponent<T>();
            window?.Show();
            return window;
        }
    }
}