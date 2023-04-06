using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Data.Perks;
using CodeBase.Data.Upgrades;
using CodeBase.Data.Weapons;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Pool;
using CodeBase.Services.Randomizer;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.StaticData.Items.Shop.Weapons;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Elements.ShopPanel.ViewItems;
using ModestTree;
using UnityEngine;

namespace CodeBase.UI.Elements.ShopPanel
{
    public class ShopItemsGenerator : MonoBehaviour
    {
        private const float DangerousHealthLevel = 0.5f;
        private const string Ammunition = "ammo";
        private const string Weapons = "weapons";
        private const string Perks = "perks";
        private const string Upgrades = "upgrades";
        private const string Items = "items";

        // [SerializeField] private GameObject _shopItem1;
        // [SerializeField] private GameObject _shopItem2;
        // [SerializeField] private GameObject _shopItem3;
        [SerializeField] private GameObject[] _shopItems;
        [SerializeField] private ShopButtons _shopButtons;

        private IPlayerProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IRandomService _randomService;
        private IObjectsPoolService _objectsPoolService;
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
        private bool _isFirst = true;

        private void Update()
        {
            Debug.Log($"money: {_money}");
        }

        public void Construct(IPlayerProgressService progressService, IStaticDataService staticDataService, IRandomService randomService,
            IObjectsPoolService objectsPoolService)
        {
            _progressService = progressService;
            _staticDataService = staticDataService;
            _randomService = randomService;
            _objectsPoolService = objectsPoolService;

            InitializeEmptyData();
            _money = _progressService.Progress.CurrentLevelStats.MoneyData.Money;
        }

