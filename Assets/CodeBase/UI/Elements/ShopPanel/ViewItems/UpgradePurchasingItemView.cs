using System;
using CodeBase.Data.Upgrades;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class UpgradePurchasingItemView : BasePurchasingItemView
    {
        [SerializeField] private Button _button;

        private UpgradeItemData _upgradeItemData;
        private ShopUpgradeStaticData _upgradeStaticData;
        private UpgradableWeaponStaticData _upgradableWeaponStaticData;
        private UpgradeLevelInfoStaticData _upgradeLevelInfoStaticData;
        private ShopUpgradeLevelStaticData _shopUpgradeLevelStaticData;

        public override event Action ShopItemClicked;

        public void Construct(UpgradeItemData upgradeItemData, IPlayerProgressService playerProgressService)
        {
            // Button = _button;
            _button.onClick.AddListener(Clicked);
            base.Construct(playerProgressService);
            _upgradeItemData = upgradeItemData;
            FillData();
        }

        public void ChangeClickability(bool isClickable) =>
            _button.interactable = isClickable;

        protected override void FillData()
        {
            BackgroundIcon.color = Constants.ShopItemUpgrade;
            BackgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            _upgradableWeaponStaticData = StaticDataService.ForUpgradableWeapon(_upgradeItemData.WeaponTypeId);
            _upgradeStaticData = StaticDataService.ForShopUpgrade(_upgradeItemData.UpgradeTypeId);
            _upgradeLevelInfoStaticData = StaticDataService.ForUpgradeLevelsInfo(_upgradeItemData.UpgradeTypeId, _upgradeItemData.LevelTypeId);
            _shopUpgradeLevelStaticData = StaticDataService.ForShopUpgradeLevel(_upgradeItemData.LevelTypeId);

            MainIcon.sprite = _upgradableWeaponStaticData.MainImage;
            MainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);

            if (_shopUpgradeLevelStaticData.MainImage != null)
                LevelIcon.sprite = _shopUpgradeLevelStaticData.MainImage;

            AdditionalIcon.sprite = _upgradeStaticData.MainImage;
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            CostText.text = $"{_upgradeLevelInfoStaticData.Cost} $";
            // CostText.color = Constants.ShopItemPerk;
            CountText.text = "";
            TitleText.text = $"{_upgradeStaticData.IRuTitle} {_shopUpgradeLevelStaticData.Level} {_upgradableWeaponStaticData.IRuTitle}";
        }

        private void Clicked()
        {
            if (IsMoneyEnough(_upgradeLevelInfoStaticData.Cost))
            {
                ReduceMoney(_upgradeLevelInfoStaticData.Cost);
                Progress.WeaponsData.WeaponUpgradesData.LevelUp(_upgradableWeaponStaticData.WeaponTypeId, _upgradeStaticData.UpgradeTypeId);
                ShopItemClicked?.Invoke();
            }

            ClearData();
        }
    }
}