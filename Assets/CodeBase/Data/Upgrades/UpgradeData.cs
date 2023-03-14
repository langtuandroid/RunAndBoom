using System;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;

namespace CodeBase.Data.Upgrades
{
    [Serializable]
    public class UpgradeData: LeveledData
    {
        public UpgradeTypeId UpgradeTypeId { get; private set; }

        public UpgradeData(UpgradeTypeId upgradeTypeId)
        {
            UpgradeTypeId = upgradeTypeId;
        }

        public void Up() => 
            base.Up();
    }
}