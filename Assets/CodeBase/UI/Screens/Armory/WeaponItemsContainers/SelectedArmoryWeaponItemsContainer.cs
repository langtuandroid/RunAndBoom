using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.CustomClasses;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Weapon;
using CodeBase.UI.Screens.Armory.WeaponItems;
using CodeBase.UI.Services.Factory;
using CodeBase.Weapons;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Screens.Armory.WeaponItemsContainers
{
    public class SelectedArmoryWeaponItemsContainer : ArmoryWeaponItemsContainer
    {
        private AvailableArmoryWeaponItemsContainer _availableArmoryWeaponItemsContainer;

        private WeaponsSelection _weaponsSelection;
        public static LinkedHashSet<WeaponTypeId> SelectedWeaponTypeIds { get; private set; }
        private List<GameObject> _weaponItemGameObjects = new List<GameObject>();

        public static event Action<WeaponTypeId> ItemSelected;

        // public new void Construct(IPersistentProgressService progressService, IStaticDataService staticData,
        //     IUIFactory uiFactory)
        // {
        //     base.Construct(progressService, staticData, uiFactory);
        // }

        [Inject]
        public void Construct(IPlayerProgressService progressService, IStaticDataService staticData, IUIFactory uiFactory,
            LinkedHashSet<WeaponTypeId> weaponTypeIds, WeaponsSelection weaponsSelection)
        {
            base.Construct(progressService, staticData, uiFactory);
            SelectedWeaponTypeIds = weaponTypeIds;
            _weaponsSelection = weaponsSelection;
            FillWeaponItems(weaponTypeIds);
        }

        public new void Initialize()
        {
            base.Initialize();
        }

        public void Subscribe()
        {
            // _availableArmoryWeaponItemsContainer.ItemSelected += ItemClicked;
        }

        public void Unsubscribe()
        {
            // _availableArmoryWeaponItemsContainer.ItemSelected -= ItemClicked;
        }

        private async void FillWeaponItems(LinkedHashSet<WeaponTypeId> weaponsTypeIds)
        {
            foreach (WeaponTypeId typeId in weaponsTypeIds)
                await CreateArmoryWeaponItem(typeId);
        }

        private async Task CreateArmoryWeaponItem(WeaponTypeId typeId)
        {
            GameObject weaponItemPrefab = await UIFactory.CreateSelectedArmoryWeaponItem(Parent);

            WeaponStaticData weaponStaticData = StaticData.ForWeaponUI(typeId);
            SelectedArmoryWeaponItem selectedArmoryWeaponItem =
                weaponItemPrefab.GetComponent<SelectedArmoryWeaponItem>();

            WeaponArmoryDescription description = new WeaponArmoryDescription(name: weaponStaticData.Name,
                mainFireDamage: weaponStaticData.MainFireDamage, mainFireCost: weaponStaticData.MainFireCost,
                mainFireCooldown: weaponStaticData.MainFireCooldown,
                mainFireBarrels: weaponStaticData.MainFireBarrels, mainFireRange: weaponStaticData.MainFireRange,
                mainFireBulletSpeed: weaponStaticData.MainFireBulletSpeed,
                mainFireRotatingSpeed: weaponStaticData.MainFireRotationSpeed);

            selectedArmoryWeaponItem.Construct(ProgressService, gameObject, weaponStaticData.Icon, typeId, description);
            selectedArmoryWeaponItem.Initialize();
            _weaponItemGameObjects.Add(weaponItemPrefab.gameObject);
            // _weaponItemGameObjects.Add(weaponItemPrefab.gameObject);
        }

        public override void OnItemClick(WeaponTypeId typeId)
        {
            // ItemSelected?.Invoke(typeId);
            // ItemClicked(typeId);

            bool isExits =
                // _weaponsSelection.
                SelectedWeaponTypeIds.Contains(typeId);

            if (isExits)
            {
                RemoveWeaponItem(typeId);
                SelectedWeaponTypeIds.Remove(typeId);
            }
            else
            {
                AddWeaponItem(typeId);
                SelectedWeaponTypeIds.Add(typeId);
            }
        }

        private void ItemClicked(WeaponTypeId typeId)
        {
            // _weaponsSelection?.AvailableWeaponDateClicked(typeId);
            // _weaponsSelection?.SelectedWeaponTypeIdClicked(typeId);

            // bool isExits = SelectedWeaponTypeIds.Contains(typeId);
        }

        private void RemoveWeaponItem(WeaponTypeId typeId)
        {
            // TODO(Fix it)
            for (int i = 0; i < _weaponItemGameObjects.Count - 1; i++)
            {
                ArmoryWeaponItem armoryWeaponItem = _weaponItemGameObjects[i].GetComponent<ArmoryWeaponItem>();

                if (armoryWeaponItem.WeaponTypeId == typeId)
                {
                    _weaponItemGameObjects.Remove(_weaponItemGameObjects[i]);
                    Destroy(_weaponItemGameObjects[i]);
                    return;
                }
            }
        }

        private async void AddWeaponItem(WeaponTypeId typeId) =>
            await CreateArmoryWeaponItem(typeId);
    }
}