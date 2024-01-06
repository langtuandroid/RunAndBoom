using CodeBase.Data;
using CodeBase.Data.Progress;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.UI.Elements.Hud;
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
            _startNewStandardGameButton.onClick.AddListener(StartNewStandardDifficultyGame);
            _startNewHardGameButton.onClick.AddListener(StartNewAsianDifficultyGame);

            if (Application.isEditor || LeaderBoardService == null || ProgressData == null)
                return;

            ProgressData.AllStats.SaveCurrentLevelStats();
            LeaderBoardService.OnInitializeSuccess += RequestLeaderBoard;
            InitializeLeaderBoard();
        }

        private void OnDisable()
        {
            _startNewStandardGameButton.onClick.RemoveListener(StartNewStandardDifficultyGame);
            _startNewHardGameButton.onClick.RemoveListener(StartNewAsianDifficultyGame);
            LeaderBoardService.OnInitializeSuccess -= RequestLeaderBoard;
        }

        public void Construct(GameObject hero, OpenSettings openSettings) =>
            base.Construct(hero, WindowId.GameEnd, openSettings);

        protected override void RequestLeaderBoard()
        {
            base.RequestLeaderBoard();
            AddLevelResult();
        }

        protected override void SubscribeSetValueSuccess() =>
            LeaderBoardService.OnSetValueSuccess += AddGameResult;

        private void StartNewStandardDifficultyGame()
        {
            PrepareToStartNewGame();
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadGameDataState, bool>(false);
        }

        private void StartNewAsianDifficultyGame()
        {
            PrepareToStartNewGame();
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadGameDataState, bool>(true);
        }

        private void PrepareToStartNewGame()
        {
            SoundInstance.StopRandomMusic();
            WindowService.ClearAll();
            SaveLoadService.ClearProgressData();
        }

        private void AddGameResult()
        {
            int allLevelsScore = ProgressData.AllStats.GetAllLevelsStats();
            Debug.Log($"AddGameResult {allLevelsScore}");
            LeaderBoardService.OnSetValueError += ShowSetValueError;
            LeaderBoardService.OnSetValueSuccess += SuccessSetValue;
            LeaderBoardService.SetValue(SceneId.Initial.GetLeaderBoardName(ProgressData.IsAsianMode),
                allLevelsScore);
        }
    }
}