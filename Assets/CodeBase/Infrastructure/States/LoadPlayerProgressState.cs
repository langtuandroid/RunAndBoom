using CodeBase.Data;
using CodeBase.Data.Settings;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure.States
{
    public class LoadPlayerProgressState : IPayloadedState<Scene>
    {
        private const Scene InitialLevel = Scene.Level_1;

        private readonly IGameStateMachine _stateMachine;
        private readonly IPlayerProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private Language _language;
        private ILocalizationService _localizationService;

        public LoadPlayerProgressState(IGameStateMachine stateMachine, IPlayerProgressService progressService,
            ISaveLoadService saveLoadService, ILocalizationService localizationService, Language language)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _localizationService = localizationService;
            _language = language;
        }

        public void Enter(Scene scene)
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadSceneState, Scene>(scene);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            PlayerProgress progressServiceProgress = _saveLoadService.LoadProgress();
            _progressService.SetProgress(progressServiceProgress ?? NewProgress());
            
            if (progressServiceProgress != null)
                _localizationService.ChangeLanguage(_language);
        }

        private PlayerProgress NewProgress() =>
            new PlayerProgress(InitialLevel, _language);
    }
}