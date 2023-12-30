﻿using CodeBase.Logic;
using CodeBase.Services.Ads;
using CodeBase.Services.PersistentProgress;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class AdListener : MonoBehaviour, IAdListener
    {
        private IAdsService _adsService;
        private GameObject _hero;
        private IPlayerProgressService _progressService;

        private void Awake() =>
            DontDestroyOnLoad(this);

        public void Construct(GameObject hero, IAdsService adsService, IPlayerProgressService progressService)
        {
            _hero = hero;
            _adsService = adsService;
            _progressService = progressService;

            ResumeGame();

            if (!Application.isEditor)
                InitializeAdsService();
        }

        private void InitializeAdsService()
        {
            Debug.Log("InitializeAdsService");
            _adsService.OnInitializeSuccess += SubscribeAdsEvents;

            if (_adsService.IsInitialized())
            {
                SubscribeAdsEvents();
                ResumeGame();
            }
            else
            {
                StartCoroutine(_adsService.Initialize());
            }
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
            _hero.ResumeHero();
        }

        private void AdClosed(bool isShowed)
        {
            Debug.Log($"InterstitialAd AdClosed {isShowed}");
            ResumeGame();
            _hero.ResumeHero();
        }

        private void ShowError(string error)
        {
            Debug.Log($"InterstitialAd ShowError {error}");
            ResumeGame();
            _hero.ResumeHero();
        }

        private void ResumeGame()
        {
            _progressService.ProgressData.WorldData.ShowAdOnLevelStart = false;
            SoundInstance.StartRandomMusic();
            Time.timeScale = Constants.TimeScaleResume;
        }
    }
}