using System;

namespace CodeBase.Data.Progress
{
    [Serializable]
    public class WorldData
    {
        public bool ShowAdOnLevelStart;
        public LevelNameData LevelNameData;

        public WorldData(string level)
        {
            LevelNameData = new LevelNameData(level);
        }
    }
}