using System;

namespace CodeBase.Data
{
    [Serializable]
    public class LevelStats
    {
        public Scene Scene { get; private set; }

        public MoneyData MoneyData { get; private set; }

        public LevelStats(Scene scene)
        {
            Scene = scene;
            MoneyData = new MoneyData();
        }
    }
}