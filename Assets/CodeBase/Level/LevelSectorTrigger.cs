using System;
using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using Zenject;

namespace CodeBase.Level
{
    public class LevelSectorTrigger : MonoBehaviour, IProgressReader
    {
        [SerializeField] private string _name;

        private const string HeroTag = "Hero";

        private IWindowService _windowService;
        private PlayerProgress _playerProgress;

        public event Action Passed;

        [Inject]
        public void Construct(IWindowService windowService) =>
            _windowService = windowService;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareByTag(HeroTag))
            {
                Time.timeScale = 0;
                _windowService.Open(WindowId.Shop);
                Passed?.Invoke();
                _playerProgress.WorldData.LevelNameData.ChangeSector(_name);
            }
        }

        public void LoadProgress(PlayerProgress progress) =>
            _playerProgress = progress;
    }
}