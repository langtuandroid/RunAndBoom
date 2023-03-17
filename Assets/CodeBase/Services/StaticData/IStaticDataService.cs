using CodeBase.StaticData.Enemies;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Inventory;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.StaticData.Items.Shop.Weapons;
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
        InventoryUpgradeLevelStaticData ForInventoryUpgradeLevel(LevelTypeId typeId);
        InventoryUpgradeStaticData ForInventoryUpgrade(UpgradeTypeId typeId);
        PerkStaticData ForPerk(PerkTypeId perkTypeId, LevelTypeId levelTypeId);
        ShopAmmoStaticData ForShopAmmo(HeroWeaponTypeId typeId, AmmoCountType countType);
        ShopItemStaticData ForShopItem(ItemTypeId typeId);
        UpgradableWeaponStaticData ForUpgradableWeapon(HeroWeaponTypeId typeId);
        ShopUpgradeLevelStaticData ForShopUpgradeLevel(LevelTypeId typeId);
        UpgradeLevelInfoStaticData ForUpgradeLevelsInfo(UpgradeTypeId upgradeTypeId, LevelTypeId levelTypeId);
        ShopUpgradeStaticData ForShopUpgrade(UpgradeTypeId typeId);
        ShopWeaponStaticData ForShopWeapon(HeroWeaponTypeId typeId);
    }
}