using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.Services;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Common;
using CodeBase.UI.Windows.Finish;
using UnityEngine;

namespace CodeBase.Level
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] private GameObject pickupEffect;
        [SerializeField] private int _maxPrice;
        [SerializeField] private Scene _scene;

        private IWindowService _windowService;

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
                WindowBase finishWindow = _windowService.Open<FinishWindow>(WindowId.Finish);
                GiftsGenerator giftsGenerator =
                    (finishWindow as FinishWindow)?.gameObject.GetComponent<GiftsGenerator>();
                giftsGenerator?.Construct(_maxPrice, other.gameObject.GetComponent<HeroHealth>());
                giftsGenerator?.Generate();
                FinishButtons finishButtons = (finishWindow as FinishWindow)?.gameObject.GetComponent<FinishButtons>();
                finishButtons?.Construct(_scene);
            }
        }

        private void Pickup()
        {
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}