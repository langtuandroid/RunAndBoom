using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Enemies;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Inventory;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.StaticData.Levels;
using CodeBase.StaticData.ProjectileTraces;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataEnemiesPath = "StaticData/Enemies";
        private const string StaticDataEnemyWeaponsPath = "StaticData/EnemyWeapons";
        private const string StaticDataHeroWeaponsPath = "StaticData/HeroWeapons";
        private const string StaticDataInventoryUpgradeLevelsPath = "StaticData/Items/Inventory/UpgradeLevels";
        private const string StaticDataInventoryUpgradesPath = "StaticData/Items/Inventory/Upgrades";
        private const string StaticDataPerksPath = "StaticData/Items/Perks";
        private const string StaticDataShopAmmoPath = "StaticData/Items/Shop/Ammo";
        private const string StaticDataShopItemsPath = "StaticData/Items/Shop/Items";
        private const string StaticDataShopUpgradesUpgradableWeaponsPath = "StaticData/Items/Shop/Upgrades/UpgradableWeapons";
        private const string StaticDataShopUpgradesUpgradeLevelsPath = "StaticData/Items/Shop/Upgrades/UpgradeLevels";
        private const string StaticDataShopUpgradesUpgradeLevelsInfoPath = "StaticData/Items/Shop/Upgrades/UpgradeLevelsInfo";
        private const string StaticDataShopUpgradesPath = "StaticData/Items/Shop/Upgrades/Upgrades";
        private const string StaticDataLevelsPath = "StaticData/Levels";
        private const string StaticDataProjectileTracesPath = "StaticData/ProjectileTraces";

        private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
        private Dictionary<EnemyWeaponTypeId, EnemyWeaponStaticData> _enemyWeapons;
        private Dictionary<HeroWeaponTypeId, HeroWeaponStaticData> _heroWeapons;
        private Dictionary<LevelTypeId, InventoryLevelStaticData> _inventoryUpgradeLevels;
        private Dictionary<UpgradeTypeId, InventoryUpgradeStaticData> _inventoryUpgrades;
        private Dictionary<PerkItem, PerkStaticData> _perks;
        private Dictionary<AmmoItem, AmmoStaticData> _ammo;
        private Dictionary<ItemTypeId, ItemStaticData> _items;
        private Dictionary<HeroWeaponTypeId, UpgradableWeaponStaticData> _shopUpgradableWeapons;
        private Dictionary<LevelTypeId, ShopUpgradeLevelStaticData> _shopUpgradeLevels;
        private Dictionary<ShopUpgradeLevelInfoData, UpgradeLevelInfoStaticData> _shopUpgradeLevelsInfo;
        private Dictionary<UpgradeTypeId, ShopUpgradeStaticData> _shopUpgrades;
        private Dictionary<string, LevelStaticData> _levels;

        private Dictionary<ProjectileTraceTypeId, ProjectileTraceStaticData> _projectileTraces;

        public void Load()
        {
            _enemies = Resources
                .LoadAll<EnemyStaticData>(StaticDataEnemiesPath)
                .ToDictionary(x => x.EnemyTypeId, x => x);

            _enemyWeapons = Resources
                .LoadAll<EnemyWeaponStaticData>(StaticDataEnemyWeaponsPath)
                .ToDictionary(x => x.WeaponTypeId, x => x);

            _heroWeapons = Resources
                .LoadAll<HeroWeaponStaticData>(StaticDataHeroWeaponsPath)
                .ToDictionary(x => x.WeaponTypeId, x => x);

            _inventoryUpgradeLevels = Resources
                .LoadAll<InventoryLevelStaticData>(StaticDataInventoryUpgradeLevelsPath)
                .ToDictionary(x => x.LevelTypeId, x => x);

            _inventoryUpgrades = Resources
                .LoadAll<InventoryUpgradeStaticData>(StaticDataInventoryUpgradesPath)
                .ToDictionary(x => x.UpgradeTypeId, x => x);

            _perks = Resources
                .LoadAll<PerkStaticData>(StaticDataPerksPath)
                .ToDictionary(x => new PerkItem(x.PerkTypeId, x.LevelTypeId), x => x);

            _ammo = Resources
                .LoadAll<AmmoStaticData>(StaticDataShopAmmoPath)
                .ToDictionary(x => new AmmoItem(x.WeaponTypeId, x.Count), x => x);

            _items = Resources
                .LoadAll<ItemStaticData>(StaticDataShopItemsPath)
                .ToDictionary(x => x.TypeId, x => x);

            _shopUpgradableWeapons = Resources
                .LoadAll<UpgradableWeaponStaticData>(StaticDataShopUpgradesUpgradableWeaponsPath)
                .ToDictionary(x => x.WeaponTypeId, x => x);

            _shopUpgradeLevels = Resources
                .LoadAll<ShopUpgradeLevelStaticData>(StaticDataShopUpgradesUpgradeLevelsPath)
                .ToDictionary(x => x.LevelTypeId, x => x);

            _shopUpgradeLevelsInfo = Resources
                .LoadAll<UpgradeLevelInfoStaticData>(StaticDataShopUpgradesUpgradeLevelsInfoPath)
                .ToDictionary(x => new ShopUpgradeLevelInfoData(x.UpgradeTypeId, x.LevelTypeId), x => x);

            _shopUpgrades = Resources
                .LoadAll<ShopUpgradeStaticData>(StaticDataShopUpgradesPath)
                .ToDictionary(x => x.UpgradeTypeId, x => x);

            _levels = Resources
                .LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);

            _projectileTraces = Resources
                .LoadAll<ProjectileTraceStaticData>(StaticDataProjectileTracesPath)
                .ToDictionary(x => x.ProjectileTraceTypeId, x => x);
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

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData
                : null;

        public InventoryLevelStaticData ForInventoryUpgradeLevel(LevelTypeId typeId) =>
            _inventoryUpgradeLevels.TryGetValue(typeId, out InventoryLevelStaticData staticData)
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

        public AmmoStaticData ForAmmo(HeroWeaponTypeId typeId, int cost, int count) =>
            _ammo.TryGetValue(new AmmoItem(typeId, count), out AmmoStaticData staticData)
                ? staticData
                : null;

        public ItemStaticData ForItem(ItemTypeId typeId) =>
            _items.TryGetValue(typeId, out ItemStaticData staticData)
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
            _shopUpgradeLevelsInfo.TryGetValue(new ShopUpgradeLevelInfoData(upgradeTypeId, levelTypeId), out UpgradeLevelInfoStaticData staticData)
                ? staticData
                : null;

        public ShopUpgradeStaticData ForUpgradeLevelsInfo(UpgradeTypeId typeId) =>
            _shopUpgrades.TryGetValue(typeId, out ShopUpgradeStaticData staticData)
                ? staticData
                : null;

        public ProjectileTraceStaticData ForProjectileTrace(ProjectileTraceTypeId projectileTraceTypeId) =>
            _projectileTraces.TryGetValue(projectileTraceTypeId, out ProjectileTraceStaticData staticData)
                ? staticData
                : null;
    }
}