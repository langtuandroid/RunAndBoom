using CodeBase.Data;
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

        private new void OnEnable()
        {
            base.OnEnable();

            PrepareLevelStats();
            Progress.AllStats.SaveCurrentLevelStats();

            _startNewStandardGameButton.onClick.AddListener(StartNewCommonGame);
            _startNewHardGameButton.onClick.AddListener(StartNewHardModeGame);

            if (Application.isEditor || LeaderBoardService == null)
                return;

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
            AddGameResult();
        }

        private void StartNewCommonGame()
        {
            PrepareToStartNewGame();
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadPlayerProgressState, bool>(false);
        }

        private void StartNewHardModeGame()
        {
            PrepareToStartNewGame();
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadPlayerProgressState, bool>(true);
        }

        private void PrepareToStartNewGame()
        {
            SoundInstance.StopRandomMusic();
            WindowService.HideAll();
            SaveLoadService.ClearProgress();
        }

        private void AddGameResult()
        {
            Debug.Log($"AddGameResult {Progress.AllStats.GetLevelsStats()}");
            LeaderBoardService.OnSetValueError += ShowSetValueError;
            LeaderBoardService.SetValue(Scene.Initial.GetLeaderBoardName(Progress.IsHardMode),
                Progress.AllStats.GetLevelsStats());
        }
    }
}