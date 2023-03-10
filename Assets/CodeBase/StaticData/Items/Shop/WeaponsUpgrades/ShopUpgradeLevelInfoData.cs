namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    public struct ShopUpgradeLevelInfoData
    {
        public UpgradeTypeId UpgradeTypeId { get; private set; }
        public UpgradeLevelTypeId LevelTypeId { get; private set; }

        public ShopUpgradeLevelInfoData(UpgradeTypeId upgradeTypeId, UpgradeLevelTypeId levelTypeId)
        {
            UpgradeTypeId = upgradeTypeId;
            LevelTypeId = levelTypeId;
        }
    }
}