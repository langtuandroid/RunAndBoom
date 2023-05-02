using CodeBase.Data;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.UI.Services;
using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Shop.ViewItems
{
    public abstract class AmmoItemBase : ShopItemBase
    {
        private AmmoCountType _countType;
        protected StaticData.Items.Shop.Ammo.AmmoItem _ammoItem;
        protected ShopAmmoStaticData _shopAmmoStaticData;
        
        // private void OnEnable() =>
        //     _button?.onClick.AddListener(Clicked);
        //
        // private void OnDisable() =>
        //     _button?.onClick.RemoveListener(Clicked);

        public void Construct(StaticData.Items.Shop.Ammo.AmmoItem ammoItem, PlayerProgress progress)
        {
            // _button?.onClick.AddListener(Clicked);
            base.Construct(progress);
            _ammoItem = ammoItem;
            FillData();
        }

        protected override void FillData()
        {
            _shopAmmoStaticData = StaticDataService.ForShopAmmo(_ammoItem.WeaponTypeId, _ammoItem.CountType);

            BackgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            BackgroundIcon.color = Constants.ShopItemAmmo;
            MainIcon.sprite = _shopAmmoStaticData.MainImage;
            MainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            LevelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            CostText.text = $"{_shopAmmoStaticData.Cost} $";
            // CostText.color = Constants.ShopItemPerk;
            int ammoCountType = (int)_shopAmmoStaticData.Count;
            CountText.text = $"{ammoCountType}";
            // CountText.color = Constants.ShopItemCountField;
            TitleText.text = $"{_shopAmmoStaticData.IRuTitle}";
        }
    }
}