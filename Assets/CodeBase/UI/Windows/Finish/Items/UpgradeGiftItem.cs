using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class UpgradeGiftItem : UpgradeItemBase
    {
        protected override void Clicked()
        {
            Progress.WeaponsData.UpgradesData.LevelUp(_upgradableWeaponStaticData.WeaponTypeId,
                _upgradeStaticData.UpgradeTypeId);
            ClearData();
        }
    }
}