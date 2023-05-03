using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    [CreateAssetMenu(fileName = "ShopWeaponUpgradeData", menuName = "StaticData/Items/Shop/WeaponUpgrade")]
    public class ShopUpgradeStaticData : BaseItemStaticData
    {
        public UpgradeTypeId UpgradeTypeId;
        public string RuTitle;
        public string EnTitle;
        public string TrTitle;
    }
}