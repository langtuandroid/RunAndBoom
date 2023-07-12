using CodeBase.Services;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.LeaderBoard;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud.LeaderBoardButton
{
    public class LeaderBoardButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private IWindowService _windowService;

        private void Awake() =>
            _windowService = AllServices.Container.Single<IWindowService>();

        private void OnEnable() =>
            _button.onClick.AddListener(ShowLeaderBoardWindow);

        private void OnDisable() =>
            _button.onClick.RemoveListener(ShowLeaderBoardWindow);

        private void ShowLeaderBoardWindow() =>
            _windowService.Show<LeaderBoardWindow>(WindowId.LeaderBoard);
    }
}