using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.StaticData.Weapons;

namespace CodeBase.Data.Upgrades
{
    [Serializable]
    public class WeaponUpgradesData
    {
        IEnumerable<HeroWeaponTypeId> _weaponTypeIds = DataExtensions.GetValues<HeroWeaponTypeId>();

        public Dictionary<HeroWeaponTypeId, List<UpgradeItemData>> UpgradeItemDatas { get; private set; }

        public event Action<HeroWeaponTypeId, UpgradeItemData> NewUpgradeAdded;

        public WeaponUpgradesData()
        {
            int updatesCount = DataExtensions.GetValues<UpgradeTypeId>().Count();

            UpgradeItemDatas = new Dictionary<HeroWeaponTypeId, List<UpgradeItemData>>(_weaponTypeIds.Count());

            foreach (HeroWeaponTypeId typeId in _weaponTypeIds)
                UpgradeItemDatas[typeId] = new List<UpgradeItemData>(updatesCount);

            FillTestData();
        }

        private void FillTestData()
        {
            UpgradeItemData aiming = new UpgradeItemData(UpgradeTypeId.Aiming);

            UpgradeItemData reloading = new UpgradeItemData(UpgradeTypeId.Reloading);
            reloading.Up();

            UpgradeItemData speed = new UpgradeItemData(UpgradeTypeId.Speed);
            speed.Up();
            speed.Up();

            UpgradeItemData blastSize = new UpgradeItemData(UpgradeTypeId.BlastSize);
            blastSize.Up();
            blastSize.Up();
            blastSize.Up();

            foreach (HeroWeaponTypeId typeId in _weaponTypeIds)
            {
                UpgradeItemDatas[typeId].Add(aiming);
                UpgradeItemDatas[typeId].Add(reloading);
                UpgradeItemDatas[typeId].Add(speed);
                UpgradeItemDatas[typeId].Add(blastSize);
            }
        }

        private void FillEmptyData()
        {
            UpgradeItemData aiming = new UpgradeItemData(UpgradeTypeId.Aiming);
            UpgradeItemData reloading = new UpgradeItemData(UpgradeTypeId.Reloading);
            UpgradeItemData speed = new UpgradeItemData(UpgradeTypeId.Speed);
            UpgradeItemData blastSize = new UpgradeItemData(UpgradeTypeId.BlastSize);

            foreach (HeroWeaponTypeId typeId in _weaponTypeIds)
            {
                UpgradeItemDatas[typeId].Add(aiming);
                UpgradeItemDatas[typeId].Add(reloading);
                UpgradeItemDatas[typeId].Add(speed);
                UpgradeItemDatas[typeId].Add(blastSize);
            }
        }

        public bool IsAvailable(HeroWeaponTypeId weaponTypeId, UpgradeTypeId typeId) =>
            UpgradeItemDatas[weaponTypeId].Exists(x => x.UpgradeTypeId == typeId && x.LevelTypeId != LevelTypeId.None);

        public void LevelUp(HeroWeaponTypeId weaponTypeId, UpgradeTypeId upgradeTypeId)
        {
            UpgradeItemData upgrade = UpgradeItemDatas[weaponTypeId].First(x => x.UpgradeTypeId == upgradeTypeId);

            if (upgrade.LevelTypeId == LevelTypeId.Level_3)
                return;

            upgrade.Up();

            if (upgrade.LevelTypeId == LevelTypeId.None)
                NewUpgradeAdded?.Invoke(weaponTypeId, upgrade);
        }

        public bool IsLastLevel(HeroWeaponTypeId weaponTypeId, UpgradeTypeId upgradeTypeId)
        {
            UpgradeItemData upgrade = UpgradeItemDatas[weaponTypeId].First(x => x.UpgradeTypeId == upgradeTypeId);
            return upgrade.LevelTypeId == LevelTypeId.Level_3;
        }
    }
}