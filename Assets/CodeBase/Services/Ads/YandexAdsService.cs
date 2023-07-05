using System;
using System.Collections;
using Agava.YandexGames;

namespace CodeBase.Services.Ads
{
    public class YandexAdsService : IAdsService
    {
        public event Action OnInitializeSuccess;
        public event Action<bool> OnClosedFullScreen;
        public event Action<string> OnErrorFullScreen;
        public event Action OnOfflineFullScreen;
        public event Action OnClosedRewarded;
        public event Action<string> OnErrorRewarded;
        public event Action OnRewarded;

        public IEnumerator Initialize()
        {
            if (!IsInitialized())
                yield return YandexGamesSdk.Initialize(OnInitializeSuccess);
        }

        private bool IsInitialized() =>
            YandexGamesSdk.IsInitialized;

        public void ShowFullScreenAd() =>
            InterstitialAd.Show(onCloseCallback: OnClosedFullScreen, onErrorCallback: OnErrorFullScreen,
                onOfflineCallback: OnOfflineFullScreen);

        public void ShowRewardedAd() =>
            VideoAd.Show(onCloseCallback: OnClosedRewarded, onErrorCallback: OnErrorRewarded,
                onRewardedCallback: OnRewarded);
    }
}