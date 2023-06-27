using System.Collections;
using CodeBase.Hero;
using CodeBase.Services;
using CodeBase.Services.Ads;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Death
{
    public class DeathWindow : WindowBase
    {
        [SerializeField] private Button _recoverForAdsButton;
        [SerializeField] private Button _restartButton;

        private IAdsService _adsService;
        private bool IsRestartButtonEnabled;

        private void Awake()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _restartButton.enabled = false;
#endif
            _adsService = AllServices.Container.Single<IAdsService>();
            StartCoroutine(InitializeYandexSDK());
        }

        private void OnEnable()
        {
            _recoverForAdsButton.onClick.AddListener(ShowAds);
            _restartButton.onClick.AddListener(RestartLevel);
            _adsService.OnInitializeSuccess += EnableRestartButton;
            _adsService.OnClosedFullScreen += RecoverForAds;
            _adsService.OnErrorFullScreen += ShowError;
            _adsService.OnOfflineFullScreen += ShowOffline;
        }

        private void OnDisable()
        {
            _recoverForAdsButton.onClick.RemoveListener(ShowAds);
            _restartButton.onClick.RemoveListener(RestartLevel);
            _adsService.OnInitializeSuccess -= EnableRestartButton;
            _adsService.OnClosedFullScreen -= RecoverForAds;
            _adsService.OnErrorFullScreen -= ShowError;
            _adsService.OnOfflineFullScreen -= ShowOffline;
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.Death);

        private IEnumerator InitializeYandexSDK()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif
            yield return _adsService.Initialize();
        }

        private void EnableRestartButton() =>
            _restartButton.enabled = true;

        private void ShowAds()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            RecoverForAds(true);
            return;
#endif
            _adsService.ShowFullScreenAd();
        }

        private void ShowError(string message) =>
            Debug.Log($"OnErrorFullScreen: {message}");

        private void ShowOffline() =>
            Debug.Log("OnOfflineFullScreen");

        private void RecoverForAds(bool wasShown)
        {
            if (wasShown)
            {
                RecoverHealth();
                Hide();
            }
        }

        private void RecoverHealth() =>
            Hero.GetComponent<HeroHealth>().Recover();

        protected override void PlayOpenSound()
        {
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.Death), transform: transform,
                Volume, AudioSource);
        }
    }
}