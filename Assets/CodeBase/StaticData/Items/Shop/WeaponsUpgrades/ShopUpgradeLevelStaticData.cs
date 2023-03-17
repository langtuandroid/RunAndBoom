using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    [CreateAssetMenu(fileName = "ShopUpgradeLevelData", menuName = "StaticData/Items/Shop/UpgradeLevel")]
    public class ShopUpgradeLevelStaticData : BaseItemStaticData
    {
        public LevelTypeId LevelTypeId;
        public string Level => LevelTypeId.ToString();
    }
}