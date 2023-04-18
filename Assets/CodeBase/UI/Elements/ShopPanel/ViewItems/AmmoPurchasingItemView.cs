using System;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.UI.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class AmmoPurchasingItemView : MonoBehaviour
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
        private AmmoCountType _countType;
        private AmmoItem _ammoItem;
        private ShopAmmoStaticData _shopAmmoStaticData;

        public event Action ShopItemClicked;

        private void Awake() =>
            BackgroundIcon = GetComponent<Image>();

        public void Construct(AmmoItem ammoItem, IPlayerProgressService playerProgressService)
        {
            PlayerProgressService = playerProgressService;
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
            _button.onClick.AddListener(Clicked);
            _ammoItem = ammoItem;
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

        // public void ChangeClickability(bool isClickable) =>
        //     Button.interactable = isClickable;

        private bool IsMoneyEnough(int value) =>
            PlayerProgressService.Progress.CurrentLevelStats.MoneyData.IsMoneyEnough(value);

        private void ReduceMoney(int value) =>
            PlayerProgressService.Progress.CurrentLevelStats.MoneyData.ReduceMoney(value);

        public void ChangeClickability(bool isClickable) =>
            _button.interactable = isClickable;

        private void FillData()
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

        private void Clicked()
        {
            if (IsMoneyEnough(_shopAmmoStaticData.Cost))
            {
                ReduceMoney(_shopAmmoStaticData.Cost);
                int.TryParse(_shopAmmoStaticData.Count.ToString(), out int count);
                PlayerProgressService.Progress.WeaponsData.WeaponsAmmoData.AddAmmo(_ammoItem.WeaponTypeId, count);
                ShopItemClicked?.Invoke();
            }

            ClearData();
        }
    }
}