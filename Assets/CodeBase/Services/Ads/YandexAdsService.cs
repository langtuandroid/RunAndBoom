using System;
using System.Collections;
using Agava.YandexGames;

namespace CodeBase.Services.Ads
{
    public class YandexAdsService : IAdsService
    {
        private const string RatingLeaderboardName = "Rating";
        private const int TopPlayersCount = 5;

        public event Action OnInitializeSuccess;
        public event Action<bool> OnClosedFullScreen;
        public event Action<string> OnErrorFullScreen;
        public event Action OnOfflineFullScreen;
        public event Action OnClosedRewarded;
        public event Action<string> OnErrorRewarded;
        public event Action OnRewarded;
        public event Action<LeaderboardGetEntriesResponse> OnSuccessGetEntries;
        public event Action<LeaderboardEntryResponse> OnSuccessGetEntry;

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

        public void GetPlayerEntry() =>
            Leaderboard.GetPlayerEntry(leaderboardName: RatingLeaderboardName,
                onSuccessCallback: OnSuccessGetEntry);

        public void GetEntries() =>
            Leaderboard.GetEntries(leaderboardName: RatingLeaderboardName,
                onSuccessCallback: OnSuccessGetEntries, topPlayersCount: TopPlayersCount);

        public void SetValue(int value) =>
            Leaderboard.SetScore(leaderboardName: RatingLeaderboardName, score: value);
    }
}