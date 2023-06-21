using System.Collections;
using Agava.YandexGames;
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

        private void Awake()
        {
            _adsService = AllServices.Container.Single<IAdsService>();
            StartCoroutine(InitializeYandexSDK());
        }

        private void OnEnable()
        {
            _recoverForAdsButton.onClick.AddListener(ShowAds);
            _restartButton.onClick.AddListener(RestartLevel);
            _adsService.OnFullScreenClosed += RecoverForAds;
        }

        private void OnDisable()
        {
            _recoverForAdsButton.onClick.RemoveListener(ShowAds);
            _restartButton.onClick.RemoveListener(RestartLevel);
            _adsService.OnFullScreenClosed -= RecoverForAds;
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.Death);

        private IEnumerator InitializeYandexSDK()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif
            yield return YandexGamesSdk.Initialize();
        }

        private void ShowAds()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            RecoverForAds(true);
            return;
#endif
            _adsService.ShowFullScreenAd();
        }

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