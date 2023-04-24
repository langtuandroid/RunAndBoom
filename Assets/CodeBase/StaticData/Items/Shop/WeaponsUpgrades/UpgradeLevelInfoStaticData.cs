using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    [CreateAssetMenu(fileName = "ShopWeaponUpgradeLevelInfoData",
        menuName = "StaticData/Items/Shop/WeaponUpgradeLevelInfo")]
    public class UpgradeLevelInfoStaticData : ScriptableObject, IShopItemCost
    {
        [Range(1, 50)] public int Cost;
        [Range(0f, 50f)] public float Value;

        public UpgradeTypeId UpgradeTypeId;
        public LevelTypeId LevelTypeId;

        public int ICost => Cost;
    }
}