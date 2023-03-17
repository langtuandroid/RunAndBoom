using CodeBase.Data.Upgrades;
using CodeBase.StaticData.Items.Inventory;

namespace CodeBase.UI.Elements.Hud.WeaponUpgrades
{
    public class UpgradeView : LevelingItemView
    {
        private UpgradeItemData _upgradeItemData;
        private InventoryUpgradeStaticData _upgradeStaticData;
        private InventoryUpgradeLevelStaticData _levelStaticData;

        public void Construct(UpgradeItemData upgradeItemData)
        {
            base.Construct(upgradeItemData);
            _upgradeItemData = upgradeItemData;
            ItemData.LevelChanged += ChangeLevel;
            ChangeLevel();
        }

        private new void ChangeLevel()
        {
            _upgradeStaticData = StaticDataService.ForInventoryUpgrade(_upgradeItemData.UpgradeTypeId);
            _levelStaticData = StaticDataService.ForInventoryUpgradeLevel(_upgradeItemData.LevelTypeId);
            LevelingStaticData = _levelStaticData;

            MainTypeImage.sprite = _upgradeStaticData.MainImage;
            LevelTypeImage.sprite = _levelStaticData.Level;

            base.ChangeLevel();
        }
    }
}