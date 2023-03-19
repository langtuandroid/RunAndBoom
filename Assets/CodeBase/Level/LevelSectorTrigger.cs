using System;
using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Level
{
    public class LevelSectorTrigger : MonoBehaviour
    {
        [SerializeField] private string _name;

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
                Time.timeScale = 0;
                Passed?.Invoke();
                _progressService.Progress.WorldData.LevelNameData.ChangeSector(_name);
                _windowService.Open(WindowId.Shop);
                _isPassed = true;
            }
        }
    }
}