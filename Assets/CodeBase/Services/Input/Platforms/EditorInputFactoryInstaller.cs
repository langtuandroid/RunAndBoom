using CodeBase.Services.Input.Types;
using Zenject;

namespace CodeBase.Services.Input.Platforms
{
    public class EditorInputFactoryInstaller : Installer<EditorInputFactoryInstaller>
    {
        public override void InstallBindings()
        {
            PlayerInput playerInput = new PlayerInput();

            Container
                .Bind<IPlatformInputService>()
                .To<EditorPlatformInputService>()
                .FromInstance(new EditorPlatformInputService(new DesktopPlatformInputService(new KeyboardMouseInputType(playerInput)),
                    new MobilePlatformInputService(new TouchScreenInputType(playerInput))))
                .AsSingle();
        }
    }
}