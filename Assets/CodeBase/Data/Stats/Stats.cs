using System;

namespace CodeBase.Data.Stats
{
    [Serializable]
    public class Stats
    {
        public LevelStats CurrentLevelStats;
        public SceneDataDictionary LevelStats;
        public MoneyData AllMoney;

        public Stats()
        {
            LevelStats = new SceneDataDictionary();
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

        public void IsMoneyEnough(int value) =>
            AllMoney.IsMoneyEnough(value);

        public void StartNewLevel(Scene scene)
        {
            LevelStats.Dictionary[CurrentLevelStats.Scene] = CurrentLevelStats;
            CurrentLevelStats = new LevelStats(scene);
        }
    }
}