using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Data.Upgrades;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.WeaponUpgrades
{
    public class UpgradesList : MonoBehaviour, IProgressReader
    {
        [SerializeField] private Transform _container;
        [SerializeField] private UpgradeView perkView;
        [SerializeField] private HeroWeaponTypeId _weaponTypeId;

        private IEnumerable<UpgradeTypeId> _upgradeTypeIds = Enum.GetValues(typeof(UpgradeTypeId)).Cast<UpgradeTypeId>();
        private Dictionary<UpgradeTypeId, UpgradeView> _activeUpgrades;
        private PlayerProgress _progress;

        private void Awake()
        {
            _activeUpgrades = new Dictionary<UpgradeTypeId, UpgradeView>(_upgradeTypeIds.Count());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;
            _progress.WeaponsData.WeaponUpgradesData.NewUpgradeAdded += AddNewUpgrade;

            ConstructUpgrades();
        }

        private void AddNewUpgrade(HeroWeaponTypeId heroWeaponTypeId, UpgradeItemData upgrade)
        {
            UpgradeView value = Instantiate(perkView, _container);
            value.Construct(upgrade);
            _activeUpgrades.Add(upgrade.UpgradeTypeId, value);
        }

        private void ConstructUpgrades()
        {
            List<UpgradeItemData> list = _progress.WeaponsData.WeaponUpgradesData.UpgradeItemDatas[_weaponTypeId];

            foreach (UpgradeItemData upgrade in list)
            {
                if (upgrade.LevelTypeId != LevelTypeId.None)
                    AddNewUpgrade(_weaponTypeId, upgrade);
            }
        }
    }
}