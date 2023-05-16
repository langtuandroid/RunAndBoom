using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Data.Perks;
using CodeBase.Data.Upgrades;
using CodeBase.Data.Weapons;
using CodeBase.Hero;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Randomizer;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Gifts;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.StaticData.Items.Shop.Weapons;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Windows.Shop;
using UnityEngine;

namespace CodeBase.UI.Windows.Common
{
    public abstract class ItemsGeneratorBase : MonoBehaviour, IProgressReader
    {
        [SerializeField] protected GameObject[] GameObjectItems;

        private const float DangerousHealthLevel = 0.5f;

        private IStaticDataService _staticDataService;
        protected IRandomService RandomService;
        private HashSet<int> _shopItemsNumbers;
        private List<AmmoItem> _unavailableAmmunition;
        private List<ItemTypeId> _unavailableItems;
        private List<UpgradeItemData> _unavailableUpgrades;
        private List<HeroWeaponTypeId> _unavailableWeapons;
        private List<PerkItemData> _unavailablePerks;
        private List<AmmoItem> _availableAmmunition;
        private List<ItemTypeId> _availableItems;
        private List<UpgradeItemData> _availableUpgrades;
        private List<HeroWeaponTypeId> _availableWeapons;
        private List<PerkItemData> _availablePerks;
        private List<MoneyTypeId> _moneyTypeIds;

        private Coroutine _coroutineShopItemsGeneration;

        protected HeroHealth Health;
        protected int Money;
        protected PlayerProgress Progress;

        public virtual event Action GenerationStarted;
        public virtual event Action GenerationEnded;

        protected void Construct(HeroHealth health)
        {
            Health = health;
            _staticDataService = AllServices.Container.Single<IStaticDataService>();
            RandomService = AllServices.Container.Single<IRandomService>();
        }

        public abstract void Generate();

        protected void InitializeEmptyData()
        {
            _shopItemsNumbers = new HashSet<int>(GameObjectItems.Length) { 0, 1, 2 };

            foreach (GameObject shopItem in GameObjectItems)
                shopItem.SetActive(false);

            _unavailableAmmunition = new List<AmmoItem>();
            _unavailableItems = new List<ItemTypeId>();
            _unavailableUpgrades = new List<UpgradeItemData>();
            _unavailableWeapons = new List<HeroWeaponTypeId>();
            _unavailablePerks = new List<PerkItemData>();
            _availableAmmunition = new List<AmmoItem>();
            _availableItems = new List<ItemTypeId>();
            _availableUpgrades = new List<UpgradeItemData>();
            _availableWeapons = new List<HeroWeaponTypeId>();
            _availablePerks = new List<PerkItemData>();
            _moneyTypeIds = new List<MoneyTypeId>();
        }

        protected abstract void GenerateAllItems();
        protected abstract void CreateAllItems();

        protected void CreateNextLevelUpgrades()
        {
            WeaponData[] availableWeapons = Progress.WeaponsData.WeaponDatas.Where(data => data.IsAvailable).ToArray();
            HashSet<UpgradeItemData> upgradeItemDatas = Progress.WeaponsData.UpgradesData.UpgradeItemDatas;

            foreach (WeaponData weaponData in availableWeapons)
            {
                foreach (UpgradeItemData upgradeItemData in upgradeItemDatas)
                {
                    if (weaponData.WeaponTypeId == upgradeItemData.WeaponTypeId)
                    {
                        UpgradeItemData nextLevelUpgrade =
                            Progress.WeaponsData.UpgradesData.GetNextLevelUpgrade(weaponData.WeaponTypeId,
                                upgradeItemData.UpgradeTypeId);

                        if (nextLevelUpgrade.LevelTypeId != LevelTypeId.None)
                        {
                            UpgradeLevelInfoStaticData upgradeLevelInfoStaticData =
                                _staticDataService.ForUpgradeLevelsInfo(nextLevelUpgrade.UpgradeTypeId,
                                    nextLevelUpgrade.LevelTypeId);

                            if (Money >= upgradeLevelInfoStaticData.Cost)
                                _availableUpgrades.Add(nextLevelUpgrade);
                            else
                                _unavailableUpgrades.Add(nextLevelUpgrade);
                        }
                    }
                }
            }
        }

