using System;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.Shop.Weapons;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class WeaponPurchasingItemView : MonoBehaviour
    {
        [SerializeField] private Image BackgroundIcon;
        [SerializeField] private Image MainIcon;
        [SerializeField] private Image LevelIcon;
        [SerializeField] private Image AdditionalIcon;
        [SerializeField] private TextMeshProUGUI CostText;
        [SerializeField] private TextMeshProUGUI CountText;
        [SerializeField] private TextMeshProUGUI TitleText;
        [SerializeField] private Button _button;

        private IStaticDataService StaticDataService;
        private IPlayerProgressService PlayerProgressService;
        private HeroWeaponTypeId _weaponTypeId;
        private ShopWeaponStaticData _weaponStaticData;

        public event Action ShopItemClicked;

        public void Construct(HeroWeaponTypeId weaponTypeId, IPlayerProgressService playerProgressService)
        {
            PlayerProgressService = playerProgressService;
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
            _button.onClick.AddListener(Clicked);
            _weaponTypeId = weaponTypeId;
            FillData();
        }

        public void ClearData()
        {
            if (BackgroundIcon != null)
                BackgroundIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (MainIcon != null)
                MainIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (LevelIcon != null)
                LevelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (AdditionalIcon != null)
                AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (CostText != null)
                CostText.text = "";

            if (CountText != null)
                CountText.text = "";

            if (TitleText != null)
                TitleText.text = "";
        }

        private bool IsMoneyEnough(int value) =>
            PlayerProgressService.Progress.CurrentLevelStats.MoneyData.IsMoneyEnough(value);

        private void ReduceMoney(int value) =>
            PlayerProgressService.Progress.CurrentLevelStats.MoneyData.ReduceMoney(value);

        public void ChangeClickability(bool isClickable) =>
            _button.interactable = isClickable;

        private void FillData()
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
                PlayerProgressService.Progress.WeaponsData.SetAvailableWeapon(_weaponTypeId);
                ShopItemClicked?.Invoke();
            }

            ClearData();
        }
    }
}