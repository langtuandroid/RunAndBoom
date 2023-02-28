using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class LoadPlayerProgressState : IState
    {
        private const string InitialLevel = Scenes.Level1;

        private readonly IGameStateMachine _stateMachine;
        private readonly IPlayerProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IEnumerable<IProgressReader> _progressReaderServices;

        [Inject]
        public LoadPlayerProgressState(IGameStateMachine stateMachine, IPlayerProgressService progressService, ISaveLoadService saveLoadService,
            IEnumerable<IProgressReader> progressReaderServices)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _progressReaderServices = progressReaderServices;
        }

        public void Enter()
        {
            var progress = LoadProgressOrInitNew();
            NotifyProgressReaderServices(progress);
            _stateMachine.Enter<LoadSceneState, string>(InitialLevel);
        }

        private void NotifyProgressReaderServices(PlayerProgress progress)
        {
            foreach (var reader in _progressReaderServices)
                reader.LoadProgress(progress);
        }

        public void Exit()
        {
        }

        private PlayerProgress LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
            return _progressService.Progress;
        }

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress();
            return progress;
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, LoadPlayerProgressState>
        {
        }
    }
}