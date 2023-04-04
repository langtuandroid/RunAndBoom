using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure.States
{
    public class LoadPlayerProgressState : IState
    {
        private const string InitialLevel = Scenes.Level1;
        private const string WebInitialScene = "PlaytestingScene";

        private readonly IGameStateMachine _stateMachine;
        private readonly IPlayerProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadPlayerProgressState(IGameStateMachine stateMachine, IPlayerProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            // if (Application.isEditor)
            _stateMachine.Enter<LoadSceneState, string>(InitialLevel);
            // else
            //     _stateMachine.Enter<LoadSceneState, string>(WebInitialScene);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() =>
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress() =>
            new PlayerProgress(InitialLevel);
    }
}