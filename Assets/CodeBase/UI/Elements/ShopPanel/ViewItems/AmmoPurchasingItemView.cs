using System;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.UI.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class AmmoPurchasingItemView : MonoBehaviour
    {
        [FormerlySerializedAs("BackgroundIcon")] [SerializeField]
        private Image _backgroundIcon;

        [FormerlySerializedAs("MainIcon")] [SerializeField]
        private Image _mainIcon;

        [FormerlySerializedAs("LevelIcon")] [SerializeField]
        private Image _levelIcon;

        [FormerlySerializedAs("AdditionalIcon")] [SerializeField]
        private Image _additionalIcon;

        [FormerlySerializedAs("CostText")] [SerializeField]
        private TextMeshProUGUI _costText;

        [FormerlySerializedAs("CountText")] [SerializeField]
        private TextMeshProUGUI _countText;

        [FormerlySerializedAs("TitleText")] [SerializeField]
        private TextMeshProUGUI _titleText;

        [SerializeField] private Button _button;

        private IStaticDataService StaticDataService;
        private IPlayerProgressService PlayerProgressService;
        private AmmoCountType _countType;
        private AmmoItem _ammoItem;
        private ShopAmmoStaticData _shopAmmoStaticData;

        public event Action ShopItemClicked;

        private void OnEnable() =>
            _button?.onClick.AddListener(Clicked);

        private void OnDisable() =>
            _button?.onClick.RemoveListener(Clicked);

        public void Construct(AmmoItem ammoItem, IPlayerProgressService playerProgressService)
        {
            _button?.onClick.AddListener(Clicked);
            PlayerProgressService = playerProgressService;
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
            _ammoItem = ammoItem;
            FillData();
        }

        public void ClearData()
        {
            if (_backgroundIcon != null)
                _backgroundIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (_mainIcon != null)
                _mainIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (_levelIcon != null)
                _levelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (_additionalIcon != null)
                _additionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (_costText != null)
                _costText.text = "";

            if (_countText != null)
                _countText.text = "";

            if (_titleText != null)
                _titleText.text = "";
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
            _backgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            _backgroundIcon.color = Constants.ShopItemAmmo;
            _mainIcon.sprite = _shopAmmoStaticData.MainImage;
            _mainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            _levelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            _additionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            _costText.text = $"{_shopAmmoStaticData.Cost} $";
            // CostText.color = Constants.ShopItemPerk;
            int ammoCountType = (int)_shopAmmoStaticData.Count;
            _countText.text = $"{ammoCountType}";
            // CountText.color = Constants.ShopItemCountField;
            _titleText.text = $"{_shopAmmoStaticData.IRuTitle}";
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