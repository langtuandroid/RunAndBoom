using System;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public abstract class BasePurchasingItemView : BaseItemView, IShopItem
    {
        public abstract event Action ShopItemClicked;

        protected abstract override void FillData();
    }
}