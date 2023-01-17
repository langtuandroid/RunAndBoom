using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class AvailableArmoryWeaponItemsContainer : ArmoryWeaponItemsContainer
    {
        private SelectedArmoryWeaponItemsContainer _selectedArmoryWeaponItemsContainer;
        private WeaponsSelection _weaponsSelection;

        public static Dictionary<WeaponTypeId, bool> AvailableWeaponDates { get; private set; }
        private List<GameObject> _weaponItemGameObjects = new List<GameObject>();
        public static event Action<WeaponTypeId> ItemSelected;

        // public new void Construct(IPlayerProgressService progressService, IStaticDataService staticData,
        //     IUIFactory uiFactory)
        // {
        //     base.Construct(progressService, staticData, uiFactory);
        // }

        [Inject]
        public void Construct(IPlayerProgressService progressService, IStaticDataService staticData, IUIFactory uiFactory, Dictionary<WeaponTypeId, bool> availableWeaponDates, WeaponsSelection weaponsSelection)
        {
            base.Construct(progressService, staticData, uiFactory);
            AvailableWeaponDates = availableWeaponDates;
            _weaponsSelection = weaponsSelection;
            FillWeaponItems(availableWeaponDates);
            // WeaponsSelection.OnItemClicked += ;
            // ItemSelected += ItemCheck;
            // ItemSelected?.Invoke(WeaponTypeId.FourBarrelShotgun);
        }

        public new void Initialize()
        {
            base.Initialize();
        }

        public void Subscribe()
        {
            // _selectedArmoryWeaponItemsContainer.ItemSelected += ItemClicked;
        }

        public void Unsubscribe()
        {
            // _selectedArmoryWeaponItemsContainer.ItemSelected -= ItemClicked;
        }


        private async void FillWeaponItems(Dictionary<WeaponTypeId, bool> weaponsDatas)
        {
            foreach (var data in weaponsDatas)
                await CreateAvailableArmoryWeaponItem(data.Key, data.Value);
        }

        private async Task CreateAvailableArmoryWeaponItem(WeaponTypeId typeId, bool isSelected)
        {
            GameObject weaponItemPrefab = await UIFactory.CreateAvailableArmoryWeaponItem(Parent);
            WeaponStaticData weaponStaticData = StaticData.ForWeaponUI(typeId);
            AvailableArmoryWeaponItem availableArmoryWeaponItem =
                weaponItemPrefab.GetComponent<AvailableArmoryWeaponItem>();

            WeaponArmoryDescription description = new WeaponArmoryDescription(name: weaponStaticData.Name,
                mainFireDamage: weaponStaticData.MainFireDamage, mainFireCost: weaponStaticData.MainFireCost,
                mainFireCooldown: weaponStaticData.MainFireCooldown,
                mainFireBarrels: weaponStaticData.MainFireBarrels, mainFireRange: weaponStaticData.MainFireRange,
                mainFireBulletSpeed: weaponStaticData.MainFireBulletSpeed,
                mainFireRotatingSpeed: weaponStaticData.MainFireRotationSpeed);

            availableArmoryWeaponItem.Construct(ProgressService, gameObject, weaponStaticData.Icon, typeId, isSelected,
                description);
            availableArmoryWeaponItem.Initialize();
            _weaponItemGameObjects.Add(weaponItemPrefab.gameObject);
            // _weaponItemGameObjects.Add(weaponItemPrefab.gameObject);
        }

        public override void OnItemClick(WeaponTypeId typeId)
        {
            AvailableWeaponDates[typeId] = !AvailableWeaponDates[typeId];
            // ItemSelected?.Invoke(typeId);
            // _weaponsSelection?.AvailableWeaponDateClicked(typeId);
            // _weaponsSelection?.SelectedWeaponTypeIdClicked(typeId);
            // ItemClicked(typeId);
        }

        private void ItemCheck(WeaponTypeId typeId)
        {
            Debug.Log("ItemClicked");
            // ChangeValue(typeId);
        }

        private void ChangeValue(WeaponTypeId typeId) =>
            AvailableWeaponDates[typeId] = !AvailableWeaponDates[typeId];
    }
}