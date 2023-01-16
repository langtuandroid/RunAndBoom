using CodeBase.Services.Input.Types;
using Zenject;

namespace CodeBase.Services.Input.Platforms
{
    public class EditorInputFactoryInstaller : Installer<EditorInputFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<IPlatformInputService, KeyboardMouseInputType, KeyboardMouseInputType.Factory>();
            Container.BindFactory<IPlatformInputService, TouchScreenInputType, TouchScreenInputType.Factory>();
            Container.BindFactory<IPlatformInputService, SwipeDetection, SwipeDetection.Factory>();

            Container
                .Bind<IPlatformInputService>()
                .To<EditorPlatformInputService>()
                .AsSingle();
        }
    }
}