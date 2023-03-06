using System;
using UnityEngine;

namespace CodeBase.Data
{
    [SerializeField]
    public class LevelNameData
    {
        private const string InitialSector = "1";

        public string Level { get; private set; }
        public string Sector { get; private set; }

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
        }

        public void ChangeSector(string section)
        {
            Sector = section;
            SectorChanged?.Invoke();
        }
    }
}