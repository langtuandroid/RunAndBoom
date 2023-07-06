using System.Collections;
using CodeBase.Hero;
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

        private void Awake()
        {
            if (Application.isEditor)
                return;

            _restartButton.enabled = false;
            AdsService.OnInitializeSuccess += EnableRestartButton;
            InitializeAdsSDK();
        }

        private void OnEnable()
        {
            _recoverForAdsButton.onClick.AddListener(ShowAds);
            _restartButton.onClick.AddListener(RestartLevel);
            AdsService.OnInitializeSuccess += EnableRestartButton;
            AdsService.OnClosedFullScreen += RecoverForAds;
            AdsService.OnError += ShowError;
            AdsService.OnOfflineFullScreen += ShowOffline;
        }

        private void OnDisable()
        {
            _recoverForAdsButton.onClick.RemoveListener(ShowAds);
            _restartButton.onClick.RemoveListener(RestartLevel);
            AdsService.OnInitializeSuccess -= EnableRestartButton;
            AdsService.OnClosedFullScreen -= RecoverForAds;
            AdsService.OnError -= ShowError;
            AdsService.OnOfflineFullScreen -= ShowOffline;
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.Death);

        private void InitializeAdsSDK()
        {
            if (IsAdsSDKInitialized())
                EnableRestartButton();
            else
                StartCoroutine(CoroutineInitializeAdsSDK());
        }

        private bool IsAdsSDKInitialized() =>
            AdsService.IsInitialized();

        private IEnumerator CoroutineInitializeAdsSDK()
        {
            yield return AdsService.Initialize();
        }

        private void EnableRestartButton() =>
            _restartButton.enabled = true;

        private void ShowAds()
        {
            if (Application.isEditor)
                RecoverForAds(true);
            else
                AdsService.ShowFullScreenAd();
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