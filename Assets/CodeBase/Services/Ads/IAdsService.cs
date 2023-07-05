using System;
using System.Collections;

namespace CodeBase.Services.Ads
{
    public interface IAdsService : IService
    {
        public event Action OnInitializeSuccess;
        public event Action<bool> OnClosedFullScreen;
        public event Action<string> OnErrorFullScreen;
        public event Action OnOfflineFullScreen;
        public event Action OnClosedRewarded;
        public event Action<string> OnErrorRewarded;
        public event Action OnRewarded;

        IEnumerator Initialize();

        void ShowFullScreenAd();
        void ShowRewardedAd();
    }
}