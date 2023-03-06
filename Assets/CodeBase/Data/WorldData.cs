using System;

namespace CodeBase.Data
{
    [Serializable]
    public class WorldData
    {
        public LevelNameData LevelNameData;

        public WorldData(string level)
        {
            LevelNameData = new LevelNameData(level);
        }
    }
}