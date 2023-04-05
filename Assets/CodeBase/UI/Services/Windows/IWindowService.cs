using CodeBase.Services;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        WindowBase? Open<TWindowBase>(WindowId windowId);
        void AddWindow(WindowId windowId, GameObject window);
    }
}