        protected void CreateNextLevelPerks()
        {
            foreach (PerkTypeId perkTypeId in DataExtensions.GetValues<PerkTypeId>())
            {
                PerkItemData nextLevelPerk = Progress.PerksData.GetNextLevelPerk(perkTypeId);

                if (nextLevelPerk.LevelTypeId != LevelTypeId.None)
                {
                    PerkStaticData perkStaticData =
                        _staticDataService.ForPerk(nextLevelPerk.PerkTypeId, nextLevelPerk.LevelTypeId);

                    if (Money >= perkStaticData.Cost)
                        _availablePerks.Add(new PerkItemData(nextLevelPerk.PerkTypeId, nextLevelPerk.LevelTypeId));
                    else
                        _unavailablePerks.Add(new PerkItemData(nextLevelPerk.PerkTypeId, nextLevelPerk.LevelTypeId));
                }
            }
        }

        protected void CreateAmmunition()
        {
            WeaponData[] availableWeapons = Progress.WeaponsData.WeaponDatas.Where(data => data.IsAvailable).ToArray();

            foreach (WeaponData weaponData in availableWeapons)
            {
                switch (weaponData.WeaponTypeId)
                {
                    case HeroWeaponTypeId.GrenadeLauncher:
                        Progress.WeaponsData.WeaponsAmmoData.Ammo.TryGetValue(weaponData.WeaponTypeId,
                            out int grenades);

                        switch (grenades)
                        {
                            case <= 3:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Max);
                                break;

                            case <= 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Medium);
                                break;

                            case > 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Min);
                                break;
                        }

                        break;

                    case HeroWeaponTypeId.RPG:
                        Progress.WeaponsData.WeaponsAmmoData.Ammo.TryGetValue(weaponData.WeaponTypeId,
                            out int rpgRockets);

                        switch (rpgRockets)
                        {
                            case <= 3:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Max);
                                break;

                            case <= 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Medium);
                                break;

