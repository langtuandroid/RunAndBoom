namespace CodeBase.StaticData.Items
{
    public struct PerkItem
    {
        public PerkTypeId PerkTypeId { get; private set; }
        public LevelTypeId LevelTypeId { get; private set; }

        public PerkItem(PerkTypeId perkTypeId, LevelTypeId levelTypeId)
        {
            PerkTypeId = perkTypeId;
            LevelTypeId = levelTypeId;
        }
    }
}