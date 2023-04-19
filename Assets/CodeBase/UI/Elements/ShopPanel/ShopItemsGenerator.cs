using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Data.Perks;
using CodeBase.Data.Upgrades;
using CodeBase.Data.Weapons;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Randomizer;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.StaticData.Items.Shop.Weapons;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Elements.ShopPanel.ViewItems;
using UnityEngine;

namespace CodeBase.UI.Elements.ShopPanel
{
    public class ShopItemsGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject[] _shopItems;
        // [SerializeField] private ShopButtons _shopButtons;

        private const float DangerousHealthLevel = 0.5f;
        private const string Ammunition = "ammo";
        private const string Weapons = "weapons";
        private const string Perks = "perks";
        private const string Upgrades = "upgrades";
        private const string Items = "items";

        private IPlayerProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IRandomService _randomService;
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
        private int _money;
        private Coroutine _coroutineShopItemsGeneration;
        private WaitForSeconds _delayShopItemsDisplaying = new WaitForSeconds(0.5f);

        public event Action GenerationStarted;
        public event Action GenerationEnded;

        private void Update()
        {
            // Debug.Log($"money: {_money}");
        }

        public void Construct(IPlayerProgressService progressService, IStaticDataService staticDataService, IRandomService randomService)
        {
            _progressService = progressService;
            _staticDataService = staticDataService;
            _randomService = randomService;

            InitializeEmptyData();
            _money = _progressService.Progress.CurrentLevelStats.MoneyData.Money;
        }

        private void InitializeEmptyData()
        {
            _shopItemsNumbers = new HashSet<int>(_shopItems.Length) { 0, 1, 2 };

            foreach (GameObject shopItem in _shopItems)
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
        }

        public void GenerateShopItems()
        {
            GenerationStarted?.Invoke();
            SetHighlightingVisibility(false);
            InitializeEmptyData();
            CreateNextLevelPerks();
            CreateNextLevelUpgrades();
            CreateAmmunition();
            CreateWeapons();
            CreateItems();

            // StartCoroutine(CoroutineShowShopItems());

            GenerateItems();
            GenerateAmmo();
            GeneratePerks();
            GenerateUpgrades();
            GenerateWeapons();

            // HideEmpty();

            SetHighlightingVisibility(true);
            GenerationEnded?.Invoke();
        }

        private void CreateNextLevelUpgrades()
        {
            WeaponData[] availableWeapons = _progressService.Progress.WeaponsData.WeaponDatas.Where(data => data.IsAvailable).ToArray();
            HashSet<UpgradeItemData> upgradeItemDatas = _progressService.Progress.WeaponsData.WeaponUpgradesData.UpgradeItemDatas;

            foreach (WeaponData weaponData in availableWeapons)
            {
                foreach (UpgradeItemData upgradeItemData in upgradeItemDatas)
                {
                    if (weaponData.WeaponTypeId == upgradeItemData.WeaponTypeId)
                    {
                        UpgradeItemData nextLevelUpgrade =
                            _progressService.Progress.WeaponsData.WeaponUpgradesData.GetNextLevelUpgrade(weaponData.WeaponTypeId,
                                upgradeItemData.UpgradeTypeId);

                        if (nextLevelUpgrade.LevelTypeId != LevelTypeId.None)
                        {
                            UpgradeLevelInfoStaticData upgradeLevelInfoStaticData =
                                _staticDataService.ForUpgradeLevelsInfo(nextLevelUpgrade.UpgradeTypeId, nextLevelUpgrade.LevelTypeId);

                            if (_money >= upgradeLevelInfoStaticData.Cost)
                                _availableUpgrades.Add(nextLevelUpgrade);

                            _unavailableUpgrades.Add(nextLevelUpgrade);
                        }
                    }
                }
            }
        }

