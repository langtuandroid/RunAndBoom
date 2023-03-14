namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    public struct ShopUpgradeLevelInfoData
    {
        public UpgradeTypeId UpgradeTypeId { get; private set; }
        public LevelTypeId LevelTypeId { get; private set; }

        public ShopUpgradeLevelInfoData(UpgradeTypeId upgradeTypeId, LevelTypeId levelTypeId)
        {
            UpgradeTypeId = upgradeTypeId;
            LevelTypeId = levelTypeId;
        }
    }
}