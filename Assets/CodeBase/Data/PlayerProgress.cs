using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.CustomClasses;
using CodeBase.StaticData.Weapon;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public LinkedHashSet<WeaponTypeId> SelectedWeaponTypeIds { get; private set; }
        public Dictionary<WeaponTypeId, bool> AvailableWeaponDatas { get; private set; }
        public LevelStats CurrentLevelStats { get; private set; }
        public List<LevelStats> LevelStats { get; private set; }
        public int MaxHP { get; private set; }

        List<WeaponTypeId> typeIds = Enum.GetValues(typeof(WeaponTypeId)).Cast<WeaponTypeId>().ToList();

        public PlayerProgress()
        {
            FillAvailableWeaponDates();
            // SelectedWeaponTypeIds = new LinkedHashSet<WeaponTypeId>(AvailableWeaponDatas.Count);
            FillSelectedWeaponTypeIds();
            CurrentLevelStats = new LevelStats();
        }

        private void FillAvailableWeaponDates()
        {
            AvailableWeaponDatas = new Dictionary<WeaponTypeId, bool>();

            foreach (WeaponTypeId typeId in typeIds)
                AvailableWeaponDatas.Add(typeId, false);
        }

        private void FillSelectedWeaponTypeIds()
        {
            SelectedWeaponTypeIds = new LinkedHashSet<WeaponTypeId>();

            foreach (WeaponTypeId typeId in typeIds)
                SelectedWeaponTypeIds.Add(typeId);
        }

        public void SetSelectedWeapons(LinkedHashSet<WeaponTypeId> selectedWeaponTypeIds)
        {
            SelectedWeaponTypeIds.Clear();
            SelectedWeaponTypeIds = selectedWeaponTypeIds;
        }

        public void SetAvailableWeapons(Dictionary<WeaponTypeId, bool> availableWeaponDates)
        {
            AvailableWeaponDatas.Clear();
            AvailableWeaponDatas = availableWeaponDates;
        }

        public void SetMaxHP(int value) =>
            MaxHP = value;

        public void AddNewLevelStats(LevelStats levelStats) =>
            LevelStats.Add(levelStats);
    }
}