using System;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.UI.Services;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class AmmoPurchasingItemView : BasePurchasingItemView
    {
        private AmmoCountType _countType;
        private AmmoItem _ammoItem;
        private ShopAmmoStaticData _shopAmmoStaticData;

        public override event Action ShopItemClicked;

        public void Construct(AmmoItem ammoItem, IPlayerProgressService playerProgressService)
        {
            base.Construct(playerProgressService);
            _ammoItem = ammoItem;
            FillData();
        }

        protected override void FillData()
        {
            _shopAmmoStaticData = StaticDataService.ForShopAmmo(_ammoItem.WeaponTypeId, _ammoItem.CountType);

            MainIcon.sprite = _shopAmmoStaticData.MainImage;
            LevelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            CostText.text = $"{_shopAmmoStaticData.Cost} $";
            CostText.color = Constants.ShopItemPerk;
            CountText.text = $"{_shopAmmoStaticData.Count}";
            CountText.color = Constants.ShopItemCountField;
            TitleText.text = $"{_shopAmmoStaticData.IRuTitle}";
        }

        protected override void Clicked()
        {
            if (IsMoneyEnough(_shopAmmoStaticData.Cost))
            {
                ReduceMoney(_shopAmmoStaticData.Cost);
                int.TryParse(_shopAmmoStaticData.Count.ToString(), out int count);
                Progress.WeaponsData.WeaponsAmmoData.AddAmmo(_ammoItem.WeaponTypeId, count);
                ShopItemClicked?.Invoke();
            }
        }
    }
}