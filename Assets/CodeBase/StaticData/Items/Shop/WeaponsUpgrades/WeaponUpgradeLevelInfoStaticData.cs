using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    [CreateAssetMenu(fileName = "ShopWeaponUpgradeLevelInfoData", menuName = "StaticData/Items/Shop/WeaponUpgradeLevelInfo")]
    public class WeaponUpgradeLevelInfoStaticData : ScriptableObject
    {
        [Range(1, 50)] public int Cost;
        [Range(0f, 50f)] public float AdditionalValue;

        public WeaponUpgradeTypeId UpgradeTypeId;
        public UpgradeLevelTypeId LevelTypeId;
    }
}