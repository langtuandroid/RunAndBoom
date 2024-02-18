using System.Collections.Generic;
using CodeBase.Data.Progress.Perks;
using CodeBase.Data.Progress.Upgrades;
using CodeBase.StaticData.Items.Gifts;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Windows.Common;
using CodeBase.UI.Windows.Gifts.Items;
using CodeBase.UI.Windows.Shop;
using UnityEngine;

namespace CodeBase.UI.Windows.Gifts
{
    public class GiftsGenerator : ItemsGeneratorBase
    {
        public new void Construct(GameObject hero) =>
            base.Construct(hero);

        public void SetMaxPrice(int maxPrice) =>
            _money = maxPrice;

        public override void Generate()
        {
            if (_progressData == null)
                return;

            SetHighlightingVisibility(false);
            GetMoney();
            InitializeEmptyData();
            CreateAllItems();
            GenerateAllItems();
            SetHighlightingVisibility(true);
            PlaySound();
        }

        protected override void CreateAllItems()
        {
            CreateNextLevelPerks();
            CreateNextLevelUpgrades();
            CreateAmmunition();
            CreateWeapons();
            CreateItems();
            CreateMoney();
        }

        protected override void GenerateAllItems()
        {
            if (_progressData.IsAsianMode)
            {
                GenerateItems();
                GenerateMoney();
                GenerateAmmo();
                GenerateWeapons();
                GeneratePerks();
                GenerateUpgrades();
            }
            else
            {
                GenerateItems();
                GenerateAmmo();

                if (_progressData.AllStats.AllMoney.Money < Constants.MinMoneyForGenerator)
                    GenerateMoney();

                GeneratePerks();
                GenerateUpgrades();
            }
        }

        protected override void CreateAmmoItem(GameObject hero, GameObject parent, List<AmmoItem> list,
            bool isClickable)
        {
            AmmoItem ammoItem = _randomService.NextFrom(list);
            AmmoGiftItem view = parent.GetComponent<ShopCell>().GetView(typeof(AmmoGiftItem)) as AmmoGiftItem;
            view?.Construct(hero.transform, ammoItem, _progressData, this);
            view?.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        protected override void CreateItemItem(GameObject hero, GameObject parent, ItemTypeId itemTypeId,
            bool isClickable)
        {
            ItemGiftItem view =
                parent.GetComponent<ShopCell>().GetView(typeof(ItemGiftItem)) as ItemGiftItem;
            view?.Construct(hero.transform, itemTypeId, _progressData, _health, this);
            view?.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        protected override void CreateUpgradeItem(GameObject hero, GameObject parent, List<UpgradeItemData> list,
            bool isClickable)
        {
            UpgradeItemData upgradeItemData = _randomService.NextFrom(list);
            UpgradeGiftItem view = parent.GetComponent<ShopCell>().GetView(typeof(UpgradeGiftItem)) as UpgradeGiftItem;
            view?.Construct(hero.transform, upgradeItemData, _progressData, this);
            view?.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        protected override void CreatePerkItem(GameObject hero, GameObject parent, List<PerkItemData> list,
            bool isClickable)
        {
            PerkItemData perkItemData = _randomService.NextFrom(list);
            PerkGiftItem view = parent.GetComponent<ShopCell>().GetView(typeof(PerkGiftItem)) as PerkGiftItem;
            view?.Construct(hero.transform, perkItemData, _progressData, this);
            view?.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        protected override void CreateWeaponItem(GameObject hero, GameObject parent, List<HeroWeaponTypeId> list,
            bool isClickable)
        {
            HeroWeaponTypeId weaponTypeId = _randomService.NextFrom(list);
            WeaponGiftItem view = parent.GetComponent<ShopCell>().GetView(typeof(WeaponGiftItem)) as WeaponGiftItem;
            view?.Construct(hero.transform, weaponTypeId, _progressData, this);
            view?.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        protected override void CreateMoneyItem(GameObject hero, GameObject parent, List<MoneyTypeId> list,
            bool isClickable)
        {
            MoneyTypeId moneyTypeId = _randomService.NextFrom(list);
            MoneyGiftItem view = parent.GetComponent<ShopCell>().GetView(typeof(MoneyGiftItem)) as MoneyGiftItem;
            view?.Construct(hero.transform, moneyTypeId, _progressData, this);
            view?.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        public void Clicked()
        {
            foreach (GameObject item in GameObjectItems)
                item.SetActive(false);
        }
    }
}