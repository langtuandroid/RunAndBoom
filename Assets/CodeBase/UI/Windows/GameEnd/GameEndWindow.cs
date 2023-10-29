﻿using CodeBase.Data;
using CodeBase.Data.Progress;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.GameEnd
{
    public class GameEndWindow : WindowBase
    {
        [SerializeField] private Button _startNewStandardGameButton;
        [SerializeField] private Button _startNewHardGameButton;

        private void OnEnable()
        {
            _startNewStandardGameButton.onClick.AddListener(StartNewCommonGame);
            _startNewHardGameButton.onClick.AddListener(StartNewHardModeGame);

            if (Application.isEditor || LeaderBoardService == null || ProgressData == null)
                return;

            ProgressData.AllStats.SaveCurrentLevelStats();
            LeaderBoardService.OnInitializeSuccess += RequestLeaderBoard;
            InitializeLeaderBoard();
        }

        private void OnDisable()
        {
            _startNewStandardGameButton.onClick.RemoveListener(StartNewCommonGame);
            _startNewHardGameButton.onClick.RemoveListener(StartNewHardModeGame);
            LeaderBoardService.OnInitializeSuccess -= RequestLeaderBoard;
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.GameEnd);

        protected override void RequestLeaderBoard()
        {
            base.RequestLeaderBoard();
            AddLevelResult();
        }

        protected override void SubscribeSetValueSuccess() =>
            LeaderBoardService.OnSetValueSuccess += AddGameResult;

        private void StartNewCommonGame()
        {
            PrepareToStartNewGame();
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadGameDataState, bool>(false);
        }

        private void StartNewHardModeGame()
        {
            PrepareToStartNewGame();
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadGameDataState, bool>(true);
        }

        private void PrepareToStartNewGame()
        {
            SoundInstance.StopRandomMusic();
            WindowService.HideAll();
            SaveLoadService.ClearProgressData();
        }

        private void AddGameResult()
        {
            int allLevelsScore = ProgressData.AllStats.GetAllLevelsStats();
            Debug.Log($"AddGameResult {allLevelsScore}");
            LeaderBoardService.OnSetValueError += ShowSetValueError;
            LeaderBoardService.OnSetValueSuccess += SuccessSetValue;
            LeaderBoardService.SetValue(SceneId.Initial.GetLeaderBoardName(ProgressData.IsHardMode),
                allLevelsScore);
        }
    }
}