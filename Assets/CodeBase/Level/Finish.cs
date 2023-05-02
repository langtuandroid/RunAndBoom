using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.FinishLevel;
using CodeBase.UI.Windows.Shop;
using UnityEngine;

namespace CodeBase.Level
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] private GameObject pickupEffect;

        private IWindowService _windowService;

        private void Awake()
        {
            _windowService = AllServices.Container.Single<IWindowService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareByTag(Constants.HeroTag))
            {
                Pickup();
                Time.timeScale = 0;
                _windowService.Open<FinishWindow>(WindowId.Finish);
            }
        }

        private void Pickup()
        {
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}