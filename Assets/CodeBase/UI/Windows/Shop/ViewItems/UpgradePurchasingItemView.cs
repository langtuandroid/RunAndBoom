using CodeBase.Data;
using CodeBase.Data.Upgrades;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop.ViewItems
{
    public class UpgradePurchasingItemView : BaseItemView
    {
        [SerializeField] private Button _button;

        private UpgradeItemData _upgradeItemData;
        private ShopUpgradeStaticData _upgradeStaticData;
        private UpgradableWeaponStaticData _upgradableWeaponStaticData;
        private UpgradeLevelInfoStaticData _upgradeLevelInfoStaticData;
        private ShopUpgradeLevelStaticData _shopUpgradeLevelStaticData;

        // private void OnEnable() =>
        //     _button?.onClick.AddListener(Clicked);
        //
        // private void OnDisable() =>
        //     _button?.onClick.RemoveListener(Clicked);

        public void Construct(UpgradeItemData upgradeItemData, PlayerProgress progress)
        {
            // _button?.onClick.AddListener(Clicked);
            base.Construct(progress);
            _upgradeItemData = upgradeItemData;
            FillData();
        }

        public void ChangeClickability(bool isClickable) =>
            _button.enabled = isClickable;

        protected override void FillData()
        {
            _upgradableWeaponStaticData = StaticDataService.ForUpgradableWeapon(_upgradeItemData.WeaponTypeId);
            _upgradeStaticData = StaticDataService.ForShopUpgrade(_upgradeItemData.UpgradeTypeId);
            _upgradeLevelInfoStaticData =
                StaticDataService.ForUpgradeLevelsInfo(_upgradeItemData.UpgradeTypeId, _upgradeItemData.LevelTypeId);
            _shopUpgradeLevelStaticData = StaticDataService.ForShopUpgradeLevel(_upgradeItemData.LevelTypeId);

            BackgroundIcon.color = Constants.ShopItemUpgrade;
            BackgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            MainIcon.sprite = _upgradableWeaponStaticData.MainImage;
            MainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);

            if (_shopUpgradeLevelStaticData.MainImage != null)
                LevelIcon.sprite = _shopUpgradeLevelStaticData.MainImage;

            LevelIcon.ChangeImageAlpha(_shopUpgradeLevelStaticData.MainImage != null
                ? Constants.AlphaActiveItem
                : Constants.AlphaInactiveItem);

            AdditionalIcon.sprite = _upgradeStaticData.MainImage;
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            CostText.text = $"{_upgradeLevelInfoStaticData.Cost} $";
            // CostText.color = Constants.ShopItemPerk;
            CountText.text = "";
            TitleText.text =
                $"{_upgradeStaticData.IRuTitle} {_shopUpgradeLevelStaticData.Level} {_upgradableWeaponStaticData.IRuTitle}";
        }

        public void Clicked()
        {
            if (IsMoneyEnough(_upgradeLevelInfoStaticData.Cost))
            {
                ReduceMoney(_upgradeLevelInfoStaticData.Cost);
                Progress.WeaponsData.UpgradesData.LevelUp(_upgradableWeaponStaticData.WeaponTypeId,
                    _upgradeStaticData.UpgradeTypeId);
                ClearData();
            }
        }
    }
}