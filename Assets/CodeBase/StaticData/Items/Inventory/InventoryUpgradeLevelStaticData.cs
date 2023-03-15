using UnityEngine;

namespace CodeBase.StaticData.Items.Inventory
{
    [CreateAssetMenu(fileName = "InventoryUpgradeLevelData", menuName = "StaticData/Items/Inventory/UpgradeLevel")]
    public class InventoryUpgradeLevelStaticData : ScriptableObject, ILeveling
    {
        public LevelTypeId LevelTypeId;
        public Sprite Level;

        public LevelTypeId ILevelTypeId => LevelTypeId;
        public Sprite ILevel => Level;
    }
}