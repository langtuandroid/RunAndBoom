using System.Threading.Tasks;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        Task CreateUIRoot();
        Transform GetUIRoot();
        Task<GameObject> CreateHud();
        Task<GameObject> CreateShopWindow();
        Task<GameObject> CreateDeathWindow();
        Task<GameObject> CreateSettingsWindow();
        Task<GameObject> CreateGiftsWindow();
        Task<GameObject> CreateResultsWindow();
        Task<GameObject> CreateGameEndWindow();
    }
}