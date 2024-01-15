using CodeBase.Data.Progress;
using CodeBase.Hero;
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
        private SceneId _nextLevel;
        private SceneId _currentLevel;
        private AreaTriggersChecker _areaTriggersChecker;

        private void Awake()
        {
            _windowService = AllServices.Container.Single<IWindowService>();
            _areaTriggersChecker = GetComponent<AreaTriggersChecker>();
            _areaTriggersChecker.Cleared += ShowWindow;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HeroHealth health))
                _areaTriggersChecker.CheckTriggersForEnemies();
        }

        public void Construct(SceneId currentLevel, SceneId nextLevel)
        {
            _currentLevel = currentLevel;
            _nextLevel = nextLevel;
        }

        private void ShowWindow()
        {
            _areaTriggersChecker.Open();

            if (pickupEffect != null)
                Pickup();

            WindowBase resultWindow = _windowService.Show<ResultsWindow>(WindowId.Result);
            (resultWindow as ResultsWindow)?.AddData(_currentLevel, _nextLevel, _maxPrice);
            (resultWindow as ResultsWindow)?.CalculateScore();
            (resultWindow as ResultsWindow)?.ShowData();
        }

        private void Pickup()
        {
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}