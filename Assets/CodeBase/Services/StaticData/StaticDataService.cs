using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
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
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string EnemiesPath = "StaticData/Enemies";
        private const string EnemyWeaponsPath = "StaticData/EnemyWeapons";
        private const string HeroWeaponsPath = "StaticData/HeroWeapons";
        private const string InventoryUpgradeLevelsPath = "StaticData/Items/Inventory/UpgradeLevels";
        private const string InventoryUpgradesPath = "StaticData/Items/Inventory/Upgrades";
        private const string PerksPath = "StaticData/Items/Perks";
        private const string ShopAmmoPath = "StaticData/Items/Shop/Ammo";
        private const string ShopItemsPath = "StaticData/Items/Shop/Items";

        private const string ShopUpgradesUpgradableWeaponsPath =
            "StaticData/Items/Shop/Upgrades/UpgradableWeapons";

        private const string ShopUpgradesUpgradeLevelsPath = "StaticData/Items/Shop/Upgrades/UpgradeLevels";

        private const string ShopUpgradesUpgradeLevelsInfoPath =
            "StaticData/Items/Shop/Upgrades/UpgradeLevelsInfo";

        private const string ShopUpgradesPath = "StaticData/Items/Shop/Upgrades/Upgrades";
        private const string ShopWeaponsPath = "StaticData/Items/Shop/Weapons";
        private const string MoneyPath = "StaticData/Items/Money";
        private const string LevelsPath = "StaticData/Levels";
        private const string ProjectilesPath = "StaticData/Projectiles/Projectiles";
        private const string TrailsPath = "StaticData/Projectiles/Trails";
        private const string ShotVfxsPath = "StaticData/Projectiles/ShotVfxs";
        private const string BlastsPath = "StaticData/Projectiles/Blasts";

        private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
        private Dictionary<EnemyWeaponTypeId, EnemyWeaponStaticData> _enemyWeapons;
        private Dictionary<HeroWeaponTypeId, HeroWeaponStaticData> _heroWeapons;
        private Dictionary<LevelTypeId, InventoryUpgradeLevelStaticData> _inventoryUpgradeLevels;
        private Dictionary<UpgradeTypeId, InventoryUpgradeStaticData> _inventoryUpgrades;
        private Dictionary<PerkItem, PerkStaticData> _perks;
        private Dictionary<AmmoItem, ShopAmmoStaticData> _shopAmmunition;
        private Dictionary<ItemTypeId, ShopItemStaticData> _shopItems;
        private Dictionary<HeroWeaponTypeId, UpgradableWeaponStaticData> _shopUpgradableWeapons;
        private Dictionary<LevelTypeId, ShopUpgradeLevelStaticData> _shopUpgradeLevels;
        private Dictionary<ShopUpgradeLevelInfoData, UpgradeLevelInfoStaticData> _shopUpgradeLevelsInfo;
        private Dictionary<UpgradeTypeId, ShopUpgradeStaticData> _shopUpgrades;
        private Dictionary<HeroWeaponTypeId, ShopWeaponStaticData> _shopWeapons;
        private Dictionary<Scene, LevelStaticData> _levels;
        private Dictionary<TrailTypeId, TrailStaticData> _trails;
        private Dictionary<ProjectileTypeId, ProjectileStaticData> _projectiles;
        private Dictionary<ShotVfxTypeId, ShotVfxStaticData> _shotVfxs;
        private Dictionary<BlastTypeId, BlastStaticData> _blasts;
        private Dictionary<MoneyTypeId, MoneyStaticData> _money;

        public void Load()
        {
            _enemies = Resources
                .LoadAll<EnemyStaticData>(EnemiesPath)
                .ToDictionary(x => x.EnemyTypeId, x => x);

            _enemyWeapons = Resources
                .LoadAll<EnemyWeaponStaticData>(EnemyWeaponsPath)
                .ToDictionary(x => x.WeaponTypeId, x => x);

            _heroWeapons = Resources
                .LoadAll<HeroWeaponStaticData>(HeroWeaponsPath)
                .ToDictionary(x => x.WeaponTypeId, x => x);

            _levels = Resources
                .LoadAll<LevelStaticData>(LevelsPath)
                .ToDictionary(x => x.Level, x => x);

            _projectiles = Resources
                .LoadAll<ProjectileStaticData>(ProjectilesPath)
                .ToDictionary(x => x.ProjectileTypeId, x => x);

            _trails = Resources
                .LoadAll<TrailStaticData>(TrailsPath)
                .ToDictionary(x => x.TrailTypeId, x => x);

            _shotVfxs = Resources
                .LoadAll<ShotVfxStaticData>(ShotVfxsPath)
                .ToDictionary(x => x.TypeId, x => x);

            _blasts = Resources
                .LoadAll<BlastStaticData>(BlastsPath)
                .ToDictionary(x => x.TypeId, x => x);

            _inventoryUpgradeLevels = Resources
                .LoadAll<InventoryUpgradeLevelStaticData>(InventoryUpgradeLevelsPath)
                .ToDictionary(x => x.LevelTypeId, x => x);

            _inventoryUpgrades = Resources
                .LoadAll<InventoryUpgradeStaticData>(InventoryUpgradesPath)
                .ToDictionary(x => x.UpgradeTypeId, x => x);

            _perks = Resources
                .LoadAll<PerkStaticData>(PerksPath)
                .ToDictionary(x => new PerkItem(x.PerkTypeId, x.ILevelTypeId), x => x);

            _shopAmmunition = Resources
                .LoadAll<ShopAmmoStaticData>(ShopAmmoPath)
                .ToDictionary(x => new AmmoItem(x.WeaponTypeId, x.AmmoCountType), x => x);

            _shopItems = Resources
                .LoadAll<ShopItemStaticData>(ShopItemsPath)
                .ToDictionary(x => x.TypeId, x => x);

            _shopUpgradableWeapons = Resources
                .LoadAll<UpgradableWeaponStaticData>(ShopUpgradesUpgradableWeaponsPath)
                .ToDictionary(x => x.WeaponTypeId, x => x);

            _shopUpgradeLevels = Resources
                .LoadAll<ShopUpgradeLevelStaticData>(ShopUpgradesUpgradeLevelsPath)
                .ToDictionary(x => x.LevelTypeId, x => x);

            _shopUpgradeLevelsInfo = Resources
                .LoadAll<UpgradeLevelInfoStaticData>(ShopUpgradesUpgradeLevelsInfoPath)
                .ToDictionary(x => new ShopUpgradeLevelInfoData(x.UpgradeTypeId, x.LevelTypeId), x => x);

            _shopUpgrades = Resources
                .LoadAll<ShopUpgradeStaticData>(ShopUpgradesPath)
                .ToDictionary(x => x.UpgradeTypeId, x => x);

            _shopWeapons = Resources
                .LoadAll<ShopWeaponStaticData>(ShopWeaponsPath)
                .ToDictionary(x => x.WeaponTypeId, x => x);

            _money = Resources
                .LoadAll<MoneyStaticData>(MoneyPath)
                .ToDictionary(x => x.TypeId, x => x);
        }

        public EnemyStaticData ForEnemy(EnemyTypeId typeId) =>
            _enemies.TryGetValue(typeId, out EnemyStaticData staticData)
                ? staticData
                : null;

        public EnemyWeaponStaticData ForEnemyWeapon(EnemyWeaponTypeId typeId) =>
            _enemyWeapons.TryGetValue(typeId, out EnemyWeaponStaticData staticData)
                ? staticData
                : null;

        public HeroWeaponStaticData ForHeroWeapon(HeroWeaponTypeId typeId) =>
            _heroWeapons.TryGetValue(typeId, out HeroWeaponStaticData staticData)
                ? staticData
                : null;

        public LevelStaticData ForLevel(Scene scene) =>
            _levels.TryGetValue(scene, out LevelStaticData staticData)
                ? staticData
                : null;

        public ShotVfxStaticData ForShotVfx(ShotVfxTypeId typeId) =>
            _shotVfxs.TryGetValue(typeId, out ShotVfxStaticData staticData)
                ? staticData
                : null;

        public ProjectileStaticData ForProjectile(ProjectileTypeId typeId) =>
            _projectiles.TryGetValue(typeId, out ProjectileStaticData staticData)
                ? staticData
                : null;

        public TrailStaticData ForTrail(TrailTypeId typeId) =>
            _trails.TryGetValue(typeId, out TrailStaticData staticData)
                ? staticData
                : null;

        public BlastStaticData ForBlast(BlastTypeId typeId) =>
            _blasts.TryGetValue(typeId, out BlastStaticData staticData)
                ? staticData
                : null;

        public InventoryUpgradeLevelStaticData ForInventoryUpgradeLevel(LevelTypeId typeId) =>
            _inventoryUpgradeLevels.TryGetValue(typeId, out InventoryUpgradeLevelStaticData staticData)
                ? staticData
                : null;

        public InventoryUpgradeStaticData ForInventoryUpgrade(UpgradeTypeId typeId) =>
            _inventoryUpgrades.TryGetValue(typeId, out InventoryUpgradeStaticData staticData)
                ? staticData
                : null;

        public PerkStaticData ForPerk(PerkTypeId perkTypeId, LevelTypeId levelTypeId) =>
            _perks.TryGetValue(new PerkItem(perkTypeId, levelTypeId), out PerkStaticData staticData)
                ? staticData
                : null;

        public ShopAmmoStaticData ForShopAmmo(HeroWeaponTypeId typeId, AmmoCountType countType) =>
            _shopAmmunition.TryGetValue(new AmmoItem(typeId, countType), out ShopAmmoStaticData staticData)
                ? staticData
                : null;

        public ShopItemStaticData ForShopItem(ItemTypeId typeId) =>
            _shopItems.TryGetValue(typeId, out ShopItemStaticData staticData)
                ? staticData
                : null;

        public UpgradableWeaponStaticData ForUpgradableWeapon(HeroWeaponTypeId typeId) =>
            _shopUpgradableWeapons.TryGetValue(typeId, out UpgradableWeaponStaticData staticData)
                ? staticData
                : null;

        public ShopUpgradeLevelStaticData ForShopUpgradeLevel(LevelTypeId typeId) =>
            _shopUpgradeLevels.TryGetValue(typeId, out ShopUpgradeLevelStaticData staticData)
                ? staticData
                : null;

        public UpgradeLevelInfoStaticData ForUpgradeLevelsInfo(UpgradeTypeId upgradeTypeId, LevelTypeId levelTypeId) =>
            _shopUpgradeLevelsInfo.TryGetValue(new ShopUpgradeLevelInfoData(upgradeTypeId, levelTypeId),
                out UpgradeLevelInfoStaticData staticData)
                ? staticData
                : null;

        public ShopUpgradeStaticData ForShopUpgrade(UpgradeTypeId typeId) =>
            _shopUpgrades.TryGetValue(typeId, out ShopUpgradeStaticData staticData)
                ? staticData
                : null;

        public ShopWeaponStaticData ForShopWeapon(HeroWeaponTypeId typeId) =>
            _shopWeapons.TryGetValue(typeId, out ShopWeaponStaticData staticData)
                ? staticData
                : null;

        public MoneyStaticData ForMoney(MoneyTypeId typeId) =>
            _money.TryGetValue(typeId, out MoneyStaticData staticData)
                ? staticData
                : null;
    }
}