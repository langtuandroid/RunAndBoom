using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    [CreateAssetMenu(fileName = "ShopWeaponUpgradeData", menuName = "StaticData/Items/Shop/WeaponUpgrade")]
    public class ShopUpgradeStaticData : BaseItemStaticData, IShopItemTitle
    {
        public UpgradeTypeId UpgradeTypeId;
        [SerializeField] string RuTitle;
        [SerializeField] string EnTitle;
        [SerializeField] string TrTitle;

        public string IRuTitle => RuTitle;
        public string IEnTitle => EnTitle;
        public string ITrTitle => TrTitle;
    }
}