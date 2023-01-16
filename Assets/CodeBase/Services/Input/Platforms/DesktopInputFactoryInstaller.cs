using CodeBase.Services.Input.Types;
using Zenject;

namespace CodeBase.Services.Input.Platforms
{
    public class DesktopInputFactoryInstaller : Installer<DesktopInputFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<IPlatformInputService, KeyboardMouseInputType, KeyboardMouseInputType.Factory>();

            Container
                .Bind<IPlatformInputService>()
                .To<DesktopPlatformInputService>()
                .AsSingle();
        }
    }
}