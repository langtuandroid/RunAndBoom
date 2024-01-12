using CodeBase.Services.Ads;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Elements.Hud;
using CodeBase.UI.Elements.Hud.MobileInputPanel;
using CodeBase.UI.Elements.Hud.MobileInputPanel.Joysticks;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Start
{
    public class StartWindow : WindowBase
    {
        [SerializeField] private Button _startButton;

        private IPlayerProgressService _progressService;
        private IAdsService _adsService;

        private void OnEnable() =>
            _startButton.onClick.AddListener(Close);

        private void OnDisable() =>
            _startButton.onClick.RemoveListener(Close);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Close();
        }

        public void Construct(GameObject hero, OpenSettings openSettings, IPlayerProgressService progressService,
            IAdsService adsService, MobileInput mobileInput, MoveJoystick moveJoystick, LookJoystick lookJoystick)
        {
            _progressService = progressService;
            _adsService = adsService;
            base.Construct(hero, WindowId.Start, openSettings, mobileInput, moveJoystick, lookJoystick);

            if (Application.isEditor)
                return;

            if (_progressService.ProgressData.WorldData.ShowAdOnLevelStart)
            {
                _startButton.enabled = false;
                _adsService.OnOfflineInterstitialAd += EnableStartButton;
                _adsService.OnClosedInterstitialAd += EnableStartButton;
                _adsService.OnShowInterstitialAdError += EnableStartButton;
            }
            else
            {
                _startButton.enabled = true;
            }
        }

        private void EnableStartButton() =>
            ToWindow();

        private void EnableStartButton(bool obj) =>
            ToWindow();

        private void EnableStartButton(string obj) =>
            ToWindow();

        private void ToWindow()
        {
            _adsService.OnOfflineInterstitialAd -= EnableStartButton;
            _adsService.OnClosedInterstitialAd -= EnableStartButton;
            _adsService.OnShowInterstitialAdError -= EnableStartButton;
            _startButton.enabled = true;
        }

        private void Close() =>
            Hide();
    }
}