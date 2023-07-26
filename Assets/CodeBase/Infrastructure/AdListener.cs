using CodeBase.Logic;
using CodeBase.Services.Ads;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class AdListener : MonoBehaviour, IAdListener
    {
        private IAdsService _adsService;
        private GameObject _hero;

        public void Construct(GameObject hero, IAdsService adsService)
        {
            _hero = hero;
            _adsService = adsService;

            if (!Application.isEditor)
                InitializeAdsService();
            else
                ResumeGame();
        }

        private void InitializeAdsService()
        {
            _adsService.OnInitializeSuccess += SubscribeAdsEvents;

            if (_adsService.IsInitialized())
                SubscribeAdsEvents();
            else
                StartCoroutine(_adsService.Initialize());
        }

        private void SubscribeAdsEvents()
        {
            Debug.Log($"SubscribeAdsEvents");
            _adsService.OnInitializeSuccess -= SubscribeAdsEvents;
            _adsService.OnOfflineInterstitialAd += OnOfflineAd;
            _adsService.OnClosedInterstitialAd += AdClosed;
            _adsService.OnShowInterstitialAdError += ShowError;
        }

        private void OnOfflineAd()
        {
            Debug.Log($"InterstitialAd OnOfflineAd");
            ResumeGame();
        }

        private void AdClosed(bool isShowed)
        {
            Debug.Log($"InterstitialAd AdClosed {isShowed}");
            ResumeGame();
        }

        private void ShowError(string error)
        {
            Debug.Log($"InterstitialAd ShowError {error}");
            ResumeGame();
        }

        private void ResumeGame()
        {
            SoundInstance.StartRandomMusic();
            _hero.ResumeHero();
            Time.timeScale = Constants.TimeScaleResume;
        }
    }
}