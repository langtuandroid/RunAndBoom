using CodeBase.Data;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class AmmoPurchasingItemView : BaseItemView
    {
        [SerializeField] private Button _button;

        private AmmoCountType _countType;
        private AmmoItem _ammoItem;
        private ShopAmmoStaticData _shopAmmoStaticData;

        // private void OnEnable() =>
        //     _button?.onClick.AddListener(Clicked);
        //
        // private void OnDisable() =>
        //     _button?.onClick.RemoveListener(Clicked);

        public void Construct(AmmoItem ammoItem, PlayerProgress progress)
        {
            // _button?.onClick.AddListener(Clicked);
            base.Construct(progress);
            _ammoItem = ammoItem;
            FillData();
        }

        public void ChangeClickability(bool isClickable) =>
            _button.interactable = isClickable;

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

        public void Clicked()
        {
            if (IsMoneyEnough(_shopAmmoStaticData.Cost))
            {
                ReduceMoney(_shopAmmoStaticData.Cost);
                int value = _shopAmmoStaticData.Count.GetHashCode();
                Progress.WeaponsData.WeaponsAmmoData.AddAmmo(_ammoItem.WeaponTypeId, value);
                ClearData();
            }
        }
    }
}