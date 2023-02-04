using System;

namespace CodeBase.Data
{
    [Serializable]
    public class LevelStats
    {
        public string Name { get; private set; }

        public ScoreData ScoreData { get; private set; }

        public LevelStats()
        {
            ScoreData = new ScoreData();
        }
    }
}