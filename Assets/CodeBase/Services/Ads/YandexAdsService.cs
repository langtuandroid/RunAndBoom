using System;
using System.Collections;
using Agava.YandexGames;

namespace CodeBase.Services.Ads
{
    public class YandexAdsService : IAdsService
    {
        public event Action OnInitializeSuccess;
        public event Action OnClosedVideoAd;
        public event Action<string> OnShowVideoAdError;
        public event Action OnRewardedAd;
        public event Action<bool> OnClosedFullScreenAd;
        public event Action<string> OnShowFullScreenAdError;
        public event Action OnOfflineFullScreenAd;

        public bool IsInitialized() =>
            YandexGamesSdk.IsInitialized;

        public IEnumerator Initialize()
        {
            yield return YandexGamesSdk.Initialize(OnInitializeSuccess);
        }

        public void ShowVideoAd() =>
            VideoAd.Show(onCloseCallback: OnClosedVideoAd, onErrorCallback: OnShowVideoAdError,
                onRewardedCallback: OnRewardedAd);

        public void ShowFullScreenAd() =>
            InterstitialAd.Show(onCloseCallback: OnClosedFullScreenAd, onErrorCallback: OnShowFullScreenAdError,
                onOfflineCallback: OnOfflineFullScreenAd);
    }
}