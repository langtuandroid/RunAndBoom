using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    [CreateAssetMenu(fileName = "UpgradableWeaponData", menuName = "StaticData/Items/Shop/UpgradableWeapon")]
    public class UpgradableWeaponStaticData : BaseItemStaticData
    {
        public HeroWeaponTypeId WeaponTypeId;

       public string RuTitle;
       public string EnTitle;
       public string TrTitle;
    }
}