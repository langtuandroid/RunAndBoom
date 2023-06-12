using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.Registrator;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private IAssets _assets;
        private IRegistratorService _registratorService;

        private Transform _uiRoot;

        public UIFactory(IAssets assets, IRegistratorService registratorService)
        {
            _assets = assets;
            _registratorService = registratorService;
        }

        public async Task CreateUIRoot()
        {
            GameObject root = await _assets.Load<GameObject>(AssetAddresses.UIRoot);
            GameObject gameObject = Object.Instantiate(root);
            _uiRoot = gameObject.transform;
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

        public async Task<GameObject> CreateFinishWindow() =>
            await _registratorService.InstantiateRegisteredAsync(AssetAddresses.FinishWindow, _uiRoot);

        public async Task<GameObject> CreateTrainingWindow() =>
            await _registratorService.InstantiateRegisteredAsync(AssetAddresses.TrainingWindow, _uiRoot);

        public async Task<GameObject> CreateResultsWindow() =>
            await _registratorService.InstantiateRegisteredAsync(AssetAddresses.ResultsWindow, _uiRoot);

        public async Task<GameObject> CreateGameEndWindow() =>
            await _registratorService.InstantiateRegisteredAsync(AssetAddresses.GameEndWindow, _uiRoot);
    }
}