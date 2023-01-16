using CodeBase.Services.Input.Types;
using Zenject;

namespace CodeBase.Services.Input.Platforms
{
    public class MobileInputFactoryInstaller : Installer<MobileInputFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<IPlatformInputService, TouchScreenInputType, TouchScreenInputType.Factory>();

            Container
                .Bind<IPlatformInputService>()
                .To<MobilePlatformInputService>()
                .AsSingle();
        }
    }
}