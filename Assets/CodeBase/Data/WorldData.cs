using System;

namespace CodeBase.Data
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