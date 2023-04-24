using CodeBase.Data;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class ItemPurchasingItemView : BaseItemView
    {
        [SerializeField] private Button _button;

        private ShopItemStaticData _itemStaticData;
        private ItemTypeId _typeId;

        // private void OnEnable() =>
        //     _button?.onClick.AddListener(Clicked);
        //
        // private void OnDisable() =>
        //     _button?.onClick.RemoveListener(Clicked);

        public void Construct(ItemTypeId typeId, PlayerProgress progress)
        {
            // _button?.onClick.AddListener(Clicked);
            _typeId = typeId;
            base.Construct(progress);
            FillData();
        }

        public void ChangeClickability(bool isClickable) =>
            _button.interactable = isClickable;

        protected override void FillData()
        {
            _itemStaticData = StaticDataService.ForShopItem(_typeId);

            BackgroundIcon.color = Constants.ShopItemItem;
            BackgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            MainIcon.sprite = _itemStaticData.MainImage;
            MainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            LevelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            CostText.text = $"{_itemStaticData.Cost} $";
            // CostText.color = Constants.ShopItemPerk;
            CountText.text = "";
            TitleText.text = $"{_itemStaticData.IRuTitle}";
        }

        public void Clicked()
        {
            if (IsMoneyEnough(_itemStaticData.Cost))
            {
                ReduceMoney(_itemStaticData.Cost);

                if (_itemStaticData.TypeId == ItemTypeId.HealthRecover)
                    Progress.HealthState.ChangeCurrentHP(Progress.HealthState.BaseMaxHp);
                ClearData();
            }
        }
    }
}