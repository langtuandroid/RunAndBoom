using System;
using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Common;
using CodeBase.UI.Windows.Shop;
using UnityEngine;

namespace CodeBase.Logic.Level
{
    public class LevelSectorTrigger : MonoBehaviour
    {
        [SerializeField] private int _number;
        [SerializeField] private int _refreshCount;
        [SerializeField] private int _watchAdsNumber;

        private IWindowService _windowService;
        private IPlayerProgressService _progressService;
        private bool _isPassed = false;

        public event Action Passed;

        private void Awake()
        {
            _windowService = AllServices.Container.Single<IWindowService>();
            _progressService = AllServices.Container.Single<IPlayerProgressService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareByTag(Constants.HeroTag) && _isPassed == false)
            {
                Passed?.Invoke();
                _progressService.Progress.WorldData.LevelNameData.ChangeSector(_number.ToString());
                WindowBase shopWindow = _windowService.Open<ShopWindow>(WindowId.Shop);
                (shopWindow as ShopWindow)?.gameObject.GetComponent<ShopItemsGenerator>()?.Generate();
                ShopButtons shopButtons = (shopWindow as ShopWindow)?.gameObject.GetComponent<ShopButtons>();
                shopButtons?.Construct(_refreshCount, _watchAdsNumber);
                _isPassed = true;
            }
        }
    }
}