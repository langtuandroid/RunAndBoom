using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.Ads;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class AdListener : MonoBehaviour, IAdListener
    {
        private IAdsService _adsService;
        private GameObject _hero;
        private ILoadingCurtain _loadingCurtain;

        public void Construct(GameObject hero, ILoadingCurtain loadingCurtain)
        {
            _hero = hero;
            _loadingCurtain = loadingCurtain;
        }

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
            _hero.ResumeHero();
            Time.timeScale = Constants.TimeScaleResume;
            TurnOnMusic();
        }

        private void AdClosed(bool isShowed)
        {
            Debug.Log($"InterstitialAd AdClosed {isShowed}");
            _hero.ResumeHero();
            Time.timeScale = Constants.TimeScaleResume;
            TurnOnMusic();
        }

        private void ShowError(string error)
        {
            Debug.Log($"InterstitialAd ShowError {error}");
            _hero.ResumeHero();
            Time.timeScale = Constants.TimeScaleResume;
            TurnOnMusic();
        }

        private void TurnOnMusic() =>
            SoundInstance.StartRandomMusic();
    }
}