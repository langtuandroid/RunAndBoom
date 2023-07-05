using System;
using System.Collections;
using Agava.YandexGames;

namespace CodeBase.Services.Ads
{
    public class YandexAdsService : IAdsService
    {
        public event Action OnInitializeSuccess;
        public event Action<bool> OnClosedFullScreen;
        public event Action OnOfflineFullScreen;
        public event Action OnClosedRewarded;
        public event Action OnRewarded;
        public event Action<string> OnError;

        public bool IsInitialized() =>
            YandexGamesSdk.IsInitialized;

        public IEnumerator Initialize()
        {
            yield return YandexGamesSdk.Initialize(OnInitializeSuccess);
        }

        public void ShowFullScreenAd() =>
            InterstitialAd.Show(onCloseCallback: OnClosedFullScreen, onErrorCallback: OnError,
                onOfflineCallback: OnOfflineFullScreen);

        public void ShowRewardedAd() =>
            VideoAd.Show(onCloseCallback: OnClosedRewarded, onErrorCallback: OnError,
                onRewardedCallback: OnRewarded);
    }
}