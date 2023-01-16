using CodeBase.Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(IGameStateMachine gameStateMachine,
            IStaticDataService staticDataService)
        {
            Debug.Log("BootstrapState constructor");
            _staticDataService = staticDataService;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            Debug.Log("BootstrapState Enter");
            InitServices();
            _gameStateMachine.Enter<LoadPlayerProgressState>();
        }

        private async void InitServices()
        {
            _staticDataService.Load();
        }

        public void Exit()
        {
            Debug.Log("BootstrapState Exit");
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, BootstrapState>
        {
        }
    }
}