                            case > 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Min);
                                break;
                        }

                        break;

                    case HeroWeaponTypeId.RocketLauncher:
                        Progress.WeaponsData.WeaponsAmmoData.Ammo.TryGetValue(weaponData.WeaponTypeId,
                            out int rocketLauncherRockets);

                        switch (rocketLauncherRockets)
                        {
                            case <= 3:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Max);
                                break;

                            case <= 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Medium);
                                break;

                            case > 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Min);
                                break;
                        }

                        break;

                    case HeroWeaponTypeId.Mortar:
                        Progress.WeaponsData.WeaponsAmmoData.Ammo.TryGetValue(weaponData.WeaponTypeId, out int bombs);

                        switch (bombs)
                        {
                            case <= 3:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Medium);
                                break;

                            case <= 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Min);
                                break;

                            case > 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Min);
                                break;
                        }

                        break;
                }
            }
        }

        protected void CreateWeapons()
        {
            WeaponData[] unavailableWeapons =
                Progress.WeaponsData.WeaponDatas.Where(data => data.IsAvailable == false).ToArray();

            foreach (WeaponData weaponData in unavailableWeapons)
            {
                ShopWeaponStaticData weaponStaticData = _staticDataService.ForShopWeapon(weaponData.WeaponTypeId);

                if (Money >= weaponStaticData.Cost)
                    _availableWeapons.Add(weaponData.WeaponTypeId);
                else
                    _unavailableWeapons.Add(weaponData.WeaponTypeId);
            }
        }

        protected void CreateItems()
        {
            foreach (ItemTypeId itemTypeId in DataExtensions.GetValues<ItemTypeId>())
            {
                ShopItemStaticData itemStaticData = _staticDataService.ForShopItem(itemTypeId);

                if (Money >= itemStaticData.Cost)
                    _availableItems.Add(itemTypeId);
                else
                    _unavailableItems.Add(itemTypeId);
            }
        }

        protected void CreateMoney()
        {
            foreach (MoneyTypeId moneyTypeId in DataExtensions.GetValues<MoneyTypeId>())
                _moneyTypeIds.Add(moneyTypeId);
        }

        private void AddAmmo(HeroWeaponTypeId typeId, AmmoCountType ammoCountType)
        {
            ShopAmmoStaticData shopAmmoStaticData = _staticDataService.ForShopAmmo(typeId, ammoCountType);
            AmmoItem ammoItem = new AmmoItem(typeId, ammoCountType, shopAmmoStaticData.Count);

            if (Money >= shopAmmoStaticData.Cost)
                _availableAmmunition.Add(ammoItem);
            else
                _unavailableAmmunition.Add(ammoItem);
        }

        protected void SetHighlightingVisibility(bool isVisible)
        {
            foreach (GameObject shopItem in GameObjectItems)
                shopItem.GetComponent<ShopItemHighlighter>().enabled = isVisible;
        }

        protected abstract void CreateAmmoItem(GameObject parent, List<AmmoItem> list,
            bool isClickable);

        protected abstract void CreateItemItem(GameObject parent, ItemTypeId itemTypeId, bool isClickable);

        protected abstract void CreateUpgradeItem(GameObject parent, List<UpgradeItemData> list, bool isClickable);

        protected abstract void CreatePerkItem(GameObject parent, List<PerkItemData> list, bool isClickable);

        protected abstract void CreateWeaponItem(GameObject parent, List<HeroWeaponTypeId> list, bool isClickable);

        protected virtual void CreateMoneyItem(GameObject parent, List<MoneyTypeId> list, bool isClickable)
        {
        }

        protected void GenerateItems()
        {
            if (_availableItems.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                float healthPercentage = Progress.HealthState.CurrentHp / Progress.HealthState.MaxHp;

                ShopItemStaticData shopItemStaticData = _staticDataService.ForShopItem(ItemTypeId.HealthRecover);

                if (healthPercentage <= DangerousHealthLevel && _availableItems.Contains(ItemTypeId.HealthRecover) &&
                    Money >= shopItemStaticData.Cost)
                {
                    GameObject view = GetRandomShopItem();

                    if (view != null)
                        CreateItemItem(view, ItemTypeId.HealthRecover, true);
                }
            }
        }

        protected void GenerateAmmo()
        {
            if (_availableAmmunition.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                GameObject view = GetRandomShopItem();

                if (view != null)
                    CreateAmmoItem(view, _availableAmmunition, true);
            }
        }

        protected void GenerateUpgrades()
        {
            if (_availableUpgrades.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                GameObject view = GetRandomShopItem();

                if (view != null)
                    CreateUpgradeItem(view, _availableUpgrades, true);
            }
        }

        protected void GenerateWeapons()
        {
            if (_availableWeapons.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                GameObject view = GetRandomShopItem();

                if (view != null)
                    CreateWeaponItem(view, _availableWeapons, true);
            }
        }

        protected void GeneratePerks()
        {
            if (_availablePerks.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                GameObject view = GetRandomShopItem();

                if (view != null)
                    CreatePerkItem(view, _availablePerks, true);
            }
        }

        protected void GenerateMoney()
        {
            if (_availablePerks.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                GameObject view = GetRandomShopItem();

                if (view != null)
                    CreatePerkItem(view, _availablePerks, true);
            }
        }

        private GameObject GetRandomShopItem()
        {
            int i = RandomService.NextNumberFrom(_shopItemsNumbers);
            return GameObjectItems[i];
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Progress = progress;
            Money = Progress.CurrentLevelStats.MoneyData.Money;
        }
    }
}