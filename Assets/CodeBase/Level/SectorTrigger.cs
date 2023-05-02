using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Shop;
using UnityEngine;

namespace CodeBase.Level
{
    public class SectorTrigger : MonoBehaviour
    {
        [SerializeField] private string _name;

        private IWindowService _windowService;
        private IPlayerProgressService _progressService;
        private bool _isPassed = false;

        private void Awake()
        {
            _windowService = AllServices.Container.Single<IWindowService>();
            _progressService = AllServices.Container.Single<IPlayerProgressService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareByTag(Constants.HeroTag) && _isPassed == false)
            {
                Time.timeScale = 0;
                _progressService.Progress.WorldData.LevelNameData.ChangeSector(_name);
                _windowService.Open<ShopWindow>(WindowId.Shop);
                _isPassed = true;
            }
        }
    }
}