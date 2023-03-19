using System;
using CodeBase.StaticData.Items.Shop.Weapons;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Services;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class ShopWeaponView : BaseShopView, IShopItem
    {
        private HeroWeaponTypeId _weaponTypeId;
        private ShopWeaponStaticData _weaponStaticData;

        public event Action ShopItemClicked;

        public void Construct(HeroWeaponTypeId weaponTypeId)
        {
            base.Construct();
            _weaponTypeId = weaponTypeId;
            FillData();
        }

        protected override void FillData()
        {
            _weaponStaticData = StaticDataService.ForShopWeapon(_weaponTypeId);

            MainIcon.sprite = _weaponStaticData.MainImage;
            LevelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            CostText.text = $"{_weaponStaticData.Cost} $";
            CostText.color = Constants.ShopItemPerk;
            CountText.text = "";
            TitleText.text = $"{_weaponStaticData.IRuTitle}";
        }

        protected override void Clicked()
        {
            if (IsMoneyEnough(_weaponStaticData.Cost))
            {
                ReduceMoney(_weaponStaticData.Cost);
                Progress.WeaponsData.SetAvailableWeapon(_weaponTypeId);
                ShopItemClicked?.Invoke();
            }
        }
    }
}