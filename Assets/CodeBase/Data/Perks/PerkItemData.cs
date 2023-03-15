using System;
using CodeBase.StaticData.Items;

namespace CodeBase.Data.Perks
{
    [Serializable]
    public class PerkItemData : LevelingItemData
    {
        public PerkTypeId PerkTypeId { get; private set; }

        public PerkItemData(PerkTypeId perkTypeId)
        {
            PerkTypeId = perkTypeId;
        }

        public void Up() =>
            base.Up();
    }
}