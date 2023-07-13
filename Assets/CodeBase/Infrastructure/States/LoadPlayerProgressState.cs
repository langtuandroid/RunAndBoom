using CodeBase.Data;
using CodeBase.Data.Settings;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Levels;

namespace CodeBase.Infrastructure.States
{
    public class LoadPlayerProgressState : IState
    {
        private const Scene InitialLevel = Scene.Level_1;

        private readonly IGameStateMachine _stateMachine;
        private readonly IPlayerProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticDataService;
        private readonly ILocalizationService _localizationService;
        private PlayerProgress _progressServiceProgress;
        private Language _language;

        public LoadPlayerProgressState(IGameStateMachine stateMachine, IPlayerProgressService progressService,
            ISaveLoadService saveLoadService, IStaticDataService staticDataService,
            ILocalizationService localizationService, Language language)
        {
            _staticDataService = staticDataService;
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

        private PlayerProgress NewProgress()
        {
            LevelStaticData levelStaticData = _staticDataService.ForLevel(InitialLevel);
            return new PlayerProgress(InitialLevel, _language, levelStaticData.TargetPlayTime,
                levelStaticData.EnemySpawners.Count);
        }
    }
}