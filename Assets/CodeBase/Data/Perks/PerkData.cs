using System;
using CodeBase.StaticData.Items;

namespace CodeBase.Data.Perks
{
    [Serializable]
    public class PerkData: LeveledData
    {
        public PerkTypeId PerkTypeId { get; private set; }

        public PerkData(PerkTypeId perkTypeId)
        {
            PerkTypeId = perkTypeId;
        }

        public void Up() => 
            base.Up();
    }
}