        private void CreateNextLevelPerks()
        {
            foreach (PerkTypeId perkTypeId in DataExtensions.GetValues<PerkTypeId>())
            {
                PerkItemData nextLevelPerk = _progressService.Progress.PerksData.GetNextLevelPerk(perkTypeId);

                if (nextLevelPerk.LevelTypeId != LevelTypeId.None)
                {
                    PerkStaticData perkStaticData = _staticDataService.ForPerk(nextLevelPerk.PerkTypeId, nextLevelPerk.LevelTypeId);

                    if (_money >= perkStaticData.Cost)
                        _availablePerks.Add(new PerkItemData(nextLevelPerk.PerkTypeId, nextLevelPerk.LevelTypeId));

                    _unavailablePerks.Add(new PerkItemData(nextLevelPerk.PerkTypeId, nextLevelPerk.LevelTypeId));
                }
            }
        }

        private void CreateAmmunition()
        {
            WeaponData[] availableWeapons = _progressService.Progress.WeaponsData.WeaponDatas.Where(data => data.IsAvailable).ToArray();

            foreach (WeaponData weaponData in availableWeapons)
            {
                switch (weaponData.WeaponTypeId)
                {
                    case HeroWeaponTypeId.GrenadeLauncher:
                        _progressService.Progress.WeaponsData.WeaponsAmmoData.Ammo.TryGetValue(weaponData.WeaponTypeId, out int grenades);

                        switch (grenades)
                        {
                            case <= 3:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Ten);
                                break;

                            case <= 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Five);
                                break;

                            case > 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.One);
                                break;
                        }

                        break;

                    case HeroWeaponTypeId.RPG:
                        _progressService.Progress.WeaponsData.WeaponsAmmoData.Ammo.TryGetValue(weaponData.WeaponTypeId, out int rpgRockets);

                        switch (rpgRockets)
                        {
                            case <= 3:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Ten);
                                break;

