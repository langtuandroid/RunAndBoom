using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Data.Perks;
using CodeBase.Data.Upgrades;
using CodeBase.Data.Weapons;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.StaticData.Items.Shop.Weapons;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Elements.ShopPanel.ViewModels;
using UnityEngine;

namespace CodeBase.UI.Elements.ShopPanel
{
    public class ShopItemsGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _item1;
        [SerializeField] private GameObject _item2;
        [SerializeField] private GameObject _item3;

        private IPlayerProgressService _progressService;
        private IStaticDataService _staticDataService;
        private HashSet<AmmoViewModel> _ammo;
        private HashSet<PerkViewModel> _perks;
        private HashSet<WeaponViewModel> _weapons;
        private HashSet<ItemViewModel> _items;
        private HashSet<UpgradeViewModel> _upgrades;

        public void Construct(IPlayerProgressService progressService, IStaticDataService staticDataService)
        {
            _progressService = progressService;
            _staticDataService = staticDataService;
            Initialize();
        }

        private void Initialize()
        {
            AddAllAvailableItems();
        }

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
            foreach (HeroWeaponTypeId weaponTypeId in DataExtensions.GetValues<HeroWeaponTypeId>())
            {
                foreach (UpgradeTypeId upgradeTypeId in DataExtensions.GetValues<UpgradeTypeId>())
                {
                    UpgradeItemData nextLevelUpgrade = _progressService.Progress.WeaponsData.WeaponUpgradesData.GetNextLevelPerk(weaponTypeId, upgradeTypeId);

                    if (nextLevelUpgrade.LevelTypeId != LevelTypeId.None)
                    {
                        UpgradableWeaponStaticData upgradableWeaponStaticData = _staticDataService.ForUpgradableWeapon(nextLevelUpgrade.WeaponTypeId);
                        ShopUpgradeStaticData shopUpgradeStaticData = _staticDataService.ForShopUpgrade(nextLevelUpgrade.UpgradeTypeId);
                        UpgradeLevelInfoStaticData upgradeLevelInfoStaticData =
                            _staticDataService.ForUpgradeLevelsInfo(nextLevelUpgrade.UpgradeTypeId, nextLevelUpgrade.LevelTypeId);
                        ShopUpgradeLevelStaticData shopUpgradeLevelStaticData = _staticDataService.ForShopUpgradeLevel(nextLevelUpgrade.LevelTypeId);

                        UpgradeViewModel upgradeViewModel = new UpgradeViewModel(mainImage: upgradableWeaponStaticData.MainImage,
                            levelImage: shopUpgradeLevelStaticData.MainImage, additionalImage: shopUpgradeStaticData.MainImage,
                            cost: upgradeLevelInfoStaticData.Cost,
                            ruTitle: $"{shopUpgradeStaticData.IRuTitle} {shopUpgradeLevelStaticData.Level} {upgradableWeaponStaticData.IRuTitle}",
                            enTitle: $"{shopUpgradeStaticData.IEnTitle} {shopUpgradeLevelStaticData.Level} {upgradableWeaponStaticData.IEnTitle}",
                            trTitle: $"{shopUpgradeStaticData.ITrTitle} {shopUpgradeLevelStaticData.Level} {upgradableWeaponStaticData.ITrTitle}");

                        _upgrades.Add(upgradeViewModel);
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

                    PerkViewModel perkViewModel = new PerkViewModel(mainImage: perkStaticData.MainImage, levelImage: perkStaticData.LevelImage,
                        cost: perkStaticData.Cost, ruTitle: perkStaticData.IRuTitle, enTitle: perkStaticData.IEnTitle, trTitle: perkStaticData.ITrTitle);

                    _perks.Add(perkViewModel);
                }
            }
        }

        private void FillAmmunition()
        {
            foreach (HeroWeaponTypeId weaponTypeId in DataExtensions.GetValues<HeroWeaponTypeId>())
            {
                foreach (AmmoCountType ammoCountType in DataExtensions.GetValues<AmmoCountType>())
                {
                    AmmoItem ammoItem = new AmmoItem(weaponTypeId, ammoCountType);
                    ShopAmmoStaticData ammoStaticData = _staticDataService.ForShopAmmo(ammoItem.WeaponTypeId, ammoItem.CountType);

                    int.TryParse(ammoStaticData.Count.ToString(), out int count);
                    AmmoViewModel ammoViewModel = new AmmoViewModel(mainImage: ammoStaticData.MainImage, cost: ammoStaticData.Cost,
                        count: count, ruTitle: ammoStaticData.IRuTitle, enTitle: ammoStaticData.IEnTitle,
                        trTitle: ammoStaticData.ITrTitle);

                    _ammo.Add(ammoViewModel);
                }
            }
        }

        private void FillWeapons()
        {
            foreach (WeaponData weaponData in _progressService.Progress.WeaponsData.WeaponDatas)
            {
                if (!weaponData.IsAvailable)
                {
                    ShopWeaponStaticData weaponStaticData = _staticDataService.ForShopWeapon(weaponData.WeaponTypeId);
                    WeaponViewModel weaponViewModel = new WeaponViewModel(mainImage: weaponStaticData.MainImage, cost: weaponStaticData.Cost,
                        ruTitle: weaponStaticData.IRuTitle, enTitle: weaponStaticData.IEnTitle, trTitle: weaponStaticData.ITrTitle);

                    _weapons.Add(weaponViewModel);
                }
            }
        }

        private void FillItems()
        {
            foreach (ItemTypeId itemTypeId in DataExtensions.GetValues<ItemTypeId>())
            {
                ShopItemStaticData itemStaticData = _staticDataService.ForShopItem(itemTypeId);

                ItemViewModel itemViewModel = new ItemViewModel(mainImage: itemStaticData.MainImage, cost: itemStaticData.Cost,
                    ruTitle: itemStaticData.IRuTitle, enTitle: itemStaticData.IEnTitle, trTitle: itemStaticData.ITrTitle);

                _items.Add(itemViewModel);
            }
        }

        public void GenerateItems()
        {
        }
    }
}