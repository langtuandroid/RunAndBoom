using System;
using CodeBase.UI.Elements.ShopPanel.ViewItems;

namespace CodeBase.UI.Elements.ShopPanel
{
    public abstract class BasePurchasingItemView : BaseItemView, IShopItem
    {
        public abstract event Action ShopItemClicked;

        protected abstract override void FillData();

        protected abstract override void Clicked();
    }
}