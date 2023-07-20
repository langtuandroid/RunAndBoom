using System;
using System.Collections;

namespace CodeBase.Services.Ads
{
    public interface IAdsService : IService
    {
        public event Action OnInitializeSuccess;
        public event Action OnClosedVideoAd;
        public event Action OnOfflineFullScreenAd;
        public event Action<bool> OnClosedFullScreenAd;
        public event Action<string> OnShowVideoAdError;
        public event Action<string> OnShowFullScreenAdError;
        public event Action OnRewardedAd;

        bool IsInitialized();
        IEnumerator Initialize();

        void ShowVideoAd();
        void ShowFullScreenAd();
    }
}