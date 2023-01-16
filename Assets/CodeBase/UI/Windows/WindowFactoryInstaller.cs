using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Error;
using CodeBase.UI.Windows.Finish;
using CodeBase.UI.Windows.Settings;
using Zenject;

namespace CodeBase.UI.Windows
{
    public class WindowFactoryInstaller : Installer<WindowFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<IWindowService, ErrorWindow, ErrorWindow.Factory>();
            Container.BindFactory<IWindowService, FinishWindow, FinishWindow.Factory>();
            Container.BindFactory<IWindowService, SettingsWindow, SettingsWindow.Factory>();

            Container
                .Bind<IWindowService>()
                .To<WindowService>()
                .AsSingle();
        }
    }
}