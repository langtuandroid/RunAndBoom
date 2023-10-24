using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.Progress;
using CodeBase.Data.Progress.Upgrades;
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

        private IEnumerable<UpgradeTypeId>
            _upgradeTypeIds = Enum.GetValues(typeof(UpgradeTypeId)).Cast<UpgradeTypeId>();

        private Dictionary<UpgradeTypeId, UpgradeView> _activeUpgrades;
        private ProgressData _progressData;

        private void Awake() =>
            _activeUpgrades = new Dictionary<UpgradeTypeId, UpgradeView>(_upgradeTypeIds.Count());

        public void LoadProgressData(ProgressData progressData)
        {
            _progressData = progressData;
            _progressData.WeaponsData.UpgradesData.NewUpgradeAdded += AddNewUpgrade;

            ConstructUpgrades();
        }

        private void AddNewUpgrade(HeroWeaponTypeId heroWeaponTypeId, UpgradeItemData upgrade)
        {
            if (heroWeaponTypeId != _weaponTypeId)
                return;

            UpgradeView value = Instantiate(perkView, _container);
            value.Construct(upgrade);
            _activeUpgrades.Add(upgrade.UpgradeTypeId, value);
        }

        private void ConstructUpgrades()
        {
            foreach (UpgradeItemData upgrade in _progressData.WeaponsData.UpgradesData.UpgradeItemDatas)
            {
                if (upgrade.WeaponTypeId == _weaponTypeId)
                    if (upgrade.LevelTypeId != LevelTypeId.None)
                        AddNewUpgrade(_weaponTypeId, upgrade);
            }
        }
    }
}