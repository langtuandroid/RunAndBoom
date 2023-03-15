using System;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;

namespace CodeBase.Data.Upgrades
{
    [Serializable]
    public class UpgradeItemData : LevelingItemData
    {
        public UpgradeTypeId UpgradeTypeId { get; private set; }

        public UpgradeItemData(UpgradeTypeId upgradeTypeId)
        {
            UpgradeTypeId = upgradeTypeId;
        }

        public void Up() =>
            base.Up();
    }
}