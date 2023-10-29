﻿using CodeBase.Data.Progress;
using CodeBase.Data.Settings;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Levels;
using Plugins.SoundInstance.Core.Static;

namespace CodeBase.Infrastructure.States
{
    public class LoadGameDataState : IPayloadedState<bool>
    {
        private const SceneId InitialLevel = SceneId.Level_1;

        private readonly IGameStateMachine _stateMachine;
        private readonly IPlayerProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticDataService;
        private readonly ILocalizationService _localizationService;
        private ProgressData _progressData;
        private SettingsData _settingsData;
        private Language _language;

        public LoadGameDataState(IGameStateMachine stateMachine, IPlayerProgressService progressService,
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
            _stateMachine.Enter<LoadSceneState, SceneId>(_progressService.ProgressData.AllStats.CurrentLevelStats
                .sceneId);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew(bool isHardMode)
        {
            _settingsData = _saveLoadService.LoadSettingsData();
            _progressData = _saveLoadService.LoadProgressData();

            if (_settingsData == null)
                CreateNewGameData(isHardMode);

            if (_progressData != null)
                _localizationService.ChangeLanguage(_language);
        }

        private void CreateNewGameData(bool isHardMode)
        {
            LevelStaticData levelStaticData = _staticDataService.ForLevel(InitialLevel);
            // _gameData = new GameData(InitialLevel, levelStaticData.TargetPlayTime, levelStaticData.EnemySpawners.Count,
            //     isHardMode, _language);
            _progressData = new ProgressData(InitialLevel, levelStaticData.TargetPlayTime,
                levelStaticData.EnemySpawners.Count,
                isHardMode);
            _settingsData = new SettingsData(_language);
            // _progressService.SetGameData(_gameData);
            _progressService.SetProgressData(_progressData);
            _progressService.SetSettingsData(_settingsData);
        }
    }
}