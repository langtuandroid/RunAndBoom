using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<IGameStateMachine, BootstrapState, BootstrapState.Factory>();
            Container.BindFactory<IGameStateMachine, LoadPlayerProgressState, LoadPlayerProgressState.Factory>();
            Container.BindFactory<IGameStateMachine, LoadSceneState, LoadSceneState.Factory>();
            Container.BindFactory<IGameStateMachine, GameLoopState, GameLoopState.Factory>();

            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

            Debug.Log("GameStateMachineInstaller");
        }
    }
}