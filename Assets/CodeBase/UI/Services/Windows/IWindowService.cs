using CodeBase.Services;
using UnityEngine;

namespace CodeBase.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowId windowId);
        void AddWindow(WindowId windowId, GameObject window);
    }
}