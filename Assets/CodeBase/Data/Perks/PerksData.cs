using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Items;

namespace CodeBase.Data.Perks
{
    [Serializable]
    public class PerksData
    {
        public List<PerkItemData> Perks { get; private set; }

        public event Action<PerkItemData> NewPerkAdded;

        public PerksData()
        {
            int count = DataExtensions.GetValues<PerkTypeId>().Count();
            Perks = new List<PerkItemData>(count);

            // FillTestData();
            FillEmptyData();
        }

        private void FillTestData()
        {
            PerkItemData armor = new PerkItemData(PerkTypeId.Armor);
            Perks.Add(armor);

            PerkItemData regeneration = new PerkItemData(PerkTypeId.Regeneration);
            regeneration.Up();
            Perks.Add(regeneration);

            PerkItemData running = new PerkItemData(PerkTypeId.Running);
            running.Up();
            running.Up();
            Perks.Add(running);

            PerkItemData vampire = new PerkItemData(PerkTypeId.Vampire);
            Perks.Add(vampire);

            PerkItemData maxHealth = new PerkItemData(PerkTypeId.MaxHealth);
            maxHealth.Up();
            Perks.Add(maxHealth);
        }

        private void FillEmptyData()
        {
            PerkItemData armor = new PerkItemData(PerkTypeId.Armor);
            PerkItemData regeneration = new PerkItemData(PerkTypeId.Regeneration);
            PerkItemData running = new PerkItemData(PerkTypeId.Running);
            PerkItemData vampire = new PerkItemData(PerkTypeId.Vampire);
            PerkItemData maxHealth = new PerkItemData(PerkTypeId.MaxHealth);

            Perks.Add(armor);
            Perks.Add(regeneration);
            Perks.Add(running);
            Perks.Add(vampire);
            Perks.Add(maxHealth);
        }

        public bool IsAvailable(PerkTypeId typeId) =>
            Perks.Exists(x => x.PerkTypeId == typeId && x.LevelTypeId != LevelTypeId.None);

        public void LevelUp(PerkTypeId typeId)
        {
            PerkItemData perkItemData = Perks.First(x => x.PerkTypeId == typeId);

            if (perkItemData.LevelTypeId == LevelTypeId.Level_3)
                return;

            perkItemData.Up();

            if (perkItemData.LevelTypeId == LevelTypeId.Level_1)
                NewPerkAdded?.Invoke(perkItemData);
        }

        public bool IsLastLevel(PerkTypeId typeId)
        {
            var perk = Perks.First(x => x.PerkTypeId == typeId);
            return perk.LevelTypeId == LevelTypeId.Level_3;
        }

        public PerkItemData GetNextLevelPerk(PerkTypeId typeId)
        {
            PerkItemData perk = new PerkItemData(Perks.First(x => x.PerkTypeId == typeId).PerkTypeId);
            LevelTypeId nextLevel = perk.GetNextLevel();
            return new PerkItemData(perk.PerkTypeId, nextLevel);
        }
    }
}