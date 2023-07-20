using CodeBase.Data;
using CodeBase.Data.Settings;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Levels;

namespace CodeBase.Infrastructure.States
{
    public class LoadPlayerProgressState : IPayloadedState<bool>
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

        public void Enter(bool isHardMode)
        {
            LoadProgressOrInitNew(isHardMode);
            _stateMachine.Enter<LoadSceneState, Scene>(_progressService.Progress.AllStats.CurrentLevelStats.Scene);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew(bool isHardMode)
        {
            _progressServiceProgress = _saveLoadService.LoadProgress();
            _progressService.SetProgress(_progressServiceProgress ?? NewProgress(isHardMode));

            if (_progressServiceProgress != null)
                _localizationService.ChangeLanguage(_language);
        }

        private PlayerProgress NewProgress(bool isHardMode)
        {
            LevelStaticData levelStaticData = _staticDataService.ForLevel(InitialLevel);
            return new PlayerProgress(InitialLevel, _language, levelStaticData.TargetPlayTime,
                levelStaticData.EnemySpawners.Count, isHardMode);
        }
    }
}