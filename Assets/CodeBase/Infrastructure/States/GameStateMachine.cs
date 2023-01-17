using System;
using System.Collections.Generic;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IExitableState> registeredStates;
        private IExitableState currentState;

        [Inject]
        public GameStateMachine(
            BootstrapState.Factory bootstrapStateFactory,
            LoadPlayerProgressState.Factory loadGameSaveStateFactory,
            LoadSceneState.Factory loadLevelStateFactory,
            GameLoopState.Factory gameLoopStateFactory)
        {
            registeredStates = new Dictionary<Type, IExitableState>();

            RegisterState(bootstrapStateFactory.Create(this));
            RegisterState(loadGameSaveStateFactory.Create(this));
            RegisterState(loadLevelStateFactory.Create(this));
            RegisterState(gameLoopStateFactory.Create(this));
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState newState = ChangeState<TState>();
            newState.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState newState = ChangeState<TState>();
            newState.Enter(payload);
        }

        protected void RegisterState<TState>(TState state) where TState : IExitableState =>
            registeredStates.Add(typeof(TState), state);

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            currentState?.Exit();

            TState state = GetState<TState>();
            currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            registeredStates[typeof(TState)] as TState;
    }
}