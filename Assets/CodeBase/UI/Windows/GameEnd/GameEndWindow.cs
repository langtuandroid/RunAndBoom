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
        [SerializeField] private Button _restartGameButton;

        private void OnEnable() =>
            _restartGameButton.onClick.AddListener(RestartGame);

        private void OnDisable() =>
            _restartGameButton.onClick.RemoveListener(RestartGame);

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.GameEnd);

        private void RestartGame()
        {
            SoundInstance.StopRandomMusic();
            WindowService.HideAll();
            SaveLoadService.ClearProgress();
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadPlayerProgressState>();
        }
    }
}