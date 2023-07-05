using CodeBase.Data;
using CodeBase.Services;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using CodeBase.UI.Windows.Results;
using UnityEngine;

namespace CodeBase.Logic.Level
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] private GameObject pickupEffect;
        [SerializeField] private int _maxPrice;

        private IWindowService _windowService;
        private Scene _nextLevel;

        private void Awake()
        {
            _windowService = AllServices.Container.Single<IWindowService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareByTag(Constants.HeroTag))
            {
                if (pickupEffect != null)
                    Pickup();

                Time.timeScale = 0;
                WindowBase resultWindow = _windowService.Show<ResultsWindow>(WindowId.Result);
                (resultWindow as ResultsWindow)?.AddData(_nextLevel, _maxPrice);
                (resultWindow as ResultsWindow)?.ShowData();
            }
        }

        public void Construct(Scene nextLevel) =>
            _nextLevel = nextLevel;

        private void Pickup()
        {
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}