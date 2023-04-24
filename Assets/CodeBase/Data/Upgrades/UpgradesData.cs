using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.StaticData.Weapons;

namespace CodeBase.Data.Upgrades
{
    [Serializable]
    public class UpgradesData
    {
        IEnumerable<HeroWeaponTypeId> _weaponTypeIds = DataExtensions.GetValues<HeroWeaponTypeId>();

        public HashSet<UpgradeItemData> UpgradeItemDatas { get; private set; }

        public event Action<HeroWeaponTypeId, UpgradeItemData> NewUpgradeAdded;

        public UpgradesData()
        {
            int updatesCount = DataExtensions.GetValues<UpgradeTypeId>().Count();
            UpgradeItemDatas = new HashSet<UpgradeItemData>(_weaponTypeIds.Count() * updatesCount);

            // FillTestData();
            FillEmptyData();
        }

        private void FillTestData()
        {
            foreach (HeroWeaponTypeId typeId in _weaponTypeIds)
            {
                UpgradeItemData aiming = new UpgradeItemData(typeId, UpgradeTypeId.Rotation);

                UpgradeItemData reloading = new UpgradeItemData(typeId, UpgradeTypeId.Reloading);
                reloading.Up();

                UpgradeItemData speed = new UpgradeItemData(typeId, UpgradeTypeId.Speed);
                speed.Up();
                speed.Up();

                UpgradeItemData blastSize = new UpgradeItemData(typeId, UpgradeTypeId.BlastSize);
                blastSize.Up();
                blastSize.Up();
                blastSize.Up();

                UpgradeItemDatas.Add(aiming);
                UpgradeItemDatas.Add(reloading);
                UpgradeItemDatas.Add(speed);
                UpgradeItemDatas.Add(blastSize);
            }
        }

        private void FillEmptyData()
        {
            foreach (HeroWeaponTypeId typeId in _weaponTypeIds)
            {
                UpgradeItemDatas.Add(new UpgradeItemData(typeId, UpgradeTypeId.Rotation));
                UpgradeItemDatas.Add(new UpgradeItemData(typeId, UpgradeTypeId.Reloading));
                UpgradeItemDatas.Add(new UpgradeItemData(typeId, UpgradeTypeId.Speed));
                UpgradeItemDatas.Add(new UpgradeItemData(typeId, UpgradeTypeId.BlastSize));
            }
        }

        public bool IsAvailable(HeroWeaponTypeId weaponTypeId, UpgradeTypeId upgradeTypeId) =>
            UpgradeItemDatas.Any(x =>
                x.WeaponTypeId == weaponTypeId && x.UpgradeTypeId == upgradeTypeId &&
                x.LevelTypeId != LevelTypeId.None);

        public void LevelUp(HeroWeaponTypeId weaponTypeId, UpgradeTypeId upgradeTypeId)
        {
            UpgradeItemData upgrade =
                UpgradeItemDatas.First(x => x.WeaponTypeId == weaponTypeId && x.UpgradeTypeId == upgradeTypeId);

            if (upgrade.LevelTypeId == LevelTypeId.Level_3)
                return;

            upgrade.Up();

            if (upgrade.LevelTypeId == LevelTypeId.Level_1)
                NewUpgradeAdded?.Invoke(weaponTypeId, upgrade);
        }

        public UpgradeItemData GetNextLevelUpgrade(HeroWeaponTypeId weaponTypeId, UpgradeTypeId upgradeTypeId)
        {
            UpgradeItemData upgrade =
                UpgradeItemDatas.First(x => x.WeaponTypeId == weaponTypeId && x.UpgradeTypeId == upgradeTypeId);
            LevelTypeId nextLevel = upgrade.GetNextLevel();
            return new UpgradeItemData(upgrade.WeaponTypeId, upgrade.UpgradeTypeId, nextLevel);
        }
    }
}