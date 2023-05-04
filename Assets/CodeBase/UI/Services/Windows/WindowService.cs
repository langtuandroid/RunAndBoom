﻿using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Common;
using UnityEngine;

namespace CodeBase.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private Dictionary<WindowId, GameObject> _windows;

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
                case WindowId.Finish:
                    window = ShowWindow<FinishWindow>(WindowId.Finish);
                    break;
            }

            return window;
        }

        public void AddWindow(WindowId windowId, GameObject window) => 
            _windows[windowId] = window;

        private T ShowWindow<T>(WindowId windowId) where T : WindowBase
        {
            _windows.TryGetValue(windowId, out GameObject windowGameObject);
            T window = windowGameObject?.GetComponent<T>();
            window?.Show();
            return window;
        }
    }
}