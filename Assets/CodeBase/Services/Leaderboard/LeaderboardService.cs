namespace CodeBase.Services.Leaderboard
{
    public class LeaderboardService
    {
        private const string RatingLeaderboardName = "Rating";

        public void GetPlayerEntry()
        {
            
            Agava.YandexGames.Leaderboard.GetPlayerEntry(RatingLeaderboardName);
        }

        public void GetEntries()
        {
            Agava.YandexGames.Leaderboard.GetEntries(RatingLeaderboardName);
        }
    }
}