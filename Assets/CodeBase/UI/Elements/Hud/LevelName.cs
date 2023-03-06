using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud
{
    public class LevelName : MonoBehaviour, IProgressReader
    {
        [SerializeField] private TextMeshProUGUI _levelNumber;

        private string _level;
        private string _sector;

        public void LoadProgress(PlayerProgress progress)
        {
            SetLevel(progress.WorldData.LevelNameData.Level);
            SetSector(progress.WorldData.LevelNameData.Sector);
            progress.WorldData.LevelNameData.LevelChanged += SetLevel;
            progress.WorldData.LevelNameData.SectorChanged += SetSector;
        }

        private void SetLevel(string level)
        {
            _level = level;
            SetLevelName(_level, _sector);
        }

        private void SetSector(string section)
        {
            _sector = section;
            SetLevelName(_level, _sector);
        }

        private void SetLevelName(string level, string section) => 
            _levelNumber.text = $"{level}-{section}";
    }
}