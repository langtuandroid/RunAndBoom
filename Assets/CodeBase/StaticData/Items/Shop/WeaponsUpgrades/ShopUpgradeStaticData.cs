using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    [CreateAssetMenu(fileName = "ShopWeaponUpgradeData", menuName = "StaticData/Items/Shop/WeaponUpgrade")]
    public class ShopUpgradeStaticData : ScriptableObject
    {
        public UpgradeTypeId UpgradeTypeId;
        public string Title;
    }
}