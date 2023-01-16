using System;
using System.Collections.Generic;
using CodeBase.CustomClasses;
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Weapon;
using CodeBase.UI.Screens.Armory.WeaponItemsContainers;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Screens.Armory
{
    public class WeaponsSelection : MonoBehaviour, IProgressSaver
    {
        // [SerializeField] private GameObject _selectedWeaponItemsContainerGameObject;
        // [SerializeField] private GameObject _availableWeaponItemsContainerGameObject;

        [Inject] private IPlayerProgressService _progressService;
        [Inject] private IAssets _assets;
        [Inject] private IStaticDataService _staticData;
        [Inject] private IUIFactory _uiFactory;
        private SelectedArmoryWeaponItemsContainer _selectedWeaponItemsContainer;
        private AvailableArmoryWeaponItemsContainer _availableWeaponItemsContainer;

        // public LinkedHashSet<WeaponTypeId> SelectedWeaponTypeIds { get; private set; }
        // private Dictionary<WeaponTypeId, bool> AvailableWeaponDates { get; set; }

        public static Action<WeaponTypeId> OnItemClicked;

        // public void Construct(IPersistentProgressService progressService, IAssets assets, IStaticDataService staticData,
        //     IUIFactory uiFactory)
        // {
        //     _progressService = progressService;
        //     _assets = assets;
        //     _staticData = staticData;
        //     _uiFactory = uiFactory;
        // }

        public void SelectedWeaponTypeIdClicked(WeaponTypeId typeId)
        {
            ChangeSelectedWeaponTypeIds(typeId);
        }

        public void AvailableWeaponDateClicked(WeaponTypeId typeId)
        {
            // AvailableWeaponDates[typeId] = !AvailableWeaponDates[typeId];
        }

        private void ChangeSelectedWeaponTypeIds(WeaponTypeId typeId)
        {
            // if (SelectedWeaponTypeIds.Contains(typeId))
            //     SelectedWeaponTypeIds.Remove(typeId);
            // else
            //     SelectedWeaponTypeIds.Add(typeId);
        }

        public void Initialize()
        {
        }

        public void LoadProgress(PlayerProgress progress)
        {
            CreateSelectedArmoryWeaponItemsContainer(progress.SelectedWeaponTypeIds);
            CreateAvailableArmoryWeaponItemsContainer(progress.AvailableWeaponDatas);
            // SelectedWeaponTypeIds = progress.SelectedWeaponTypeIds;
            // AvailableWeaponDates = progress.AvailableWeaponDatas;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            // progress.SetSelectedWeapons(SelectedWeaponTypeIds);
            // progress.SetAvailableWeapons(AvailableWeaponDates);
            progress.SetSelectedWeapons(SelectedArmoryWeaponItemsContainer.SelectedWeaponTypeIds);
            progress.SetAvailableWeapons(AvailableArmoryWeaponItemsContainer.AvailableWeaponDates);

            Unsubscribe();
        }

        private async void CreateSelectedArmoryWeaponItemsContainer(LinkedHashSet<WeaponTypeId> weaponTypeIds)
        {
            GameObject selectedWeaponItemsContainerGameObject =
                await _uiFactory.CreateSelectedArmoryWeaponItemsContainer(gameObject.transform);
            _selectedWeaponItemsContainer = selectedWeaponItemsContainerGameObject
                .GetComponent<SelectedArmoryWeaponItemsContainer>();
            _selectedWeaponItemsContainer.Construct(
                // _progressService, _staticData, _uiFactory, 
                weaponTypeIds, this);
            _selectedWeaponItemsContainer.Initialize();
            // _selectedWeaponItemsContainer.FillWeaponItems(weaponTypeIds);
            // SelectedArmoryWeaponItemsContainer.ItemSelected += WeaponItemSelected;
        }

        private async void CreateAvailableArmoryWeaponItemsContainer(
            Dictionary<WeaponTypeId, bool> availableWeaponDatas)
        {
            GameObject availableWeaponItemsContainerGameObject =
                await _uiFactory.CreateAvailableArmoryWeaponItemsContainer(gameObject.transform);
            _availableWeaponItemsContainer = availableWeaponItemsContainerGameObject
                .GetComponent<AvailableArmoryWeaponItemsContainer>();
            _availableWeaponItemsContainer.Construct(_progressService, _staticData, _uiFactory,
                availableWeaponDatas, this);
            // _availableWeaponItemsContainer.FillWeaponItems(availableWeaponDatas);
            // AvailableArmoryWeaponItemsContainer.ItemSelected += WeaponItemSelected;
            _availableWeaponItemsContainer.Initialize();
        }

        private void Unsubscribe()
        {
            // _selectedWeaponItemsContainer.ItemSelected -= WeaponItemSelected;
            // _availableWeaponItemsContainer.ItemSelected -= WeaponItemSelected;
        }

        private void WeaponItemSelected(WeaponTypeId typeId)
        {
            // _selectedWeaponItemsContainer.OnItemClick(typeId);
            // _availableWeaponItemsContainer.OnItemClick(typeId);
        }
    }
}