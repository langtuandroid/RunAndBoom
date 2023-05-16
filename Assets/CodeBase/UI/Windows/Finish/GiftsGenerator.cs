using System;
using System.Collections.Generic;
using CodeBase.Data.Perks;
using CodeBase.Data.Upgrades;
using CodeBase.Hero;
using CodeBase.StaticData.Items.Gifts;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Windows.Common;
using CodeBase.UI.Windows.Finish.Items;
using CodeBase.UI.Windows.Shop;
using UnityEngine;

namespace CodeBase.UI.Windows.Finish
{
    public class GiftsGenerator : ItemsGeneratorBase
    {
        public override event Action GenerationStarted;
        public override event Action GenerationEnded;

        public void Construct(HeroHealth health)
        {
            base.Construct(health);
        }

        public void SetMaxPrice(int maxPrice) =>
            Money = maxPrice;

        public override void Generate()
        {
            GenerationStarted?.Invoke();
            SetHighlightingVisibility(false);
            InitializeEmptyData();
            CreateAllItems();
            GenerateAllItems();
            SetHighlightingVisibility(true);
            GenerationEnded?.Invoke();
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
            GenerateItems();
            GenerateAmmo();
            GeneratePerks();
            GenerateUpgrades();
            GenerateWeapons();
            GenerateMoney();
        }

        protected override void CreateAmmoItem(GameObject parent, List<AmmoItem> list, bool isClickable)
        {
            AmmoItem ammoItem = RandomService.NextFrom(list);
            AmmoGiftItem view = parent.GetComponent<ShopCell>().GetView(typeof(AmmoGiftItem)) as AmmoGiftItem;
            view?.Construct(ammoItem, Progress, this);
            view?.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        protected override void CreateItemItem(GameObject parent, ItemTypeId itemTypeId, bool isClickable)
        {
            ItemGiftItem view =
                parent.GetComponent<ShopCell>().GetView(typeof(ItemGiftItem)) as ItemGiftItem;
            view?.Construct(itemTypeId, Progress, Health, this);
            view?.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        protected override void CreateUpgradeItem(GameObject parent, List<UpgradeItemData> list, bool isClickable)
        {
            UpgradeItemData upgradeItemData = RandomService.NextFrom(list);
            UpgradeGiftItem view = parent.GetComponent<ShopCell>().GetView(typeof(UpgradeGiftItem)) as UpgradeGiftItem;
            view?.Construct(upgradeItemData, Progress, this);
            view?.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        protected override void CreatePerkItem(GameObject parent, List<PerkItemData> list, bool isClickable)
        {
            PerkItemData perkItemData = RandomService.NextFrom(list);
            PerkGiftItem view = parent.GetComponent<ShopCell>().GetView(typeof(PerkGiftItem)) as PerkGiftItem;
            view?.Construct(perkItemData, Progress, this);
            view?.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        protected override void CreateWeaponItem(GameObject parent, List<HeroWeaponTypeId> list, bool isClickable)
        {
            HeroWeaponTypeId weaponTypeId = RandomService.NextFrom(list);
            WeaponGiftItem view = parent.GetComponent<ShopCell>().GetView(typeof(WeaponGiftItem)) as WeaponGiftItem;
            view?.Construct(weaponTypeId, Progress, this);
            view?.ChangeClickability(isClickable);
            parent.SetActive(true);
        }

        protected override void CreateMoneyItem(GameObject parent, List<MoneyTypeId> list, bool isClickable)
        {
            MoneyTypeId moneyTypeId = RandomService.NextFrom(list);
            MoneyGiftItem view = parent.GetComponent<ShopCell>().GetView(typeof(MoneyGiftItem)) as MoneyGiftItem;
            view?.Construct(moneyTypeId, Progress, this);
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