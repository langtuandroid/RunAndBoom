using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    [CreateAssetMenu(fileName = "ShopWeaponUpgradeData", menuName = "StaticData/Items/Shop/WeaponUpgrade")]
    public class ShopWeaponUpgradeStaticData : ScriptableObject
    {
        public WeaponUpgradeTypeId UpgradeTypeId;
        public string Title;
    }
}