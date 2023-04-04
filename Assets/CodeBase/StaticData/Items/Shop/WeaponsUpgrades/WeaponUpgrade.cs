using CodeBase.StaticData.Weapons;

namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    public class WeaponUpgrade
    {
        public HeroWeaponTypeId WeaponTypeId;
        public UpgradeTypeId UpgradeTypeId;
        public LevelTypeId LevelTypeId;

        public WeaponUpgrade(HeroWeaponTypeId weaponTypeId, UpgradeTypeId upgradeTypeId, LevelTypeId levelTypeId)
        {
            WeaponTypeId = weaponTypeId;
            UpgradeTypeId = upgradeTypeId;
            LevelTypeId = levelTypeId;
        }
    }
}