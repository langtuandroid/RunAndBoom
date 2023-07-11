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

        private void Start()
        {
        }

        private void OnEnable()
        {
            if (!Application.isEditor)
                _restartButton.enabled = false;

            _recoverForAdsButton.onClick.AddListener(ShowAds);
            _restartButton.onClick.AddListener(RestartLevel);

            if (Application.isEditor)
                return;

            if (AdsService == null)
                return;

            AdsService.OnInitializeSuccess += AdsServiceInitializedSuccess;
            AdsService.OnClosedFullScreen += RecoverForAds;
            AdsService.OnShowFullScreenAdError += ShowError;
            AdsService.OnOfflineFullScreen += ShowOffline;
            InitializeAdsSDK();
        }

        private void OnDisable()
        {
            _recoverForAdsButton.onClick.RemoveListener(ShowAds);
            _restartButton.onClick.RemoveListener(RestartLevel);

            if (AdsService == null)
                return;

            AdsService.OnInitializeSuccess -= AdsServiceInitializedSuccess;
            AdsService.OnClosedFullScreen -= RecoverForAds;
            AdsService.OnShowFullScreenAdError -= ShowError;
            AdsService.OnOfflineFullScreen -= ShowOffline;
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.Death);

        protected override void AdsServiceInitializedSuccess() =>
            _restartButton.enabled = true;

        private void ShowAds()
        {
            if (Application.isEditor)
                RecoverForAds(true);
            else
                AdsService.ShowFullScreenAd();
        }

        private void ShowError(string message)
        {
            Debug.Log($"OnErrorFullScreen: {message}");
            RecoverForAds(true);
        }

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