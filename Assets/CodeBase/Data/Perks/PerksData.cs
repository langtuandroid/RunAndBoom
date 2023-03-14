using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Items;

namespace CodeBase.Data.Perks
{
    [Serializable]
    public class PerksData
    {
        public List<PerkData> Perks { get; private set; }

        public event Action<PerkData> NewPerkAdded;

        public PerksData()
        {
            int count = Enum.GetValues(typeof(PerkTypeId)).Cast<PerkTypeId>().Count();
            Perks = new List<PerkData>(count);

            FillTestData();
        }

        private void FillTestData()
        {
            PerkData armor = new PerkData(PerkTypeId.Armor);
            Perks.Add(armor);

            PerkData regeneration = new PerkData(PerkTypeId.Regeneration);
            regeneration.Up();
            Perks.Add(regeneration);

            PerkData running = new PerkData(PerkTypeId.Running);
            running.Up();
            running.Up();
            Perks.Add(running);

            PerkData vampire = new PerkData(PerkTypeId.Vampire);
            Perks.Add(vampire);

            PerkData maxHealth = new PerkData(PerkTypeId.MaxHealth);
            maxHealth.Up();
            Perks.Add(maxHealth);
        }

        public bool IsAvailable(PerkTypeId typeId) =>
            Perks.Exists(x => x.PerkTypeId == typeId && x.LevelTypeId != LevelTypeId.None);

        public void LevelUp(PerkTypeId typeId)
        {
            PerkData perkData = Perks.First(x => x.PerkTypeId == typeId);

            if (perkData.LevelTypeId == LevelTypeId.Level_3)
                return;

            perkData.Up();

            if (perkData.LevelTypeId == LevelTypeId.None)
                NewPerkAdded?.Invoke(perkData);
        }

        public bool IsLastLevel(PerkTypeId typeId)
        {
            var perk = Perks.First(x => x.PerkTypeId == typeId);
            return perk.LevelTypeId == LevelTypeId.Level_3;
        }
    }
}