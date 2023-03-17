using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    [CreateAssetMenu(fileName = "UpgradableWeaponData", menuName = "StaticData/Items/Shop/UpgradableWeapon")]
    public class UpgradableWeaponStaticData : BaseItemStaticData, IShopItemTitle
    {
        public HeroWeaponTypeId WeaponTypeId;

        [SerializeField] private string RuTitle;
        [SerializeField] private string EnTitle;
        [SerializeField] private string TrTitle;

        public string IRuTitle => RuTitle;
        public string IEnTitle => EnTitle;
        public string ITrTitle => TrTitle;
    }
}