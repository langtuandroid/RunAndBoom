using System;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class ItemPurchasingItemView : BasePurchasingItemView
    {
        [SerializeField] private Button _button;

        private ShopItemStaticData _itemStaticData;
        private ItemTypeId _typeId;

        public override event Action ShopItemClicked;

        public void Construct(ItemTypeId typeId, IPlayerProgressService progressService)
        {
            // Button = _button;
            _button.onClick.AddListener(Clicked);
            _typeId = typeId;
            base.Construct(progressService);
            FillData();
        }

        public void ChangeClickability(bool isClickable) =>
            _button.interactable = isClickable;

        protected override void FillData()
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
                    Progress.HealthState.ChangeCurrentHP(Progress.HealthState.MaxHp);

                ShopItemClicked?.Invoke();
            }

            ClearData();
        }
    }
}