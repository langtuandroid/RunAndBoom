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
        private const float DangerousHealthLevel = 0.5f;

        // [SerializeField] private GameObject _shopItem1;
        // [SerializeField] private GameObject _shopItem2;
        // [SerializeField] private GameObject _shopItem3;
        [SerializeField] private Transform[] _shopItems;
        [SerializeField] private ShopButtons _shopButtons;
        [SerializeField] private ItemPurchasingItemView _itemView;
        [SerializeField] private AmmoPurchasingItemView _ammoView;
        [SerializeField] private WeaponPurchasingItemView _weaponView;
        [SerializeField] private UpgradePurchasingItemView _upgradeView;
        [SerializeField] private PerkPurchasingItemView _perkView;

        private IPlayerProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IRandomService _randomService;
        private HashSet<int> _shopItemsNumbers;

        private List<AmmoItem> _availableAmmo;
        private List<ItemTypeId> _availableItems;
        private List<UpgradeItemData> _availableUpgrades;
        private List<HeroWeaponTypeId> _availableWeapons;
        private List<PerkTypeId> _availablePerks;
        private int _money;

        private void Update()
        {
            Debug.Log($"money: {_money}");
        }

        public void Construct(IPlayerProgressService progressService, IStaticDataService staticDataService, IRandomService randomService)
        {
            _progressService = progressService;
            _staticDataService = staticDataService;
            _randomService = randomService;
            _shopItemsNumbers = new HashSet<int>(_shopItems.Length);
            _availableAmmo = new List<AmmoItem>();
            _availableItems = new List<ItemTypeId>();
            _availableUpgrades = new List<UpgradeItemData>();
            _availableWeapons = new List<HeroWeaponTypeId>();
            _availablePerks = new List<PerkTypeId>();
            _money = _progressService.Progress.CurrentLevelStats.MoneyData.Money;
        }

        private void OnEnable() =>
            AddAllAvailableItems();

        private void AddAllAvailableItems()
        {
            FillNextLevelPerks();
            FillNextLevelUpgrades();
            FillAmmunition();
            FillWeapons();
            FillItems();
        }

        private void FillNextLevelUpgrades()
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
                            UpgradableWeaponStaticData upgradableWeaponStaticData = _staticDataService.ForUpgradableWeapon(nextLevelUpgrade.WeaponTypeId);
                            ShopUpgradeStaticData shopUpgradeStaticData = _staticDataService.ForShopUpgrade(nextLevelUpgrade.UpgradeTypeId);
                            UpgradeLevelInfoStaticData upgradeLevelInfoStaticData =
                                _staticDataService.ForUpgradeLevelsInfo(nextLevelUpgrade.UpgradeTypeId, nextLevelUpgrade.LevelTypeId);
                            ShopUpgradeLevelStaticData shopUpgradeLevelStaticData = _staticDataService.ForShopUpgradeLevel(nextLevelUpgrade.LevelTypeId);

                            WeaponUpgrade weaponUpgrade = new WeaponUpgrade(upgradeItemData.WeaponTypeId, upgradeItemData.UpgradeTypeId,
                                nextLevelUpgrade.LevelTypeId);

                            if (_money >= upgradeLevelInfoStaticData.Cost)
                                _availableUpgrades.Add(upgradeItemData);
                        }
                    }
                }
            }
        }

        private void FillNextLevelPerks()
        {
            foreach (PerkTypeId perkTypeId in DataExtensions.GetValues<PerkTypeId>())
            {
                PerkItemData nextLevelPerk = _progressService.Progress.PerksData.GetNextLevelPerk(perkTypeId);

                if (nextLevelPerk.LevelTypeId != LevelTypeId.None)
                {
                    PerkStaticData perkStaticData = _staticDataService.ForPerk(nextLevelPerk.PerkTypeId, nextLevelPerk.LevelTypeId);

                    if (_money >= perkStaticData.Cost)
                        _availablePerks.Add(perkTypeId);
                }
            }
        }

        private void FillAmmunition()
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
                        }

                        break;
                }
            }
        }

        private void AddAmmo(HeroWeaponTypeId typeId, AmmoCountType ammoCount)
        {
            ShopAmmoStaticData shopAmmoStaticData = _staticDataService.ForShopAmmo(typeId, ammoCount);

            if (_money >= shopAmmoStaticData.Cost)
            {
                AmmoItem ammoItem = new AmmoItem(typeId, ammoCount);
                _availableAmmo.Add(ammoItem);
            }
        }

        private void FillWeapons()
        {
            foreach (WeaponData weaponData in _progressService.Progress.WeaponsData.WeaponDatas)
            {
                if (weaponData.IsAvailable)
                {
                    ShopWeaponStaticData weaponStaticData = _staticDataService.ForShopWeapon(weaponData.WeaponTypeId);

                    if (_money >= weaponStaticData.Cost)
                        _availableWeapons.Add(weaponData.WeaponTypeId);
                }
            }
        }

        private void FillItems()
        {
            foreach (ItemTypeId itemTypeId in DataExtensions.GetValues<ItemTypeId>())
            {
                ShopItemStaticData itemStaticData = _staticDataService.ForShopItem(itemTypeId);

                if (_money >= itemStaticData.Cost)
                    _availableItems.Add(itemTypeId);
            }
        }

        public void GenerateItems()
        {
            if (_availableAmmo.Count != 0)
            {
                float healthPercentage = _progressService.Progress.HealthState.CurrentHp / _progressService.Progress.HealthState.MaxHp;

                ShopItemStaticData shopItemStaticData = _staticDataService.ForShopItem(ItemTypeId.HealthRecover);

                if (healthPercentage <= DangerousHealthLevel && _availableItems.Contains(ItemTypeId.HealthRecover) && _money >= shopItemStaticData.Cost)
                {
                    Transform view = GetRandomShopItem();
                    ItemPurchasingItemView itemView = Instantiate(_itemView, view);
                    itemView.Construct(shopItemStaticData, _progressService);
                }
            }
        }

        public void GenerateAmmo()
        {
            if (_availableAmmo.Count != 0)
            {
                AmmoItem ammoItem = _randomService.NextFrom(_availableAmmo);
                Transform view = GetRandomShopItem();
                AmmoPurchasingItemView ammoView = Instantiate(_ammoView, view);
                ammoView.Construct(ammoItem, _progressService);
            }
        }

        public void GenerateUpgrades()
        {
            if (_availableUpgrades.Count != 0)
            {
                UpgradeItemData upgradeItemData = _randomService.NextFrom(_availableUpgrades);
                Transform view = GetRandomShopItem();
                UpgradePurchasingItemView ammoView = Instantiate(_upgradeView, view);
                ammoView.Construct(upgradeItemData, _progressService);
            }
        }

        public void GenerateWeapons()
        {
            if (_availableWeapons.Count != 0)
            {
                HeroWeaponTypeId weaponTypeId = _randomService.NextFrom(_availableWeapons);
                Transform view = GetRandomShopItem();
                WeaponPurchasingItemView ammoView = Instantiate(_weaponView, view);
                ammoView.Construct(weaponTypeId, _progressService);
            }
        }

        private Transform GetRandomShopItem()
        {
            int i = _randomService.NextNumberFrom(_shopItemsNumbers);
            return _shopItems[i];
        }
    }
}