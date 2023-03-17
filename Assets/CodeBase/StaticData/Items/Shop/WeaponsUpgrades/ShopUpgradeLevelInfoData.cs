namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    public struct ShopUpgradeLevelInfoData
    {
        public UpgradeTypeId UpgradeTypeId { get; }
        public LevelTypeId LevelTypeId { get; }

        public ShopUpgradeLevelInfoData(UpgradeTypeId upgradeTypeId, LevelTypeId levelTypeId)
        {
            UpgradeTypeId = upgradeTypeId;
            LevelTypeId = levelTypeId;
        }
    }
}