using CodeBase.Services.Pool;
using CodeBase.StaticData.Enemies;
using CodeBase.StaticData.Hits;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Gifts;
using CodeBase.StaticData.Items.Inventory;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.StaticData.Items.Shop.Weapons;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.StaticData.Levels;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.ShotVfxs;
using CodeBase.StaticData.Weapons;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        EnemyStaticData ForEnemy(EnemyTypeId typeId);
        HeroWeaponStaticData ForHeroWeapon(HeroWeaponTypeId typeId);
        EnemyWeaponStaticData ForEnemyWeapon(EnemyWeaponTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
        ProjectileStaticData ForProjectile(ProjectileTypeId typeId);
        ShotVfxStaticData ForShotVfx(ShotVfxTypeId typeId);
        TrailStaticData ForTrail(TrailTypeId typeId);
        BlastStaticData ForBlast(BlastTypeId typeId);
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
        MoneyStaticData ForMoney(MoneyTypeId typeId);
    }
}