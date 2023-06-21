using System;

namespace CodeBase.Services.Ads
{
    public interface IAdsService : IService
    {
        public event Action<bool> OnFullScreenClosed;
        public event Action OnRewardedClosed;

        void ShowFullScreenAd();
        void ShowRewardedAd();
    }
}