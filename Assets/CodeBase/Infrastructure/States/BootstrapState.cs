using System;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Data.Settings;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factories;
using CodeBase.Services;
using CodeBase.Services.Ads;
using CodeBase.Services.Constructor;
using CodeBase.Services.Input.Platforms;
using CodeBase.Services.Input.Types;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Pool;
using CodeBase.Services.Randomizer;
using CodeBase.Services.Registrator;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private AllServices _services;
        private Language _language;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services,
            Language language)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _language = language;

            RegisterServices();
            SetTargetFrameRate();
        }

        public void Enter() =>
            _sceneLoader.Load(scene: Scene.Initial, onLoaded: EnterLoadLevel);

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadPlayerProgressState>();

        private void RegisterServices()
        {
            _services.RegisterSingle<ILocalizationService>((new LocalizationService(_language)));
            _services.RegisterSingle<IAdsService>((new YandexAdsService()));
            RegisterStaticData();
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            RegisterAssetsProvider();
            RegisterInputService();
            RegisterPlatformInputService();
            _services.RegisterSingle<IRandomService>(new RandomService());
            _services.RegisterSingle<IPlayerProgressService>(new PlayerProgressService());
            _services.RegisterSingle<IRegistratorService>(new RegistratorService(_services.Single<IAssets>()));
            _services.RegisterSingle<IConstructorService>(new ConstructorService());
            _services.RegisterSingle<IObjectsPoolService>(new ObjectsPoolService(_services.Single<IAssets>(),
                _services.Single<IConstructorService>(), _services.Single<IStaticDataService>()));

            _services.RegisterSingle<IUIFactory>(
                new UIFactory(_services.Single<IAssets>(), _services.Single<IRegistratorService>())
            );

            _services.RegisterSingle<IWindowService>(new WindowService());

            _services.RegisterSingle<IGameFactory>(
                new GameFactory(
                    _services.Single<IAssets>(), _services.Single<IObjectsPoolService>(),
                    _services.Single<IRegistratorService>(), _services.Single<IGameStateMachine>()
                ));

            _services.RegisterSingle<IEnemyFactory>(
                new EnemyFactory(
                    _services.Single<IAssets>(), _services.Single<IStaticDataService>(),
                    _services.Single<IRegistratorService>(), _services.Single<IGameFactory>()
                ));

            _services.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(_services.Single<IGameFactory>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle(staticData);
        }

        private void RegisterAssetsProvider()
        {
            var assetsProvider = new AssetsProvider();
            assetsProvider.Initialize();
            _services.RegisterSingle<IAssets>(assetsProvider);
        }

        public void Exit()
        {
        }

        private void RegisterInputService()
        {
            PlayerInput playerInput = new PlayerInput();
            InputTypesServices inputTypesServices = new InputTypesServices();

            if (Application.isEditor)
            {
                TouchScreenInputType touchScreenInputType = new TouchScreenInputType(playerInput);
                inputTypesServices.AddInputService(touchScreenInputType);
                inputTypesServices.AddInputService(new KeyboardMouseInputType(playerInput));
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                inputTypesServices.AddInputService(new KeyboardMouseInputType(playerInput));
            }
            else
            {
                TouchScreenInputType touchScreenInputType = new TouchScreenInputType(playerInput);
                inputTypesServices.AddInputService(touchScreenInputType);
            }

            _services.RegisterSingle<IInputTypesServices>(inputTypesServices);
        }

        private void RegisterPlatformInputService()
        {
            IInputTypesServices inputTypesServices = _services.Single<IInputTypesServices>();

            if (Application.isEditor)
            {
                RegisterEditorInput(inputTypesServices);
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer ||
                     Application.platform == RuntimePlatform.OSXPlayer ||
                     Application.platform == RuntimePlatform.LinuxPlayer)
            {
                RegisterDesktopInput(inputTypesServices);
            }
            else if (Application.platform == RuntimePlatform.Android ||
                     Application.platform == RuntimePlatform.IPhonePlayer)
            {
                RegisterMobileInput(inputTypesServices);
            }
        }

        private void RegisterEditorInput(IInputTypesServices inputTypesServices)
        {
            KeyboardMouseInputType keyboardMouseInputType = null;
            TouchScreenInputType touchScreenInputType = null;

            List<IInputTypeService> inputServicesList = inputTypesServices.GetInputServices();

            foreach (IInputTypeService inputService in inputServicesList)
            {
                if (inputService is KeyboardMouseInputType)
                    keyboardMouseInputType = inputService as KeyboardMouseInputType;

                if (inputService is TouchScreenInputType)
                    touchScreenInputType = inputService as TouchScreenInputType;
            }

            if (keyboardMouseInputType != null && touchScreenInputType != null)
            {
                DesktopPlatformInputService desktopInputTypeService =
                    new DesktopPlatformInputService(keyboardMouseInputType);
                MobilePlatformInputService mobileInputTypeService =
                    new MobilePlatformInputService(touchScreenInputType);
                _services.RegisterSingle<IPlatformInputService>(new EditorPlatformInputService(desktopInputTypeService,
                    mobileInputTypeService));
            }
            else
            {
                InputServicesException("Input services for EditorInput are not created");
            }
        }

        private void RegisterDesktopInput(IInputTypesServices inputTypesServices)
        {
            KeyboardMouseInputType keyboardMouseInputType = null;

            List<IInputTypeService> inputServicesList = inputTypesServices.GetInputServices();

            foreach (IInputTypeService inputService in inputServicesList)
            {
                if (inputService is KeyboardMouseInputType)
                    keyboardMouseInputType = inputService as KeyboardMouseInputType;
            }

            if (keyboardMouseInputType != null)
                _services.RegisterSingle<IPlatformInputService>(
                    new DesktopPlatformInputService(keyboardMouseInputType));
            else
                InputServicesException("Input services for DesktopInput are not created");
        }

        private void RegisterMobileInput(IInputTypesServices inputTypesServices)
        {
            TouchScreenInputType touchScreenInputType = null;

            List<IInputTypeService> inputServicesList = inputTypesServices.GetInputServices();

            foreach (IInputTypeService inputService in inputServicesList)
            {
                if (inputService is TouchScreenInputType)
                    touchScreenInputType = inputService as TouchScreenInputType;
            }

            if (touchScreenInputType != null)
                _services.RegisterSingle<IPlatformInputService>(new MobilePlatformInputService(touchScreenInputType));
            else
                InputServicesException("Input services for MobileInput are not created");
        }

        private void InputServicesException(string message) =>
            throw new Exception(message);

        private void SetTargetFrameRate()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = Screen.currentResolution.refreshRate;
        }
    }
}