        private void InitializeEmptyData()
        {
            _shopItemsNumbers = new HashSet<int>(_shopItems.Length);
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

        private void OnEnable()
        {
            if (_isFirst == false)
                CreateAvailableItems();
        }

        public void CreateAvailableItems()
        {
            InitializeEmptyData();
            CreateNextLevelPerks();
            CreateNextLevelUpgrades();
            CreateAmmunition();
            CreateWeapons();
            CreateItems();

            StartCoroutine(CoroutineShowShopItems());
            _isFirst = false;
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
            SetHighlightingVisibility(false);
            ShowShopItems();
            yield return _delayShopItemsDisplaying;
            ShowShopItems();
            yield return _delayShopItemsDisplaying;

            GenerateItems();
            GenerateAmmo();
            GeneratePerks();
            GenerateUpgrades();
            GenerateWeapons();

            SetHighlightingVisibility(true);
        }

        private void SetHighlightingVisibility(bool isVisible)
        {
            foreach (GameObject shopItem in _shopItems)
                shopItem.GetComponent<ShopItemHighlighter>().SetVisibility(isVisible);
        }

        private void ShowShopItems()
        {
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
                        if (_unavailableAmmunition.IsEmpty())
                            return;

                        CreateAmmoPurchasingItemView(shopItem, _unavailableAmmunition, false);
                        break;
                    }

                    case Weapons:
                    {
                        if (_unavailableWeapons.IsEmpty())
                            return;

                        CreateWeaponPurchasingItemView(shopItem, _unavailableWeapons, false);
                        break;
                    }

                    case Perks:
                    {
                        if (_unavailablePerks.IsEmpty())
                            return;

                        CreatePerkPurchasingItemView(shopItem, _unavailablePerks, false);
                        break;
                    }

                    case Upgrades:
                    {
                        if (_unavailableUpgrades.IsEmpty())
                            return;

                        CreateUpgradePurchasingItemView(shopItem, _unavailableUpgrades, false);
                        break;
                    }

                    case Items:
                    {
                        if (_unavailableItems.IsEmpty())
                            return;

                        CreateItemPurchasingItemView(shopItem, _unavailableItems, false);
                        break;
                    }
                }
            }
        }

        private void CreateItemPurchasingItemView(GameObject parent, List<ItemTypeId> list, bool isClickable)
        {
            ItemTypeId itemTypeId = _randomService.NextFrom(list);
            // GameObject item = _objectsPoolService.GetShopItem(ShopItemTypeIds.Item.ToString());
            // item.transform.SetParent(parent);
            // item.transform.position = parent.transform.position;
            ItemPurchasingItemView view = parent.GetComponentInChildren<ItemPurchasingItemView>();
            // ItemPurchasingItemView view = item.GetComponent<ItemPurchasingItemView>();
            view.Construct(itemTypeId, _progressService);
            view.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        private void CreateItemPurchasingItemView(GameObject parent, ItemTypeId itemTypeId, bool isClickable)
        {
            // GameObject item = _objectsPoolService.GetShopItem(itemTypeId.ToString());
            // item.transform.SetParent(parent);
            // item.transform.position = parent.transform.position;
            ItemPurchasingItemView view = parent.GetComponentInChildren<ItemPurchasingItemView>();
            // ItemPurchasingItemView view = item.GetComponent<ItemPurchasingItemView>();
            view.Construct(itemTypeId, _progressService);
            view.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        private void CreateUpgradePurchasingItemView(GameObject parent, List<UpgradeItemData> list, bool isClickable)
        {
            UpgradeItemData upgradeItemData = _randomService.NextFrom(list);
            // GameObject item = _objectsPoolService.GetShopItem(ShopItemTypeIds.Upgrade.ToString());
            // item.transform.SetParent(parent);
            // item.transform.position = parent.transform.position;
            UpgradePurchasingItemView view = parent.GetComponentInChildren<UpgradePurchasingItemView>();
            // UpgradePurchasingItemView view = item.GetComponent<UpgradePurchasingItemView>();
            view.Construct(upgradeItemData, _progressService);
            view.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        private void CreatePerkPurchasingItemView(GameObject parent, List<PerkItemData> list, bool isClickable)
        {
            PerkItemData perkItemData = _randomService.NextFrom(list);
            // GameObject item = _objectsPoolService.GetShopItem(ShopItemTypeIds.Perk.ToString());
            // item.transform.SetParent(parent);
            // item.transform.position = parent.transform.position;
            PerkPurchasingItemView view = parent.GetComponentInChildren<PerkPurchasingItemView>();
            // PerkPurchasingItemView view = item.GetComponent<PerkPurchasingItemView>();
            view.Construct(perkItemData, _progressService);
            view.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        private void CreateAmmoPurchasingItemView(GameObject parent, List<AmmoItem> list, bool isClickable)
        {
            AmmoItem ammoItem = _randomService.NextFrom(list);
            // GameObject item = _objectsPoolService.GetShopItem(ShopItemTypeIds.Ammo.ToString());
            // item.transform.SetParent(parent);
            // item.transform.position = parent.transform.position;
            AmmoPurchasingItemView view = parent.GetComponentInChildren<AmmoPurchasingItemView>();
            // AmmoPurchasingItemView view = item.GetComponent<AmmoPurchasingItemView>();
            view.Construct(ammoItem, _progressService);
            view.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        private void CreateWeaponPurchasingItemView(GameObject parent, List<HeroWeaponTypeId> list, bool isClickable)
        {
            HeroWeaponTypeId weaponTypeId = _randomService.NextFrom(list);
            // GameObject item = _objectsPoolService.GetShopItem(ShopItemTypeIds.Weapon.ToString());
            // item.transform.SetParent(parent);
            // item.transform.position = parent.transform.position;
            WeaponPurchasingItemView view = parent.GetComponentInChildren<WeaponPurchasingItemView>();
            // WeaponPurchasingItemView view = item.GetComponent<WeaponPurchasingItemView>();
            view.Construct(weaponTypeId, _progressService);
            view.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        private void GenerateItems()
        {
            if (_availableItems.IsEmpty() || _shopItemsNumbers.IsEmpty())
                return;

            float healthPercentage = _progressService.Progress.HealthState.CurrentHp / _progressService.Progress.HealthState.MaxHp;

            ShopItemStaticData shopItemStaticData = _staticDataService.ForShopItem(ItemTypeId.HealthRecover);

            if (healthPercentage <= DangerousHealthLevel && _availableItems.Contains(ItemTypeId.HealthRecover) && _money >= shopItemStaticData.Cost)
            {
                GameObject view = GetRandomShopItem();
                CreateItemPurchasingItemView(view, ItemTypeId.HealthRecover, true);
            }
        }

        private void GenerateAmmo()
        {
            if (_availableAmmunition.IsEmpty() || _shopItemsNumbers.IsEmpty())
                return;

            GameObject view = GetRandomShopItem();
            CreateAmmoPurchasingItemView(view, _availableAmmunition, true);
        }

        private void GenerateUpgrades()
        {
            if (_availableUpgrades.IsEmpty() || _shopItemsNumbers.IsEmpty())
                return;

            GameObject view = GetRandomShopItem();
            CreateUpgradePurchasingItemView(view, _availableUpgrades, true);
        }

        private void GenerateWeapons()
        {
            if (_availableWeapons.IsEmpty() || _shopItemsNumbers.IsEmpty())
                return;

            GameObject view = GetRandomShopItem();
            CreateWeaponPurchasingItemView(view, _availableWeapons, true);
        }

        private void GeneratePerks()
        {
            if (_availablePerks.IsEmpty() || _shopItemsNumbers.IsEmpty())
                return;

            GameObject view = GetRandomShopItem();
            CreatePerkPurchasingItemView(view, _availablePerks, true);
        }

        private GameObject GetRandomShopItem()
        {
            int i = _randomService.NextNumberFrom(_shopItemsNumbers);
            _shopItemsNumbers.Remove(i);
            return _shopItems[i];
        }
    }
}