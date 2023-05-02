using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class WeaponShopItem : WeaponItemBase
    {
        protected override void Clicked()
        {
            if (ShopItemBalance.IsMoneyEnough(_weaponStaticData.Cost))
            {
                ShopItemBalance.ReduceMoney(_weaponStaticData.Cost);
                Progress.WeaponsData.SetAvailableWeapon(_weaponTypeId);
                ClearData();
            }
        }
    }
}