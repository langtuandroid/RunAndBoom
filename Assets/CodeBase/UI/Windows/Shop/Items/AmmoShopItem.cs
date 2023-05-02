using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class AmmoShopItem : AmmoItemBase
    {
        protected override void Clicked()
        {
            if (ShopItemBalance.IsMoneyEnough(_shopAmmoStaticData.Cost))
            {
                ShopItemBalance.ReduceMoney(_shopAmmoStaticData.Cost);
                int value = _shopAmmoStaticData.Count.GetHashCode();
                Progress.WeaponsData.WeaponsAmmoData.AddAmmo(_ammoItem.WeaponTypeId, value);
                ClearData();
            }
        }
    }
}