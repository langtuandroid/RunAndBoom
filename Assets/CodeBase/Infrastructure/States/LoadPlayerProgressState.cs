using CodeBase.Data;
using CodeBase.Data.Settings;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure.States
{
    public class LoadPlayerProgressState : IState
    {
        private const Scene InitialLevel = Scene.Level_1;

        private readonly IGameStateMachine _stateMachine;
        private readonly IPlayerProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private Language _language;
        private ILocalizationService _localizationService;
        private PlayerProgress _progressServiceProgress;

        public LoadPlayerProgressState(IGameStateMachine stateMachine, IPlayerProgressService progressService,
            ISaveLoadService saveLoadService, ILocalizationService localizationService, Language language)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _localizationService = localizationService;
            _language = language;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadSceneState, Scene>(_progressService.Progress.Stats.CurrentLevelStats.Scene);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressServiceProgress = _saveLoadService.LoadProgress();
            _progressService.SetProgress(_progressServiceProgress ?? NewProgress());

            if (_progressServiceProgress != null)
                _localizationService.ChangeLanguage(_language);
        }

        private PlayerProgress NewProgress() =>
            new PlayerProgress(InitialLevel, _language);
    }
}