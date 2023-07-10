using CodeBase.Services;
using CodeBase.UI.Windows.Common;
using UnityEngine;

namespace CodeBase.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        WindowBase? Show<TWindowBase>(WindowId windowId);
        void AddWindow(WindowId windowId, GameObject window);
        bool IsAnotherActive(WindowId windowId);
        void HideAll();
        void HideOthers(WindowId windowId);
    }
}