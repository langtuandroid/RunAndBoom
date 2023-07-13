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
        public event Action<string> OnShowFullScreenAdError;
        public event Action<string> OnShowRewardedAdError;

        public bool IsInitialized() =>
            YandexGamesSdk.IsInitialized;

        public IEnumerator Initialize()
        {
            yield return YandexGamesSdk.Initialize(OnInitializeSuccess);
        }

        public void ShowFullScreenAd() =>
            InterstitialAd.Show(onCloseCallback: OnClosedFullScreen, onErrorCallback: OnShowFullScreenAdError,
                onOfflineCallback: OnOfflineFullScreen);

        public void ShowRewardedAd() =>
            VideoAd.Show(onCloseCallback: OnClosedRewarded, onErrorCallback: OnShowRewardedAdError,
                onRewardedCallback: OnRewarded);
    }
}