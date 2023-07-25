using CodeBase.Services;
using CodeBase.Services.Ads;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class AdListener : MonoBehaviour, IAdListener
    {
        private IAdsService _adsService;

        public void SubscribeAdsService()
        {
            _adsService = AllServices.Container.Single<IAdsService>();
            _adsService.OnOfflineInterstitialAd += OnOfflineAd;
            _adsService.OnClosedInterstitialAd += AdClosed;
            _adsService.OnShowInterstitialAdError += ShowError;
        }

        private void OnOfflineAd()
        {
            Debug.Log($"InterstitialAd OnOfflineAd");
            TurnOnMusic();
        }

        private void AdClosed(bool isShowed)
        {
            Debug.Log($"InterstitialAd AdClosed {isShowed}");
            TurnOnMusic();
        }

        private void ShowError(string error)
        {
            Debug.Log($"InterstitialAd ShowError {error}");
            TurnOnMusic();
        }

        private void TurnOnMusic() =>
            SoundInstance.StartRandomMusic();
    }
}