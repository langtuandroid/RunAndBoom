using CodeBase.Data;
using CodeBase.Data.Upgrades;
using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class UpgradeGiftItem : UpgradeItemBase
    {
        private GiftsGenerator _generator;

        public void Construct(UpgradeItemData upgradeItemData, PlayerProgress progress, GiftsGenerator generator)
        {
            _generator = generator;
            base.Construct(upgradeItemData, progress);
        }

        protected override void Clicked()
        {
            Progress.WeaponsData.UpgradesData.LevelUp(_upgradableWeaponStaticData.WeaponTypeId,
                _upgradeStaticData.UpgradeTypeId);
            ClearData();
            _generator.Clicked();
        }
    }
}