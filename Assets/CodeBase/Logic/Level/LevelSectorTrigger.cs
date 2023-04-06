using System;
using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Pool;
using CodeBase.Services.Randomizer;
using CodeBase.Services.StaticData;
using CodeBase.UI.Elements.ShopPanel;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Logic.Level
{
    public class LevelSectorTrigger : MonoBehaviour
    {
        [SerializeField] private int _number;
        [SerializeField] private int _refreshCount;

        private IWindowService _windowService;
        private IPlayerProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IRandomService _randomService;
        private IObjectsPoolService _objectsPoolService;
        private bool _isPassed = false;

        public event Action Passed;

        private void Awake()
        {
            _windowService = AllServices.Container.Single<IWindowService>();
            _progressService = AllServices.Container.Single<IPlayerProgressService>();
            _staticDataService = AllServices.Container.Single<IStaticDataService>();
            _randomService = AllServices.Container.Single<IRandomService>();
            _objectsPoolService = AllServices.Container.Single<IObjectsPoolService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareByTag(Constants.HeroTag) && _isPassed == false)
            {
                Passed?.Invoke();
                _progressService.Progress.WorldData.LevelNameData.ChangeSector(_number.ToString());
                WindowBase shopWindow = _windowService.Open<ShopWindow>(WindowId.Shop);
                ShopItemsGenerator shopItemsGenerator = (shopWindow as ShopWindow)?.gameObject.GetComponent<ShopItemsGenerator>();
                shopItemsGenerator?.Construct(_progressService, _staticDataService, _randomService, _objectsPoolService);
                shopItemsGenerator?.CreateAvailableItems();
                _isPassed = true;
            }
        }
    }
}