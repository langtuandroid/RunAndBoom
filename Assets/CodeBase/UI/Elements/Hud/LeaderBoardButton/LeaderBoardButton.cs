using CodeBase.Services;
using CodeBase.Services.Ads;
using CodeBase.Services.Input;
using CodeBase.Services.PlayerAuthorization;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Authorization;
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
        private IAdsService _adsService;
        private IAuthorization _authorization;
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
                _button.onClick.AddListener(CheckAuthorization);

            if (_adsService == null)
                _adsService = AllServices.Container.Single<IAdsService>();

            if (_authorization == null)
                _authorization = AllServices.Container.Single<IAuthorization>();
        }

        private void OnDisable()
        {
            _button.gameObject.SetActive(false);

            if (_inputService is MobileInputService)
                _button.onClick.RemoveListener(CheckAuthorization);
        }

        private void Update()
        {
            if (_inputService is MobileInputService || !Input.GetKeyUp(KeyCode.Tab))
                return;

            CheckAuthorization();

            if (!_isTutorialVisible)
                return;

            _tutorialPanel.HidePanel();
            _isTutorialVisible = false;
        }

        private void CheckAuthorization()
        {
            if (Application.isEditor)
            {
                ToLeaderBoardWindow();
                return;
            }

            if (_adsService.IsInitialized())
                Authorize();
            else
                InitializeAds();
        }

        private void Authorize()
        {
            Debug.Log("Authorize");
            if (_authorization.IsAuthorized())
                ToLeaderBoardWindow();
            else
                ToAuthorizationWindow();
        }

        private void InitializeAds()
        {
            _adsService.OnInitializeSuccess += Authorize;
            StartCoroutine(_adsService.Initialize());
        }

        private void ToAuthorizationWindow()
        {
            Debug.Log("ToAuthorizationWindow");
            _windowService.Show<AuthorizationWindow>(WindowId.Authorization, false);
        }

        private void ToLeaderBoardWindow()
        {
            Debug.Log("ToLeaderBoardWindow");
            _windowService.Show<LeaderBoardWindow>(WindowId.LeaderBoard, false);
        }
    }
}