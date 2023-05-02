using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class PerkShopItem : PerkItemBase
    {
        protected override void Clicked()
        {
            if (ShopItemBalance.IsMoneyEnough(_perkStaticData.Cost))
            {
                ShopItemBalance.ReduceMoney(_perkStaticData.Cost);
                Progress.PerksData.LevelUp(_perkStaticData.PerkTypeId);
                ClearData();
            }
        }
    }
}