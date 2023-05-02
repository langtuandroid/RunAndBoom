using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class UpgradeShopItem : UpgradeItemBase
    {
        protected override void Clicked()
        {
            if (ShopItemBalance.IsMoneyEnough(_upgradeLevelInfoStaticData.Cost))
            {
                ShopItemBalance.ReduceMoney(_upgradeLevelInfoStaticData.Cost);
                Progress.WeaponsData.UpgradesData.LevelUp(_upgradableWeaponStaticData.WeaponTypeId,
                    _upgradeStaticData.UpgradeTypeId);
                ClearData();
            }
        }
    }
}