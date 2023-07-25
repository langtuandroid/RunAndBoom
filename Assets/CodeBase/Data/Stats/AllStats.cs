using System;
using System.Collections.Generic;

namespace CodeBase.Data.Stats
{
    [Serializable]
    public class AllStats
    {
        public LevelStats CurrentLevelStats;
        public SceneDataDictionary LevelsStats;
        public MoneyData AllMoney;

        public AllStats()
        {
            LevelsStats = new SceneDataDictionary();
            AllMoney = new MoneyData();
        }

        public void AddMoney(int value)
        {
            CurrentLevelStats.MoneyData.AddMoney(value);
            AllMoney.AddMoney(value);
        }

        public void ReduceMoney(int value)
        {
            CurrentLevelStats.MoneyData.ReduceMoney(value);
            AllMoney.ReduceMoney(value);
        }

        public bool IsMoneyEnough(int value) =>
            AllMoney.IsMoneyEnough(value);

        public void StartNewLevel(Scene scene, int targetPlayTime, int totalEnemies)
        {
            SaveCurrentLevelStats();
            CurrentLevelStats = new LevelStats(scene, targetPlayTime, totalEnemies);
        }

        public void SaveCurrentLevelStats() =>
            LevelsStats.Dictionary[CurrentLevelStats.Scene] = CurrentLevelStats;

        public void RestartedLevel()
        {
            CurrentLevelStats.RestartsData.Increment();
            CurrentLevelStats.PlayTimeData.Clear();
            CurrentLevelStats.KillsData.Clear();
            CurrentLevelStats.MoneyData.Clear();
        }

        public int GetLevelsStats()
        {
            int results = 0;

            foreach (KeyValuePair<Scene, LevelStats> pair in LevelsStats.Dictionary)
                results += pair.Value.Score;

            return results;
        }
    }
}