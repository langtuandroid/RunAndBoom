using UnityEngine;

namespace CodeBase.StaticData.Items.Inventory
{
    [CreateAssetMenu(fileName = "InventoryUpgradeLevelData", menuName = "StaticData/Items/Inventory/UpgradeLevel")]
    public class InventoryLevelStaticData : ScriptableObject
    {
        public LevelTypeId LevelTypeId;
        public Sprite UpgradeLevel;
    }
}