                            case <= 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Five);
                                break;

                            case > 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.One);
                                break;
                        }

                        break;

                    case HeroWeaponTypeId.RocketLauncher:
                        _progressService.Progress.WeaponsData.WeaponsAmmoData.Ammo.TryGetValue(weaponData.WeaponTypeId, out int rocketLauncherRockets);

                        switch (rocketLauncherRockets)
                        {
                            case <= 3:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Ten);
                                break;

                            case <= 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Five);
                                break;

                            case > 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.One);
                                break;
                        }

                        break;

                    case HeroWeaponTypeId.Mortar:
                        _progressService.Progress.WeaponsData.WeaponsAmmoData.Ammo.TryGetValue(weaponData.WeaponTypeId, out int bombs);

                        switch (bombs)
                        {
                            case <= 3:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.Five);
                                break;

                            case <= 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.One);
                                break;

                            case > 8:
                                AddAmmo(weaponData.WeaponTypeId, AmmoCountType.One);
                                break;
                        }

                        break;
                }
            }
        }

        private void AddAmmo(HeroWeaponTypeId typeId, AmmoCountType ammoCount)
        {
            ShopAmmoStaticData shopAmmoStaticData = _staticDataService.ForShopAmmo(typeId, ammoCount);
            AmmoItem ammoItem = new AmmoItem(typeId, ammoCount);

            if (_money >= shopAmmoStaticData.Cost)
                _availableAmmunition.Add(ammoItem);

            _unavailableAmmunition.Add(ammoItem);
        }

        private void CreateWeapons()
        {
            WeaponData[] unavailableWeapons = _progressService.Progress.WeaponsData.WeaponDatas.Where(data => data.IsAvailable == false).ToArray();

            foreach (WeaponData weaponData in unavailableWeapons)
            {
                ShopWeaponStaticData weaponStaticData = _staticDataService.ForShopWeapon(weaponData.WeaponTypeId);

                if (_money >= weaponStaticData.Cost)
                    _availableWeapons.Add(weaponData.WeaponTypeId);

                _unavailableWeapons.Add(weaponData.WeaponTypeId);
            }
        }

        private void CreateItems()
        {
            foreach (ItemTypeId itemTypeId in DataExtensions.GetValues<ItemTypeId>())
            {
                ShopItemStaticData itemStaticData = _staticDataService.ForShopItem(itemTypeId);

                if (_money >= itemStaticData.Cost)
                    _availableItems.Add(itemTypeId);

                _unavailableItems.Add(itemTypeId);
            }
        }

        private IEnumerator CoroutineShowShopItems()
        {
            Debug.Log("CoroutineShowShopItems");
            yield return ShowShopItems();
            yield return _delayShopItemsDisplaying;
            yield return ShowShopItems();
            yield return _delayShopItemsDisplaying;
        }

        private void SetHighlightingVisibility(bool isVisible)
        {
            foreach (GameObject shopItem in _shopItems)
                shopItem.GetComponent<ShopItemHighlighter>().SetVisibility(isVisible);
        }

        private IEnumerator ShowShopItems()
        {
            Debug.Log("ShowShopItems");
            ShowItems();

            yield return null;
        }

        private void ShowItems()
        {
            Debug.Log("ShowShopItems");
            foreach (GameObject shopItem in _shopItems)
            {
                List<string> lists = new List<string>();
                lists.Add(Ammunition);
                lists.Add(Weapons);
                lists.Add(Perks);
                lists.Add(Upgrades);
                lists.Add(Items);

                string nextFrom = _randomService.NextFrom(lists);

                switch (nextFrom)
                {
                    case Ammunition:
                    {
                        if (_unavailableAmmunition.Count != 0)
                            CreateAmmoPurchasingItemView(shopItem, _unavailableAmmunition, false);

                        break;
                    }

                    case Weapons:
                    {
                        if (_unavailableWeapons.Count != 0)
                            CreateWeaponPurchasingItemView(shopItem, _unavailableWeapons, false);

                        break;
                    }

                    case Perks:
                    {
                        if (_unavailablePerks.Count != 0)
                            CreatePerkPurchasingItemView(shopItem, _unavailablePerks, false);

                        break;
                    }

                    case Upgrades:
                    {
                        if (_unavailableUpgrades.Count != 0)
                            CreateUpgradePurchasingItemView(shopItem, _unavailableUpgrades, false);

                        break;
                    }

                    case Items:
                    {
                        if (_unavailableItems.Count != 0)
                            CreateItemPurchasingItemView(shopItem, _unavailableItems, false);

                        break;
                    }
                }
            }
        }

        private void CreateItemPurchasingItemView(GameObject parent, List<ItemTypeId> list, bool isClickable)
        {
            ItemTypeId itemTypeId = _randomService.NextFrom(list);
            // DisableComponentsExcept(parent);
            ItemPurchasingItemView view = parent.GetComponentInChildren<ItemPurchasingItemView>();
            parent.SetActive(true);
            parent.GetComponent<ShopCell>().Show(view);
            // view.enabled = true;
            view.Construct(itemTypeId, _progressService);
            view.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        private void CreateItemPurchasingItemView(GameObject parent, ItemTypeId itemTypeId, bool isClickable)
        {
            // DisableComponentsExcept(parent);
            ItemPurchasingItemView view = parent.GetComponentInChildren<ItemPurchasingItemView>();
            parent.SetActive(true);
            parent.GetComponent<ShopCell>().Show(view);
            // view.enabled = true;
            view.Construct(itemTypeId, _progressService);
            view.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        private void CreateUpgradePurchasingItemView(GameObject parent, List<UpgradeItemData> list, bool isClickable)
        {
            UpgradeItemData upgradeItemData = _randomService.NextFrom(list);
            // DisableComponentsExcept(parent);
            UpgradePurchasingItemView view = parent.GetComponentInChildren<UpgradePurchasingItemView>();
            parent.SetActive(true);
            parent.GetComponent<ShopCell>().Show(view);
            // view.enabled = true;
            view.Construct(upgradeItemData, _progressService);
            view.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        private void CreatePerkPurchasingItemView(GameObject parent, List<PerkItemData> list, bool isClickable)
        {
            PerkItemData perkItemData = _randomService.NextFrom(list);
            // DisableComponentsExcept(parent);
            PerkPurchasingItemView view = parent.GetComponentInChildren<PerkPurchasingItemView>();
            parent.SetActive(true);
            parent.GetComponent<ShopCell>().Show(view);
            // view.enabled = true;
            view.Construct(perkItemData, _progressService);
            view.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        private void CreateAmmoPurchasingItemView(GameObject parent, List<AmmoItem> list, bool isClickable)
        {
            AmmoItem ammoItem = _randomService.NextFrom(list);
            // DisableComponentsExcept(parent);
            AmmoPurchasingItemView view = parent.GetComponentInChildren<AmmoPurchasingItemView>();
            parent.SetActive(true);
            parent.GetComponent<ShopCell>().Show(view);
            // view.enabled = true;
            view.Construct(ammoItem, _progressService);
            view.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        private void CreateWeaponPurchasingItemView(GameObject parent, List<HeroWeaponTypeId> list, bool isClickable)
        {
            HeroWeaponTypeId weaponTypeId = _randomService.NextFrom(list);
            // DisableComponentsExcept(parent);
            WeaponPurchasingItemView view = parent.GetComponentInChildren<WeaponPurchasingItemView>();
            parent.SetActive(true);
            parent.GetComponent<ShopCell>().Show(view);
            // view.enabled = true;
            view.Construct(weaponTypeId, _progressService);
            view.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        private void DisableComponentsExcept(GameObject view)
        {
            ItemPurchasingItemView itemPurchasingItemView = view.GetComponentInChildren<ItemPurchasingItemView>();
            itemPurchasingItemView.ClearData();
            itemPurchasingItemView.enabled = false;
            AmmoPurchasingItemView ammoPurchasingItemView = view.GetComponentInChildren<AmmoPurchasingItemView>();
            ammoPurchasingItemView.ClearData();
            ammoPurchasingItemView.enabled = false;
            UpgradePurchasingItemView upgradePurchasingItemView = view.GetComponentInChildren<UpgradePurchasingItemView>();
            upgradePurchasingItemView.ClearData();
            upgradePurchasingItemView.enabled = false;
            PerkPurchasingItemView perkPurchasingItemView = view.GetComponentInChildren<PerkPurchasingItemView>();
            perkPurchasingItemView.ClearData();
            perkPurchasingItemView.enabled = false;
            WeaponPurchasingItemView weaponPurchasingItemView = view.GetComponentInChildren<WeaponPurchasingItemView>();
            weaponPurchasingItemView.ClearData();
            weaponPurchasingItemView.enabled = false;
        }

        private void GenerateItems()
        {
            if (_availableItems.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                float healthPercentage = _progressService.Progress.HealthState.CurrentHp / _progressService.Progress.HealthState.MaxHp;

                ShopItemStaticData shopItemStaticData = _staticDataService.ForShopItem(ItemTypeId.HealthRecover);

                if (healthPercentage <= DangerousHealthLevel && _availableItems.Contains(ItemTypeId.HealthRecover) && _money >= shopItemStaticData.Cost)
                {
                    GameObject view = GetRandomShopItem();

                    if (view != null)
                        CreateItemPurchasingItemView(view, ItemTypeId.HealthRecover, true);
                }
            }
        }

        private void GenerateAmmo()
        {
            if (_availableAmmunition.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                GameObject view = GetRandomShopItem();

                if (view != null)
                    CreateAmmoPurchasingItemView(view, _availableAmmunition, true);
            }
        }

        private void GenerateUpgrades()
        {
            if (_availableUpgrades.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                GameObject view = GetRandomShopItem();

                if (view != null)
                    CreateUpgradePurchasingItemView(view, _availableUpgrades, true);
            }
        }

        private void GenerateWeapons()
        {
            if (_availableWeapons.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                GameObject view = GetRandomShopItem();

                if (view != null)
                    CreateWeaponPurchasingItemView(view, _availableWeapons, true);
            }
        }

        private void GeneratePerks()
        {
            if (_availablePerks.Count != 0 && _shopItemsNumbers.Count != 0)
            {
                GameObject view = GetRandomShopItem();

                if (view != null)
                    CreatePerkPurchasingItemView(view, _availablePerks, true);
            }
        }

        private GameObject GetRandomShopItem()
        {
            int i = _randomService.NextNumberFrom(_shopItemsNumbers);
            return _shopItems[i];
        }
    }
}