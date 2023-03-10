using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using UnityEngine;

namespace CodeBase.StaticData.Items.Inventory
{
    [CreateAssetMenu(fileName = "InventoryUpgradeLevelData", menuName = "StaticData/Items/Inventory/UpgradeLevel")]
    public class InventoryLevelStaticData : ScriptableObject
    {
        public UpgradeLevelTypeId LevelTypeId;
        public Sprite UpgradeLevel;
    }
}