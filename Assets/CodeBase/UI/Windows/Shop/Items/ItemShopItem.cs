using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class ItemShopItem : ItemItemBase
    {
        protected override void Clicked()
        {
            if (ShopItemBalance.IsMoneyEnough(_itemStaticData.Cost))
            {
                ShopItemBalance.ReduceMoney(_itemStaticData.Cost);

                if (_itemStaticData.TypeId == ItemTypeId.HealthRecover)
                {
                    Progress.HealthState.ChangeCurrentHP(Progress.HealthState.BaseMaxHp);
                    _health.ChangeHealth();
                }

                ClearData();
            }
        }
    }
}