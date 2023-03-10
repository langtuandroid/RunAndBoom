using CodeBase.StaticData.Enemies;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Inventory;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.StaticData.Levels;
using CodeBase.StaticData.ProjectileTraces;
using CodeBase.StaticData.Weapons;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        EnemyStaticData ForEnemy(EnemyTypeId typeId);
        HeroWeaponStaticData ForHeroWeapon(HeroWeaponTypeId typeId);
        EnemyWeaponStaticData ForEnemyWeapon(EnemyWeaponTypeId typeId);
        ProjectileTraceStaticData ForProjectileTrace(ProjectileTraceTypeId projectileTraceTypeId);

        LevelStaticData ForLevel(string sceneKey);
        // WindowStaticData ForWindow(WindowId windowId);
        InventoryLevelStaticData ForInventoryUpgradeLevel(UpgradeLevelTypeId typeId);
        InventoryUpgradeStaticData ForInventoryUpgradeLevel(UpgradeTypeId typeId);
        PerkStaticData ForPerk(PerkTypeId perkTypeId, UpgradeLevelTypeId levelTypeId);
        AmmoStaticData ForAmmo(HeroWeaponTypeId typeId);
        ItemStaticData ForItem(ItemTypeId typeId);
        UpgradableWeaponStaticData ForUpgradableWeapon(HeroWeaponTypeId typeId);
        ShopUpgradeLevelStaticData ForShopUpgradeLevel(UpgradeLevelTypeId typeId);
        UpgradeLevelInfoStaticData ForUpgradeLevelsInfo(UpgradeTypeId upgradeTypeId, UpgradeLevelTypeId levelTypeId);
        ShopUpgradeStaticData ForUpgradeLevelsInfo(UpgradeTypeId typeId);
    }
}