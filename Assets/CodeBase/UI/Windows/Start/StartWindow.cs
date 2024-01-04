using CodeBase.Services.Ads;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Elements.Hud;
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
            IAdsService adsService)
        {
            _progressService = progressService;
            _adsService = adsService;

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

            base.Construct(hero, WindowId.Start, openSettings);
        }

        private void EnableStartButton()
        {
            _adsService.OnOfflineInterstitialAd -= EnableStartButton;
            _adsService.OnClosedInterstitialAd -= EnableStartButton;
            _adsService.OnShowInterstitialAdError -= EnableStartButton;
            _startButton.enabled = true;
        }

        private void EnableStartButton(bool obj)
        {
            _adsService.OnOfflineInterstitialAd -= EnableStartButton;
            _adsService.OnClosedInterstitialAd -= EnableStartButton;
            _adsService.OnShowInterstitialAdError -= EnableStartButton;
            _startButton.enabled = true;
        }

        private void EnableStartButton(string obj)
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