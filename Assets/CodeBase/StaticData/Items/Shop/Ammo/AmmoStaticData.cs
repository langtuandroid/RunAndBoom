using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.Ammo
{
    [CreateAssetMenu(fileName = "AmmoData", menuName = "StaticData/Items/Shop/Ammo")]
    public class AmmoStaticData : BaseShopStaticData
    {
        public HeroWeaponTypeId WeaponTypeId;

        [Range(1, 20)] public int Count;
    }
}