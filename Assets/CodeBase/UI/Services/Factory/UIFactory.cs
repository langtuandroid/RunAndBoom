using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.Registrator;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        [Inject] private readonly IAssets _assets;
        [Inject] private readonly IRegistratorService _registratorService;

        private Transform _uiRoot;

        public async Task CreateUIRoot()
        {
            GameObject root = await _assets.Instantiate(AssetAddresses.UIRoot);
            _uiRoot = root.transform;
        }

        public Transform GetUIRoot() => _uiRoot;

        public async Task<GameObject> CreateAvailableArmoryWeaponItem(Transform parent) =>
            await ConstructUIElement(AssetAddresses.AvailableArmoryWeaponItem, parent);

        public async Task<GameObject> CreateSelectedArmoryWeaponItem(Transform parent) =>
            await ConstructUIElement(AssetAddresses.SelectedArmoryWeaponItem, parent);

        public async Task<GameObject> CreateLevelWeaponItem(Transform parent) =>
            await ConstructUIElement(AssetAddresses.LevelWeaponItem, parent);

        public async Task<GameObject> CreateSelectedArmoryWeaponItemsContainer() =>
            await ConstructUIElement(AssetAddresses.SelectedArmoryWeaponItemsContainer);

        public async Task<GameObject> CreateAvailableArmoryWeaponItemsContainer() =>
            await ConstructUIElement(AssetAddresses.AvailableArmoryWeaponItemsContainer);

        public async Task<GameObject> CreateSelectedArmoryWeaponItemsContainer(Transform parent) =>
            await ConstructUIElement(AssetAddresses.SelectedArmoryWeaponItemsContainer, parent);

        public async Task<GameObject> CreateAvailableArmoryWeaponItemsContainer(Transform parent) =>
            await ConstructUIElement(AssetAddresses.AvailableArmoryWeaponItemsContainer, parent);

        public async Task<GameObject> CreateMainUI() =>
            await ConstructUIElement(AssetAddresses.MainUI);

        public async Task<GameObject> CreateHud() =>
            await ConstructUIElement(AssetAddresses.Hud);

        public async Task<GameObject> CreateMapUI() =>
            await ConstructUIElement(AssetAddresses.MapUI);

        public async Task<GameObject> CreateArmoryUI() =>
            await ConstructUIElement(AssetAddresses.ArmoryUI);

        public async Task<GameObject> CreateSettingsUI() =>
            await ConstructUIElement(AssetAddresses.SettingsUI);

        private async Task<GameObject> ConstructUIElement(string path) =>
            await _registratorService.InstantiateRegisteredAsync(path);

        private async Task<GameObject> ConstructUIElement(string path, Transform parent) =>
            await _registratorService.InstantiateRegisteredAsync(path, parent);
    }
}