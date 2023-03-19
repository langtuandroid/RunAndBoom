using System;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Services;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class ShopItemView : BaseShopView, IShopItem
    {
        private ShopItemStaticData _itemStaticData;
        private ItemTypeId _typeId;

        public event Action ShopItemClicked;

        public void Construct(ItemTypeId typeId)
        {
            _typeId = typeId;
            base.Construct();
            FillData();
        }

        protected override void FillData()
        {
            _itemStaticData = StaticDataService.ForShopItem(_typeId);

            MainIcon.sprite = _itemStaticData.MainImage;
            LevelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            CostText.text = $"{_itemStaticData.Cost} $";
            CostText.color = Constants.ShopItemPerk;
            CountText.text = "";
            TitleText.text = $"{_itemStaticData.IRuTitle}";
        }

        protected override void Clicked()
        {
            if (IsMoneyEnough(_itemStaticData.Cost))
            {
                ReduceMoney(_itemStaticData.Cost);

                if (_typeId == ItemTypeId.HealthRecover)
                    Progress.HealthState.ChangeCurrentHP(Progress.HealthState.MaxHp);

                ShopItemClicked?.Invoke();
            }
        }
    }
}