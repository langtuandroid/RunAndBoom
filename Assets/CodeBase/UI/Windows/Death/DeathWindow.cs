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
#if UNITY_WEBGL && !UNITY_EDITOR
            _restartButton.enabled = false;
#endif

            AdsService.OnInitializeSuccess += EnableRestartButton;
            StartCoroutine(InitializeAdsSDK());
        }

        private void OnEnable()
        {
            _recoverForAdsButton.onClick.AddListener(ShowAds);
            _restartButton.onClick.AddListener(RestartLevel);
            AdsService.OnInitializeSuccess += EnableRestartButton;
            AdsService.OnClosedFullScreen += RecoverForAds;
            AdsService.OnErrorFullScreen += ShowError;
            AdsService.OnOfflineFullScreen += ShowOffline;
        }

        private void OnDisable()
        {
            _recoverForAdsButton.onClick.RemoveListener(ShowAds);
            _restartButton.onClick.RemoveListener(RestartLevel);
            AdsService.OnInitializeSuccess -= EnableRestartButton;
            AdsService.OnClosedFullScreen -= RecoverForAds;
            AdsService.OnErrorFullScreen -= ShowError;
            AdsService.OnOfflineFullScreen -= ShowOffline;
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.Death);

        private IEnumerator InitializeAdsSDK()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif
            yield return AdsService.Initialize();
        }

        private void EnableRestartButton() =>
            _restartButton.enabled = true;

        private void ShowAds()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            RecoverForAds(true);
            return;
#endif
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