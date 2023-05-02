using CodeBase.Data;
using CodeBase.StaticData.Items.Shop.Weapons;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Services;

namespace CodeBase.UI.Windows.Common
{
    public abstract class WeaponItemBase : ItemBase
    {
        protected HeroWeaponTypeId _weaponTypeId;
        protected ShopWeaponStaticData _weaponStaticData;

        // private void OnEnable() =>
        //     _button?.onClick.AddListener(Clicked);
        //
        // private void OnDisable() =>
        //     _button?.onClick.RemoveListener(Clicked);

        public void Construct(HeroWeaponTypeId weaponTypeId, PlayerProgress progress)
        {
            // _button?.onClick.AddListener(Clicked);
            base.Construct(progress);
            _weaponTypeId = weaponTypeId;
            FillData();
        }

        protected override void FillData()
        {
            _weaponStaticData = StaticDataService.ForShopWeapon(_weaponTypeId);

            BackgroundIcon.color = Constants.ShopItemWeapon;
            BackgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            MainIcon.sprite = _weaponStaticData.MainImage;
            MainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            LevelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            CostText.text = $"{_weaponStaticData.Cost} $";
            // CostText.color = Constants.ShopItemPerk;
            CountText.text = "";
            TitleText.text = $"{_weaponStaticData.RuTitle}";
        }
    }
}