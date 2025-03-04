﻿using CodeBase.Data.Progress;
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
        private LevelNameData _levelNameData;

        public void LoadProgressData(ProgressData progressData)
        {
            _levelNameData = progressData.WorldData.LevelNameData;
            _levelNameData.LevelChanged += SetLevel;
            _levelNameData.SectorChanged += SetSector;
            SetLevel();
            SetSector();
        }

        private void SetLevel()
        {
            _level = _levelNameData.Level.Replace("Level_", "").Trim();
            SetLevelName(_level, _sector);
        }

        private void SetSector()
        {
            _sector = _levelNameData.Sector;
            SetLevelName(_level, _sector);
        }

        private void SetLevelName(string level, string section) =>
            _levelNumber.text = $"{level}-{section}";
    }
}