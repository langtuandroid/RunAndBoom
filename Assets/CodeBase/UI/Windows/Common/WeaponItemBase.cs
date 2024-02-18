using CodeBase.Data.Progress;
using CodeBase.StaticData.Items.Shop.Weapons;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Services;

namespace CodeBase.UI.Windows.Common
{
    public abstract class WeaponItemBase : ItemBase
    {
        protected HeroWeaponTypeId _weaponTypeId;
        protected ShopWeaponStaticData _weaponStaticData;

        private void OnEnable() =>
            _button?.onClick.AddListener(Clicked);

        private void OnDisable() =>
            _button?.onClick.RemoveListener(Clicked);

        public void Construct(HeroWeaponTypeId weaponTypeId, ProgressData progressData)
        {
            base.Construct(progressData);
            _weaponTypeId = weaponTypeId;
            FillData();
        }

        protected override void FillData()
        {
            _weaponStaticData = _staticDataService.ForShopWeapon(_weaponTypeId);

            _backgroundIcon.color = Constants.ShopItemWeapon;
            _backgroundIcon.ChangeImageAlpha(Constants.Visible);
            _mainIcon.sprite = _weaponStaticData.MainImage;
            _mainIcon.ChangeImageAlpha(Constants.Visible);
            _levelIcon.ChangeImageAlpha(Constants.Invisible);
            _additionalIcon.ChangeImageAlpha(Constants.Invisible);

            if (_costText != null)
                _costText.text = $"{_weaponStaticData.Cost}";

            // CostText.color = Constants.ShopItemPerk;
            _countText.text = "";
            _titleText.text =
                $"{_localizationService.GetText(russian: _weaponStaticData.RuTitle, turkish: _weaponStaticData.TrTitle, english: _weaponStaticData.EnTitle)}";
        }
    }
}