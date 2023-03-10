using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;

namespace CodeBase.StaticData.Items
{
    public struct PerkData
    {
        public PerkTypeId PerkTypeId { get; private set; }
        public UpgradeLevelTypeId LevelTypeId { get; private set; }

        public PerkData(PerkTypeId perkTypeId, UpgradeLevelTypeId levelTypeId)
        {
            PerkTypeId = perkTypeId;
            LevelTypeId = levelTypeId;
        }
    }
}