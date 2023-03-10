using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.Weapons
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "StaticData/Items/Shop/Weapon")]
    public class WeaponStaticData : BaseShopStaticData
    {
        public HeroWeaponTypeId WeaponTypeId;
    }
}