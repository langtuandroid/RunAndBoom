using CodeBase.Data;
using CodeBase.Data.Upgrades;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.UI.Services;

namespace CodeBase.UI.Windows.Common
{
    public abstract class UpgradeItemBase : ItemBase
    {
        private UpgradeItemData _upgradeItemData;
        protected ShopUpgradeStaticData _upgradeStaticData;
        protected UpgradableWeaponStaticData _upgradableWeaponStaticData;
        protected UpgradeLevelInfoStaticData _upgradeLevelInfoStaticData;
        private ShopUpgradeLevelStaticData _shopUpgradeLevelStaticData;

        private void OnEnable() =>
            Button?.onClick.AddListener(Clicked);

        private void OnDisable() =>
            Button?.onClick.RemoveListener(Clicked);

        public void Construct(UpgradeItemData upgradeItemData, PlayerProgress progress)
        {
            Button?.onClick.AddListener(Clicked);
            base.Construct(progress);
            _upgradeItemData = upgradeItemData;
            FillData();
        }

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
                $"{_upgradeStaticData.RuTitle} {_shopUpgradeLevelStaticData.Level} {_upgradableWeaponStaticData.RuTitle}";
        }
    }
}