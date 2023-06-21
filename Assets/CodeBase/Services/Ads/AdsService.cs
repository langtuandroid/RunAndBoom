using System;
using Agava.YandexGames;

namespace CodeBase.Services.Ads
{
    public class AdsService : IAdsService
    {
        public event Action<bool> OnFullScreenClosed;
        public event Action OnRewardedClosed;

        public void ShowFullScreenAd() =>
            InterstitialAd.Show(onCloseCallback: OnFullScreenClosed);

        public void ShowRewardedAd() =>
            VideoAd.Show(onCloseCallback: OnRewardedClosed);
    }
}