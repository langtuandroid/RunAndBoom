using CodeBase.StaticData.Weapons;

namespace CodeBase.StaticData.Items.Shop.Ammo
{
    public struct AmmoItem
    {
        public HeroWeaponTypeId WeaponTypeId;
        public AmmoCountType CountType;
        public int Count;

        public AmmoItem(HeroWeaponTypeId weaponTypeId, AmmoCountType countType)
        {
            WeaponTypeId = weaponTypeId;
            CountType = countType;
            Count = 0;
        }

        public AmmoItem(HeroWeaponTypeId weaponTypeId, AmmoCountType countType, int count)
        {
            WeaponTypeId = weaponTypeId;
            CountType = countType;
            Count = count;
        }
    }
}