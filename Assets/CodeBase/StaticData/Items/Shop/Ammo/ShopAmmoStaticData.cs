using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.Ammo
{
    [CreateAssetMenu(fileName = "AmmoData", menuName = "StaticData/Items/Shop/Ammo")]
    public class ShopAmmoStaticData : BaseShopStaticData
    {
        public HeroWeaponTypeId WeaponTypeId;
        public AmmoCountType Count;
    }
}