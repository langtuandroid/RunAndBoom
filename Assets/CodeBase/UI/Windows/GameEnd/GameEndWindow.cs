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
        }

        private void OnDisable()
        {
            _startNewStandardGameButton.onClick.RemoveListener(StartNewCommonGame);
            _startNewHardGameButton.onClick.RemoveListener(StartNewHardModeGame);
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.GameEnd);

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
    }
}