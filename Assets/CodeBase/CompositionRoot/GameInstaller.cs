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

            BindEnemyFactory();

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
        }

        private void BindGameBootstrapperFactory()
        {
            Container
                .BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.GameBootstrapper);
        }

        private void BindSaveLoadService()
        {
            Container
                .Bind<ISaveLoadService>()
                .To<SaveLoadService>()
                .AsSingle();
        }

        private void BindPlayerProgressService()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerProgressService>()
                .AsSingle();
        }

        private void BindRandomService()
        {
            Container
                .BindInterfacesAndSelfTo<RandomService>()
                .AsSingle();
        }

        private void BindGameFactory()
        {
            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container
                .Bind<IEnemyFactory>()
                .To<EnemyFactory>()
                .AsSingle();
        }

        private void BindUIFactory()
        {
            Container
                .Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle();
        }

        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.CoroutineRunnerPath)
                .AsSingle();
        }

        private void BindSceneLoader()
        {
            Container
                .BindInterfacesAndSelfTo<SceneLoader>()
                .AsSingle();
        }

        private void BindLoadingCurtain()
        {
            Container
                .BindInterfacesAndSelfTo<LoadingCurtain>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.CurtainPath)
                .AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container
                .Bind<IGameStateMachine>()
                .FromSubContainerResolve()
                .ByInstaller<GameStateMachineInstaller>()
                .AsSingle();
        }

        private void BindAssetsService()
        {
            Container
                .BindInterfacesAndSelfTo<AssetsProvider>()
                .AsSingle();
        }

        private void BindInputTypeServicesService()
        {
            Container
                .Bind<PlayerInput>()
                .AsSingle();

            if (Application.isEditor)
                BindEditorInputFactoryInstaller();
            else if (Application.platform == RuntimePlatform.WindowsPlayer ||
                     Application.platform == RuntimePlatform.OSXPlayer ||
                     Application.platform == RuntimePlatform.LinuxPlayer)
                BindDesktopInputFactoryInstaller();
            else if (Application.platform == RuntimePlatform.Android ||
                     Application.platform == RuntimePlatform.IPhonePlayer)
                BindMobileInputFactoryInstaller();
        }

        private void BindDesktopInputFactoryInstaller()
        {
            Container
                .Bind<IPlatformInputService>()
                .FromSubContainerResolve()
                .ByInstaller<DesktopInputFactoryInstaller>()
                .AsSingle();
        }

        private void BindMobileInputFactoryInstaller()
        {
            Container
                .Bind<IPlatformInputService>()
                .FromSubContainerResolve()
                .ByInstaller<MobileInputFactoryInstaller>()
                .AsSingle();
        }

        private void BindEditorInputFactoryInstaller()
        {
            Container
                .Bind<IPlatformInputService>()
                .FromSubContainerResolve()
                .ByInstaller<EditorInputFactoryInstaller>()
                .AsSingle();
        }

        private void BindRegistratorService()
        {
            Container
                .BindInterfacesAndSelfTo<RegistratorService>()
                .AsSingle();
        }

        private void BindWindowService()
        {
            Container
                .Bind<IWindowService>()
                .FromSubContainerResolve()
                .ByInstaller<WindowFactoryInstaller>()
                .AsSingle();
        }
    }
}