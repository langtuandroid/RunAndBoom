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
            Button?.onClick.AddListener(Clicked);

        private void OnDisable() =>
            Button?.onClick.RemoveListener(Clicked);

        public void Construct(HeroWeaponTypeId weaponTypeId, ProgressData progressData)
        {
            base.Construct(progressData);
            _weaponTypeId = weaponTypeId;
            FillData();
        }

        protected override void FillData()
        {
            _weaponStaticData = StaticDataService.ForShopWeapon(_weaponTypeId);

            BackgroundIcon.color = Constants.ShopItemWeapon;
            BackgroundIcon.ChangeImageAlpha(Constants.Visible);
            MainIcon.sprite = _weaponStaticData.MainImage;
            MainIcon.ChangeImageAlpha(Constants.Visible);
            LevelIcon.ChangeImageAlpha(Constants.Invisible);
            AdditionalIcon.ChangeImageAlpha(Constants.Invisible);

            if (CostText != null)
                CostText.text = $"{_weaponStaticData.Cost}";

            // CostText.color = Constants.ShopItemPerk;
            CountText.text = "";
            TitleText.text =
                $"{LocalizationService.GetText(russian: _weaponStaticData.RuTitle, turkish: _weaponStaticData.TrTitle, english: _weaponStaticData.EnTitle)}";
        }
    }
}