using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure.States
{
    public class LoadPlayerProgressState : IPayloadedState<Scene>
    {
        private const Scene InitialLevel = Scene.Level_1;
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

        // public void Enter()
        // {
        //     LoadProgressOrInitNew();
        //     _stateMachine.Enter<LoadSceneState, string>(InitialLevel);
        // }

        public void Enter(Scene scene)
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadSceneState, Scene>(scene);
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