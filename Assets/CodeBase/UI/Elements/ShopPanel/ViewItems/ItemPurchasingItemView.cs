using System;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class ItemPurchasingItemView : MonoBehaviour
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
        private ShopItemStaticData _itemStaticData;
        private ItemTypeId _typeId;

        public event Action ShopItemClicked;

        private void OnEnable() =>
            _button?.onClick.AddListener(Clicked);

        private void OnDisable() =>
            _button.onClick.RemoveListener(Clicked);

        public void Construct(ItemTypeId typeId, IPlayerProgressService progressService)
        {
            _button?.onClick.AddListener(Clicked);
            PlayerProgressService = progressService;
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
            _typeId = typeId;
            FillData();
        }

        private bool IsMoneyEnough(int value) =>
            PlayerProgressService.Progress.CurrentLevelStats.MoneyData.IsMoneyEnough(value);

        private void ReduceMoney(int value) =>
            PlayerProgressService.Progress.CurrentLevelStats.MoneyData.ReduceMoney(value);

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

        public void ChangeClickability(bool isClickable) =>
            _button.interactable = isClickable;

        private void FillData()
        {
            _backgroundIcon.color = Constants.ShopItemItem;
            _backgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            _itemStaticData = StaticDataService.ForShopItem(_typeId);
            _mainIcon.sprite = _itemStaticData.MainImage;
            _mainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            _levelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            _additionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            _costText.text = $"{_itemStaticData.Cost} $";
            // CostText.color = Constants.ShopItemPerk;
            _countText.text = "";
            _titleText.text = $"{_itemStaticData.IRuTitle}";
        }

        private void Clicked()
        {
            if (IsMoneyEnough(_itemStaticData.Cost))
            {
                ReduceMoney(_itemStaticData.Cost);

                if (_itemStaticData.TypeId == ItemTypeId.HealthRecover)
                    PlayerProgressService.Progress.HealthState.ChangeCurrentHP(PlayerProgressService.Progress.HealthState.MaxHp);

                ShopItemClicked?.Invoke();
            }

            ClearData();
        }
    }
}