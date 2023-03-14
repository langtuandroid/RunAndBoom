using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    [CreateAssetMenu(fileName = "ShopUpgradeLevelData", menuName = "StaticData/Items/Shop/UpgradeLevel")]
    public class ShopUpgradeLevelStaticData : ScriptableObject
    {
        public LevelTypeId LevelTypeId;
        public Sprite UpgradeLevel;
    }
}