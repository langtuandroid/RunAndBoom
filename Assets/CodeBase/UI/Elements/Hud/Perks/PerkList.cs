using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Data.Perks;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Items;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.Perks
{
    public class PerkList : MonoBehaviour, IProgressReader
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Perk perk;

        private IEnumerable<PerkTypeId> _perkTypeIds = Enum.GetValues(typeof(PerkTypeId)).Cast<PerkTypeId>();
        private Dictionary<PerkTypeId, Perk> _activePerks;
        private PlayerProgress _progress;

        private void Awake()
        {
            _activePerks = new Dictionary<PerkTypeId, Perk>(_perkTypeIds.Count());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;
            _progress.PerksData.NewPerkAdded += AddNewPerk;

            ConstructPerks();
        }

        private void AddNewPerk(PerkData perkData)
        {
            Perk value = Instantiate(perk, _container);
            value.Construct(perkData);
            _activePerks.Add(perkData.PerkTypeId, value);
        }

        private void ConstructPerks()
        {
            foreach (PerkData perkData in _progress.PerksData.Perks)
            {
                if (perkData.LevelTypeId != LevelTypeId.None)
                    AddNewPerk(perkData);
            }
        }
    }
}