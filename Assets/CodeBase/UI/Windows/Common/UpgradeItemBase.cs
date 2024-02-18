using CodeBase.Data.Progress;
using CodeBase.Data.Progress.Upgrades;
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
            _button?.onClick.AddListener(Clicked);

        private void OnDisable() =>
            _button?.onClick.RemoveListener(Clicked);

        public void Construct(UpgradeItemData upgradeItemData, ProgressData progressData)
        {
            base.Construct(progressData);
            _upgradeItemData = upgradeItemData;
            FillData();
        }

        protected override void FillData()
        {
            _upgradableWeaponStaticData = _staticDataService.ForUpgradableWeapon(_upgradeItemData.WeaponTypeId);
            _upgradeStaticData = _staticDataService.ForShopUpgrade(_upgradeItemData.UpgradeTypeId);
            _upgradeLevelInfoStaticData =
                _staticDataService.ForUpgradeLevelsInfo(_upgradeItemData.UpgradeTypeId, _upgradeItemData.LevelTypeId);
            _shopUpgradeLevelStaticData = _staticDataService.ForShopUpgradeLevel(_upgradeItemData.LevelTypeId);

            _backgroundIcon.color = Constants.ShopItemUpgrade;
            _backgroundIcon.ChangeImageAlpha(Constants.Visible);
            _mainIcon.sprite = _upgradableWeaponStaticData.MainImage;
            _mainIcon.ChangeImageAlpha(Constants.Visible);

            if (_shopUpgradeLevelStaticData.MainImage != null)
                _levelIcon.sprite = _shopUpgradeLevelStaticData.MainImage;

            _levelIcon.ChangeImageAlpha(_shopUpgradeLevelStaticData.MainImage != null
                ? Constants.Visible
                : Constants.Invisible);

            _additionalIcon.sprite = _upgradeStaticData.MainImage;
            _additionalIcon.ChangeImageAlpha(Constants.Visible);

            if (_costText != null)
                _costText.text = $"{_upgradeLevelInfoStaticData.Cost}";

            // CostText.color = Constants.ShopItemPerk;
            _countText.text = "";
            _titleText.text =
                $"{_localizationService.GetText(russian: _upgradeStaticData.RuTitle, turkish: _upgradeStaticData.TrTitle, english: _upgradeStaticData.EnTitle)} {_shopUpgradeLevelStaticData.Level} {_localizationService.GetText(russian: _upgradableWeaponStaticData.RuTitle, turkish: _upgradableWeaponStaticData.TrTitle, english: _upgradableWeaponStaticData.EnTitle)}";
        }
    }
}