using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Data.Progress;
using CodeBase.Data.Progress.Perks;
using CodeBase.Data.Progress.Upgrades;
using CodeBase.Data.Progress.Weapons;
using CodeBase.Hero;
using CodeBase.Services;
using CodeBase.Services.Input;
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

        private IInputService _inputService;
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
        protected HeroHealth Health;
        protected int Money;
        protected ProgressData ProgressData;
        protected GameObject Hero;
        private Coroutine _coroutineShopItemsGeneration;

        public virtual event Action GenerationStarted;
        public virtual event Action GenerationEnded;

        protected void Construct(GameObject hero)
        {
            Hero = hero;
            Health = hero.GetComponent<HeroHealth>();
            _inputService = AllServices.Container.Single<IInputService>();
            _staticDataService = AllServices.Container.Single<IStaticDataService>();
            RandomService = AllServices.Container.Single<IRandomService>();
        }

        public abstract void Generate();

        protected void GetMoney() =>
            Money = ProgressData.AllStats.AllMoney.Money;

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
            WeaponData[] availableWeapons =
                ProgressData.WeaponsData.WeaponData.Where(data => data.IsAvailable).ToArray();
            List<UpgradeItemData> upgradeItemDatas = ProgressData.WeaponsData.UpgradesData.UpgradeItemDatas;

            foreach (WeaponData weaponData in availableWeapons)
            {
                foreach (UpgradeItemData upgradeItemData in upgradeItemDatas)
                {
                    if (weaponData.WeaponTypeId == upgradeItemData.WeaponTypeId)
                    {
                        UpgradeItemData nextLevelUpgrade =
                            ProgressData.WeaponsData.UpgradesData.GetNextLevelUpgrade(weaponData.WeaponTypeId,
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
                PerkItemData nextLevelPerk = ProgressData.PerksData.GetNextLevelPerk(perkTypeId);

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
            WeaponData[] availableWeapons =
                ProgressData.WeaponsData.WeaponData.Where(data => data.IsAvailable).ToArray();

            foreach (WeaponData weaponData in availableWeapons)
                CheckAmmoCount(weaponData);
        }

        private void CheckAmmoCount(WeaponData weaponData)
        {
            ShopAmmoStaticData maxShopAmmoStaticData =
                _staticDataService.ForShopAmmo(weaponData.WeaponTypeId, AmmoCountType.Max);

            if (Money >= maxShopAmmoStaticData.Count)
            {
                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Max, maxShopAmmoStaticData);
                return;
            }

            ShopAmmoStaticData medShopAmmoStaticData =
                _staticDataService.ForShopAmmo(weaponData.WeaponTypeId, AmmoCountType.Medium);

            if (Money >= medShopAmmoStaticData.Count)
            {
                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Medium, medShopAmmoStaticData);
                return;
            }

            ShopAmmoStaticData minShopAmmoStaticData =
                _staticDataService.ForShopAmmo(weaponData.WeaponTypeId, AmmoCountType.Min);

            if (Money >= minShopAmmoStaticData.Count)
            {
                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Min, minShopAmmoStaticData);
                return;
            }
        }

        protected void CreateWeapons()
        {
            WeaponData[] unavailableWeapons =
                ProgressData.WeaponsData.WeaponData.Where(data => data.IsAvailable == false).ToArray();

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
            {
                if (ProgressData.IsHardMode)
                {
                    if (moneyTypeId == MoneyTypeId.Bag)
                        _moneyTypeIds.Add(moneyTypeId);
                }
                else
                {
                    if (moneyTypeId != MoneyTypeId.Bag && moneyTypeId != MoneyTypeId.BagHard)
                        _moneyTypeIds.Add(moneyTypeId);
                }
            }
        }

        private void AddAmmo(HeroWeaponTypeId typeId, AmmoCountType ammoCountType,
            ShopAmmoStaticData shopAmmoStaticData)
        {
            AmmoItem ammoItem = new AmmoItem(typeId, ammoCountType, GetCount(shopAmmoStaticData.Count));

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

        protected abstract void CreateAmmoItem(GameObject hero, GameObject parent, List<AmmoItem> list,
            bool isClickable);

        protected abstract void CreateItemItem(GameObject hero, GameObject parent, ItemTypeId itemTypeId,
            bool isClickable);

        protected abstract void CreateUpgradeItem(GameObject hero, GameObject parent, List<UpgradeItemData> list,
            bool isClickable);

        protected abstract void CreatePerkItem(GameObject hero, GameObject parent, List<PerkItemData> list,
            bool isClickable);

        protected abstract void CreateWeaponItem(GameObject hero, GameObject parent, List<HeroWeaponTypeId> list,
            bool isClickable);

        protected virtual void CreateMoneyItem(GameObject hero, GameObject parent, List<MoneyTypeId> list,
            bool isClickable)
        {
        }

        protected void GenerateItems()
        {
            if (_availableItems.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                float healthPercentage = Health.Current / Health.Max;
                ShopItemStaticData shopItemStaticData = _staticDataService.ForShopItem(ItemTypeId.HealthRecover);

                if (healthPercentage <= DangerousHealthLevel && _availableItems.Contains(ItemTypeId.HealthRecover) &&
                    Money >= shopItemStaticData.Cost)
                {
                    GameObject view = GetRandomShopItem();

                    if (view != null)
                        CreateItemItem(Hero, view, ItemTypeId.HealthRecover, true);
                }
            }
        }

        protected void GenerateAmmo()
        {
            if (_availableAmmunition.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                GameObject view = GetRandomShopItem();

                if (view != null)
                    CreateAmmoItem(Hero, view, _availableAmmunition, true);
            }
        }

        protected void GenerateUpgrades()
        {
            if (_availableUpgrades.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                GameObject view = GetRandomShopItem();

                if (view != null)
                    CreateUpgradeItem(Hero, view, _availableUpgrades, true);
            }
        }

        protected void GenerateWeapons()
        {
            if (_availableWeapons.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                GameObject view = GetRandomShopItem();

                if (view != null)
                    CreateWeaponItem(Hero, view, _availableWeapons, true);
            }
        }

        protected void GeneratePerks()
        {
            if (_availablePerks.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                GameObject view = GetRandomShopItem();

                if (view != null)
                    CreatePerkItem(Hero, view, _availablePerks, true);
            }
        }

        protected void GenerateMoney()
        {
            if (_availablePerks.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                GameObject view = GetRandomShopItem();

                if (view != null)
                    CreateMoneyItem(Hero, view, _moneyTypeIds, true);
            }
        }

        private GameObject GetRandomShopItem()
        {
            int i = RandomService.NextNumberFrom(_shopItemsNumbers);
            return GameObjectItems[i];
        }

        private int GetCount(int baseCount)
        {
            if (_inputService is MobileInputService)
                return (int)(baseCount * Constants.MobileAmmoMultiplier);
            else
                return baseCount;
        }

        public void LoadProgressData(ProgressData progressData) =>
            ProgressData = progressData;
    }
}