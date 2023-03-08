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
            GameObject root = await _assets.Instantiate(AssetAddresses.UIRoot);
            _uiRoot = root.transform;
        }

        public Transform GetUIRoot() =>
            _uiRoot;

        public async Task<GameObject> CreateHud() =>
            await _registratorService.InstantiateRegisteredAsync(AssetAddresses.Hud);
    }
}