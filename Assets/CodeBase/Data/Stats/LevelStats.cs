using System;

namespace CodeBase.Data.Stats
{
    [Serializable]
    public class LevelStats
    {
        public Scene Scene;
        public MoneyData MoneyData;

        public LevelStats(Scene scene)
        {
            Scene = scene;
            MoneyData = new MoneyData();
        }
    }
}