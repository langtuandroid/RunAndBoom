using System;
using System.Collections.Generic;

namespace CodeBase.Data.Progress.Stats
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

        public void StartNewLevel(SceneId sceneId, int targetPlayTime, int totalEnemies)
        {
            SaveCurrentLevelStats();
            CurrentLevelStats = new LevelStats(sceneId, targetPlayTime, totalEnemies);
        }

        public void SaveCurrentLevelStats() =>
            LevelsStats.Dictionary[CurrentLevelStats.sceneId] = CurrentLevelStats;

        public void RestartedLevel()
        {
            CurrentLevelStats.RestartsData.Increment();
            CurrentLevelStats.PlayTimeData.Clear();
            CurrentLevelStats.KillsData.Clear();
            CurrentLevelStats.MoneyData.Clear();
        }

        public int GetAllLevelsStats()
        {
            int results = 0;

            foreach (KeyValuePair<SceneId, LevelStats> pair in LevelsStats.Dictionary)
                results += pair.Value.Score;

            return results;
        }
    }
}