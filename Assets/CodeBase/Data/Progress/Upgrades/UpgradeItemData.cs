using System;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.StaticData.Weapons;

namespace CodeBase.Data.Progress.Upgrades
{
    [Serializable]
    public class UpgradeItemData : LevelingItemData
    {
        public HeroWeaponTypeId WeaponTypeId;
        public UpgradeTypeId UpgradeTypeId;

        public UpgradeItemData(HeroWeaponTypeId weaponTypeId, UpgradeTypeId upgradeTypeId)
        {
            WeaponTypeId = weaponTypeId;
            UpgradeTypeId = upgradeTypeId;
            InitNew();
        }

        public UpgradeItemData(HeroWeaponTypeId weaponTypeId, UpgradeTypeId upgradeTypeId, LevelTypeId levelTypeId)
        {
            WeaponTypeId = weaponTypeId;
            UpgradeTypeId = upgradeTypeId;
            LevelTypeId = levelTypeId;
        }

        public void Up() =>
            base.Up();

        public LevelTypeId GetNextLevel() =>
            base.GetNextLevel();
    }
}