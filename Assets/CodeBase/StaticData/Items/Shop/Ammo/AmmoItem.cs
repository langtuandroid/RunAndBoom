using CodeBase.StaticData.Weapons;

namespace CodeBase.StaticData.Items.Shop.Ammo
{
    public class AmmoItem
    {
        public HeroWeaponTypeId WeaponTypeId;
        public int Count;

        public AmmoItem(HeroWeaponTypeId weaponTypeId, int count)
        {
            WeaponTypeId = weaponTypeId;
            Count = count;
        }
    }
}