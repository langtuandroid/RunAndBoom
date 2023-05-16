using CodeBase.Data;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.UI.Services;

namespace CodeBase.UI.Windows.Common
{
    public abstract class AmmoItemBase : ItemBase
    {
        private AmmoCountType _countType;
        protected AmmoItem _ammoItem;
        protected ShopAmmoStaticData _shopAmmoStaticData;

        private void OnEnable() =>
            Button?.onClick.AddListener(Clicked);

        private void OnDisable() =>
            Button?.onClick.RemoveListener(Clicked);

        public void Construct(AmmoItem ammoItem, PlayerProgress progress)
        {
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

            if (CostText != null)
                CostText.text = $"{_shopAmmoStaticData.Cost} $";

            // CostText.color = Constants.ShopItemPerk;
            CountText.text = $"{_shopAmmoStaticData.Count}";
            // CountText.color = Constants.ShopItemCountField;
            TitleText.text = $"{_shopAmmoStaticData.RuTitle}";
        }
    }
}