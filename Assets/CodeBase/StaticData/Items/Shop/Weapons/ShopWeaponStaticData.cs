using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.Weapons
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "StaticData/Items/Shop/Weapon")]
    public class ShopWeaponStaticData : BaseShopStaticData
    {
        public HeroWeaponTypeId WeaponTypeId;
    }
}