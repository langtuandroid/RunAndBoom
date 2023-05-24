using CodeBase.Services;
using CodeBase.UI.Windows.Common;
using UnityEngine;

namespace CodeBase.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        WindowBase? Open<TWindowBase>(WindowId windowId);
        void AddWindow(WindowId windowId, GameObject window);
        bool IsAnotherActive(WindowId windowId);
        void HideAll();
    }
}