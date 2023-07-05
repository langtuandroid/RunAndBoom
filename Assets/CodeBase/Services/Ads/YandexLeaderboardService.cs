using System;
using System.Collections;
using Agava.YandexGames;

namespace CodeBase.Services.Ads
{
    public class YandexLeaderboardService : ILeaderboardService
    {
        private const int TopPlayersCount = 5;

        public event Action OnInitializeSuccess;

        public event Action<LeaderboardGetEntriesResponse> OnSuccessGetEntries;
        public event Action<LeaderboardEntryResponse> OnSuccessGetEntry;

        public IEnumerator Initialize()
        {
            if (!IsInitialized())
                yield return YandexGamesSdk.Initialize(OnInitializeSuccess);
        }

        private bool IsInitialized() =>
            YandexGamesSdk.IsInitialized;

        public void GetPlayerEntry(string leaderboardName) =>
            Leaderboard.GetPlayerEntry(leaderboardName: leaderboardName,
                onSuccessCallback: OnSuccessGetEntry);

        public void GetEntries(string leaderboardName) =>
            Leaderboard.GetEntries(leaderboardName: leaderboardName,
                onSuccessCallback: OnSuccessGetEntries, topPlayersCount: TopPlayersCount);

        public void SetValue(string leaderboardName, int value) =>
            Leaderboard.SetScore(leaderboardName: leaderboardName, score: value);
    }
}