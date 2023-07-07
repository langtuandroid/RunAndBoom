using System;
using System.Collections;

namespace CodeBase.Services.Ads
{
    public interface IAdsService : IService
    {
        public event Action OnInitializeSuccess;
        public event Action<bool> OnClosedFullScreen;
        public event Action OnOfflineFullScreen;
        public event Action OnClosedRewarded;
        public event Action<string> OnShowFullScreenAdError;
        public event Action<string> OnShowRewardedAdError;
        public event Action OnRewarded;

        bool IsInitialized();
        IEnumerator Initialize();

        void ShowFullScreenAd();
        void ShowRewardedAd();
    }
}