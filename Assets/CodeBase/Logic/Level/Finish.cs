using CodeBase.Data;
using CodeBase.Services;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using CodeBase.UI.Windows.Finish;
using UnityEngine;

namespace CodeBase.Logic.Level
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] private GameObject pickupEffect;
        [SerializeField] private int _maxPrice;

        private Scene _scene;

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
                WindowBase finishWindow = _windowService.Show<FinishWindow>(WindowId.Finish);
                GiftsGenerator giftsGenerator =
                    (finishWindow as FinishWindow)?.gameObject.GetComponent<GiftsGenerator>();
                giftsGenerator?.SetMaxPrice(_maxPrice);
                giftsGenerator?.Generate();
                (finishWindow as FinishWindow)?.AddScene(_scene);
            }
        }

        public void Construct(Scene scene) =>
            _scene = scene;

        private void Pickup()
        {
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}