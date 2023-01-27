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
            // GameObject root = await _assets.Instantiate(AssetAddresses.UIRoot);
            // _uiRoot = root.transform;
        }

        public Transform GetUIRoot() => _uiRoot;

        public Task<GameObject> CreateHud() =>
            _registratorService.InstantiateRegisteredAsync(AssetAddresses.Hud);
    }
}