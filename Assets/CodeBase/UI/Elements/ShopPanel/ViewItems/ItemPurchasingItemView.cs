using System;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class ItemPurchasingItemView : MonoBehaviour
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
        private ShopItemStaticData _itemStaticData;
        private ItemTypeId _typeId;

        public event Action ShopItemClicked;

        public void Construct(ItemTypeId typeId, IPlayerProgressService progressService)
        {
            PlayerProgressService = progressService;
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
            _button.onClick.AddListener(Clicked);
            _typeId = typeId;
            FillData();
        }

        private bool IsMoneyEnough(int value) =>
            PlayerProgressService.Progress.CurrentLevelStats.MoneyData.IsMoneyEnough(value);

        private void ReduceMoney(int value) =>
            PlayerProgressService.Progress.CurrentLevelStats.MoneyData.ReduceMoney(value);

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

        public void ChangeClickability(bool isClickable) =>
            _button.interactable = isClickable;

        protected void FillData()
        {
            BackgroundIcon.color = Constants.ShopItemItem;
            BackgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            _itemStaticData = StaticDataService.ForShopItem(_typeId);
            MainIcon.sprite = _itemStaticData.MainImage;
            MainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            LevelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            CostText.text = $"{_itemStaticData.Cost} $";
            // CostText.color = Constants.ShopItemPerk;
            CountText.text = "";
            TitleText.text = $"{_itemStaticData.IRuTitle}";
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