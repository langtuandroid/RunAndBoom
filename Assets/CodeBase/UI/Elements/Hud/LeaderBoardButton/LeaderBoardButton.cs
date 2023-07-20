using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.LeaderBoard;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud.LeaderBoardButton
{
    public class LeaderBoardButton : MonoBehaviour
    {
        [SerializeField] private TutorialPanel.TutorialPanel _tutorialPanel;
        [SerializeField] private Button _button;

        private IWindowService _windowService;
        private IInputService _inputService;
        private bool _isTutorialVisible;

        private void Awake()
        {
            _windowService = AllServices.Container.Single<IWindowService>();
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void OnEnable()
        {
            _isTutorialVisible = true;
            _button.gameObject.SetActive(_inputService is MobileInputService);

            if (_inputService is MobileInputService)
                _button.onClick.AddListener(ShowLeaderBoardWindow);
        }

        private void OnDisable()
        {
            _button.gameObject.SetActive(false);

            if (_inputService is MobileInputService)
                _button.onClick.RemoveListener(ShowLeaderBoardWindow);
        }

        private void Update()
        {
            if (_inputService is MobileInputService || !Input.GetKeyUp(KeyCode.Tab))
                return;

            ShowLeaderBoardWindow();

            if (!_isTutorialVisible)
                return;

            _tutorialPanel.HidePanel();
            _isTutorialVisible = false;
        }

        private void ShowLeaderBoardWindow() =>
            _windowService.Show<LeaderBoardWindow>(WindowId.LeaderBoard,false);
    }
}