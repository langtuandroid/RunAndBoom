using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Registrator;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private IAssets _assets;
        private IRegistratorService _registratorService;

        private Transform _uiRoot;
        private IPlayerProgressService _playerProgressService;

        public UIFactory(IPlayerProgressService playerProgressService, IAssets assets,
            IRegistratorService registratorService)
        {
            _playerProgressService = playerProgressService;
            _assets = assets;
            _registratorService = registratorService;
        }

        public async Task CreateUIRoot()
        {
            GameObject root = await _assets.Load<GameObject>(AssetAddresses.UIRoot);
            GameObject gameObject = Object.Instantiate(root);
            _uiRoot = gameObject.transform;
            // GameObject root = await _assets.Instantiate(AssetAddresses.UIRoot);
            // _uiRoot = root.transform;
        }

        public Transform GetUIRoot() =>
            _uiRoot;

        public async Task<GameObject> CreateHud() =>
            await _registratorService.InstantiateRegisteredAsync(AssetAddresses.Hud);

        public async Task<GameObject> CreateShopWindow() =>
            await _registratorService.InstantiateRegisteredAsync(AssetAddresses.ShopWindow, _uiRoot);

        public async Task<GameObject> CreateDeathWindow() =>
            await _registratorService.InstantiateRegisteredAsync(AssetAddresses.DeathWindow, _uiRoot);

        public async Task<GameObject> CreateSettingsWindow() =>
            await _registratorService.InstantiateRegisteredAsync(AssetAddresses.SettingsWindow, _uiRoot);
    }
}