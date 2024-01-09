using System;
using UnityEngine;

namespace CodeBase.Data.Progress.Stats
{
    [Serializable]
    public class LevelStats
    {
        private const int TargetScore = 100;
        private const int AddingForRestarts = 1;

        public SceneId SceneId;
        public MoneyData MoneyData;

        public PlayTimeData PlayTimeData;

        // public KillsData KillsData;
        public RestartsData RestartsData;
        public int StarsCount;
        public int Score;
        public int MaxStarsScore;

        public LevelStats(SceneId sceneId, int targetPlayTime, int maxStarsScore, int totalEnemies)
        {
            MaxStarsScore = maxStarsScore;
            SceneId = sceneId;
            MoneyData = new MoneyData();
            PlayTimeData = new PlayTimeData(targetPlayTime);
            // KillsData = new KillsData(totalEnemies);
            RestartsData = new RestartsData();
            StarsCount = (int)Constants.Zero;
            Score = (int)Constants.Zero;
        }

        public void CalculateScore()
        {
            CalculatePlayTime();
            // CalculateKills();
            CalculateRestarts();
            CalculateStars();
            Debug.Log($"Score {Score}");
        }

        private void CalculatePlayTime()
        {
            // if (PlayTimeData.IsPlayTimeLessTarget())
            //     StarsCount++;

            Score += (int)(TargetScore * PlayTimeData.Ratio);
        }

        // private void CalculateKills()
        // {
        //     if (KillsData.IsTotalKilled())
        //         StarsCount++;
        //
        //     Score += (int)Math.Floor(TargetScore * KillsData.Ratio);
        // }

        private void CalculateRestarts()
        {
            // if (RestartsData.Count == Constants.Zero)
            //     StarsCount++;

            Score += TargetScore / (RestartsData.Count + AddingForRestarts);
        }

        private void CalculateStars()
        {
            if (Score > MaxStarsScore)
            {
                StarsCount = 3;
                return;
            }

            if (Score > MaxStarsScore * 2 / 3)
            {
                StarsCount = 2;
                return;
            }

            if (Score > MaxStarsScore / 3)
                StarsCount = 1;
        }
    }
}