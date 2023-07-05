using System;
using System.Collections;
using Agava.YandexGames;

namespace CodeBase.Services.LeaderBoard
{
    public interface ILeaderboardService : IService
    {
        public event Action OnInitializeSuccess;
        public event Action<LeaderboardGetEntriesResponse> OnSuccessGetEntries;
        public event Action<LeaderboardEntryResponse> OnSuccessGetEntry;

        bool IsInitialized();
        IEnumerator Initialize();
        void GetPlayerEntry(string leaderboardName);
        void GetEntries(string leaderboardName);
        void SetValue(string leaderboardName, int value);
    }
}