using CodeBase.StaticData.Weapons;

namespace CodeBase.StaticData.Items.Shop.Ammo
{
    public struct AmmoItem
    {
        public HeroWeaponTypeId WeaponTypeId;
        public AmmoCountType CountType;

        public AmmoItem(HeroWeaponTypeId weaponTypeId, AmmoCountType countType)
        {
            WeaponTypeId = weaponTypeId;
            CountType = countType;
        }
    }
}