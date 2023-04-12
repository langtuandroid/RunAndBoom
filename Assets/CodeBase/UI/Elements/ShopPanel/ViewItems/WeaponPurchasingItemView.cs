using System;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Items.Shop.Weapons;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class WeaponPurchasingItemView : BasePurchasingItemView
    {
        [SerializeField] private Button _button;

        private HeroWeaponTypeId _weaponTypeId;
        private ShopWeaponStaticData _weaponStaticData;

        public override event Action ShopItemClicked;

        public void Construct(HeroWeaponTypeId weaponTypeId, IPlayerProgressService playerProgressService)
        {
            // Button = _button;
            _button.onClick.AddListener(Clicked);
            base.Construct(playerProgressService);
            _weaponTypeId = weaponTypeId;
            FillData();
        }

        public void ChangeClickability(bool isClickable) =>
            _button.interactable = isClickable;

        protected override void FillData()
        {
            BackgroundIcon.color = Constants.ShopItemWeapon;
            BackgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            _weaponStaticData = StaticDataService.ForShopWeapon(_weaponTypeId);

            MainIcon.sprite = _weaponStaticData.MainImage;
            MainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            LevelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            CostText.text = $"{_weaponStaticData.Cost} $";
            // CostText.color = Constants.ShopItemPerk;
            CountText.text = "";
            TitleText.text = $"{_weaponStaticData.IRuTitle}";
        }

        private void Clicked()
        {
            if (IsMoneyEnough(_weaponStaticData.Cost))
            {
                ReduceMoney(_weaponStaticData.Cost);
                Progress.WeaponsData.SetAvailableWeapon(_weaponTypeId);
                ShopItemClicked?.Invoke();
            }

            ClearData();
        }
    }
}