using System.Threading.Tasks;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        Task CreateUIRoot();
        Transform GetUIRoot();
        Task<GameObject> CreateAvailableArmoryWeaponItem(Transform parent);
        Task<GameObject> CreateSelectedArmoryWeaponItem(Transform parent);
        Task<GameObject> CreateLevelWeaponItem(Transform parent);
        Task<GameObject> CreateSelectedArmoryWeaponItemsContainer(Transform parent);
        Task<GameObject> CreateAvailableArmoryWeaponItemsContainer(Transform parent);
        Task<GameObject> CreateSelectedArmoryWeaponItemsContainer();
        Task<GameObject> CreateAvailableArmoryWeaponItemsContainer();
        Task<GameObject> CreateMainUI();
        Task<GameObject> CreateHud();
        Task<GameObject> CreateMapUI();
        Task<GameObject> CreateArmoryUI();
        Task<GameObject> CreateSettingsUI();
    }
}