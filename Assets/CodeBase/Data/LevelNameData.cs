using System;

namespace CodeBase.Data
{
    [Serializable]
    public class LevelNameData
    {
        private const string InitialSector = "1";

        public string Level;
        public string Sector;

        public event Action LevelChanged;
        public event Action SectorChanged;

        public LevelNameData(string level)
        {
            var levelNumber = GetLevelNumber(level);
            ChangeLevel(levelNumber);
            ChangeSector(InitialSector);
        }

        private string GetLevelNumber(string level) =>
            level.Replace("Level ", "").Trim();

        public void ChangeLevel(string level)
        {
            Level = level;
            LevelChanged?.Invoke();
            ChangeSector("1");
        }

        public void ChangeSector(string section)
        {
            Sector = section;
            SectorChanged?.Invoke();
        }
    }
}