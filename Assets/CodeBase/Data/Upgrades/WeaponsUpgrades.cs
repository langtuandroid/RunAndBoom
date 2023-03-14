using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;

namespace CodeBase.Data.Upgrades
{
    [Serializable]
    public class WeaponsUpgrades
    {
        public List<UpgradeData> Upgrades { get; private set; }

        public WeaponsUpgrades() =>
            Upgrades = new List<UpgradeData>();

        public bool IsAvailable(UpgradeTypeId typeId) =>
            Upgrades.Exists(x => x.UpgradeTypeId == typeId);

        public void SetAvailable(UpgradeTypeId typeId) =>
            Upgrades.Add(new UpgradeData(typeId));

        public void LevelUp(UpgradeTypeId typeId) =>
            Upgrades.First(x => x.UpgradeTypeId == typeId).Up();

        public bool IsLastLevel(UpgradeTypeId typeId)
        {
            var upgrade = Upgrades.First(x => x.UpgradeTypeId == typeId);
            return upgrade.LevelTypeId == LevelTypeId.Level_3;
        }
    }
}