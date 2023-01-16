using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services.Windows;
using Zenject;

namespace CodeBase.UI.Windows.Settings
{
    public class SettingsWindow : WindowBase
    {
        [Inject]
        public void Construct(IPlayerProgressService progressService)
        {
            base.Construct(progressService);
        }

        public class Factory : PlaceholderFactory<IWindowService, SettingsWindow>
        {
        }
    }
}