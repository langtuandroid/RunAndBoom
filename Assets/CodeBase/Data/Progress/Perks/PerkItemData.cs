using System;
using CodeBase.StaticData.Items;

namespace CodeBase.Data.Progress.Perks
{
    [Serializable]
    public class PerkItemData : LevelingItemData
    {
        public PerkTypeId PerkTypeId;

        public PerkItemData(PerkTypeId perkTypeId)
        {
            PerkTypeId = perkTypeId;
            InitNew();
        }

        public PerkItemData(PerkTypeId perkTypeId, LevelTypeId levelTypeId)
        {
            PerkTypeId = perkTypeId;
            LevelTypeId = levelTypeId;
        }

        public new void Up() =>
            base.Up();

        public new LevelTypeId GetNextLevel() =>
            base.GetNextLevel();
    }
}