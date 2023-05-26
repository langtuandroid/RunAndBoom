using System;
using System.Collections.Generic;
using CodeBase.Data.Settings;
using CodeBase.Infrastructure.Factories;
using CodeBase.Services;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services,
            Language language)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, language),
                [typeof(LoadSceneState)] =
                    new LoadSceneState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>(),
                        services.Single<IEnemyFactory>(), services.Single<IPlayerProgressService>(),
                        services.Single<IStaticDataService>(), services.Single<IUIFactory>(),
                        services.Single<IWindowService>()),
                [typeof(LoadPlayerProgressState)] = new LoadPlayerProgressState(this,
                    services.Single<IPlayerProgressService>(), services.Single<ISaveLoadService>(), services.Single<ILocalizationService>(), language),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}