using System;
using System.Collections;
using Agava.YandexGames;

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
        public event Action<LeaderboardGetEntriesResponse> OnSuccessGetEntries;
        public event Action<LeaderboardEntryResponse> OnSuccessGetEntry;

        IEnumerator Initialize();

        void ShowFullScreenAd();
        void ShowRewardedAd();

        void GetPlayerEntry();
        void GetEntries();
        void SetValue(int value);
    }
}