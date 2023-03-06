using System;
using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using Zenject;

namespace CodeBase.Level
{
    public class LevelSectorTrigger : MonoBehaviour
    {
        [SerializeField] private string _name;

        private IWindowService _windowService;
        private IPlayerProgressService _progressService;

        public event Action Passed;

        [Inject]
        public void Construct(IWindowService windowService, IPlayerProgressService progressService)
        {
            _windowService = windowService;
            _progressService = progressService;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareByTag(Constants.HeroTag))
            {
                // Time.timeScale = 0;
                // _windowService.Open(WindowId.Shop);
                Passed?.Invoke();
                _progressService.Progress.WorldData.LevelNameData.ChangeSector(_name);
            }
        }
    }
}