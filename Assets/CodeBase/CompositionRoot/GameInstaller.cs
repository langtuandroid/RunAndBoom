using CodeBase.Infrastructure;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using CodeBase.Services.Input.Platforms;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Randomizer;
using CodeBase.Services.Registrator;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;
using Zenject;

namespace CodeBase.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameBootstrapperFactory();

            BindCoroutineRunner();

            BindSceneLoader();

            BindLoadingCurtain();

            BindGameStateMachine();

            BindStaticDataService();

            BindGameFactory();

            BindUIFactory();

            BindRandomService();

            BindPlayerProgressService();

            BindSaveLoadService();

            BindAssetsService();

            BindInputTypeServicesService();

            BindWindowService();

            BindRegistratorService();
        }

        private void BindStaticDataService()
        {
            Container
                .BindInterfacesAndSelfTo<StaticDataService>()
                .AsSingle();
            Debug.Log("Bind StaticDataService");
        }

        private void BindGameBootstrapperFactory()
        {
            Container
                .BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.GameBootstrapper);
            Debug.Log("Bind GameBootstrapper.Factory");
        }

        private void BindSaveLoadService()
        {
            Container
                .Bind<ISaveLoadService>()
                .To<SaveLoadService>()
                // .BindInterfacesAndSelfTo<SaveLoadService>()
                .AsSingle();
            Debug.Log("Bind SaveLoadService");
        }

        private void BindPlayerProgressService()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerProgressService>()
                .AsSingle();
            Debug.Log("Bind PlayerProgressService");
        }

        private void BindRandomService()
        {
            Container
                .BindInterfacesAndSelfTo<RandomService>()
                .AsSingle();
            Debug.Log("Bind RandomService");
        }

        private void BindGameFactory()
        {
            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();
            Debug.Log("Bind IGameFactory");
        }

        private void BindUIFactory()
        {
            Container
                .Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle();
            Debug.Log("Bind IUIFactory");
        }

        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.CoroutineRunnerPath)
                .AsSingle();
            Debug.Log("Bind ICoroutineRunner");
        }

        private void BindSceneLoader()
        {
            Container
                .BindInterfacesAndSelfTo<SceneLoader>()
                .AsSingle();
            Debug.Log("Bind SceneLoader");
        }

        private void BindLoadingCurtain()
        {
            Container
                .BindInterfacesAndSelfTo<LoadingCurtain>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.CurtainPath)
                .AsSingle();
            Debug.Log("Bind ILoadingCurtain");
        }

        private void BindGameStateMachine()
        {
            Container
                .Bind<IGameStateMachine>()
                .FromSubContainerResolve()
                .ByInstaller<GameStateMachineInstaller>()
                .AsSingle();
            Debug.Log("Bind IGameStateMachine");
        }

        private void BindAssetsService()
        {
            Container
                .BindInterfacesAndSelfTo<AssetsProvider>()
                .AsSingle();
            Debug.Log("Bind AssetsProvider");
        }

        private void BindInputTypeServicesService()
        {
            Container
                .Bind<PlayerInput>()
                .AsSingle();
            Debug.Log("Bind PlayerInput");

            if (Application.isEditor)
            {
                BindEditorInputFactoryInstaller();
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer ||
                     Application.platform == RuntimePlatform.OSXPlayer ||
                     Application.platform == RuntimePlatform.LinuxPlayer)
            {
                BindDesktopInputFactoryInstaller();
            }
            else if (Application.platform == RuntimePlatform.Android ||
                     Application.platform == RuntimePlatform.IPhonePlayer)
            {
                BindMobileInputFactoryInstaller();
            }
        }

        private void BindDesktopInputFactoryInstaller()
        {
            Container
                .Bind<IPlatformInputService>()
                .FromSubContainerResolve()
                .ByInstaller<DesktopInputFactoryInstaller>()
                .AsSingle();
            Debug.Log("Bind DesktopInputFactoryInstaller");
        }

        private void BindMobileInputFactoryInstaller()
        {
            Container
                .Bind<IPlatformInputService>()
                .FromSubContainerResolve()
                .ByInstaller<MobileInputFactoryInstaller>()
                .AsSingle();
            Debug.Log("Bind MobileInputFactoryInstaller");
        }

        private void BindEditorInputFactoryInstaller()
        {
            Container
                .Bind<IPlatformInputService>()
                .FromSubContainerResolve()
                .ByInstaller<EditorInputFactoryInstaller>()
                .AsSingle()
                // .WhenInjectedInto<HeroMovement>()
                ;
            Debug.Log("Bind EditorInputFactoryInstaller");
        }

        private void BindRegistratorService()
        {
            Container
                .BindInterfacesAndSelfTo<RegistratorService>()
                .AsSingle();
            Debug.Log("Bind RegistratorService");
        }

        private void BindWindowService()
        {
            Container
                .Bind<IWindowService>()
                .FromSubContainerResolve()
                .ByInstaller<WindowFactoryInstaller>()
                .AsSingle();
            Debug.Log("Bind WindowService");
        }
    }
}