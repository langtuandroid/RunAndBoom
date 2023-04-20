using System;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroDeath : MonoBehaviour, IDeath
    {
        private IWindowService _windowService;
        private IHealth _health;

        public event Action Died;

        private void Awake()
        {
            _windowService = AllServices.Container.Single<IWindowService>();
            _health = GetComponent<IHealth>();
            _health.HealthChanged += HealthChanged;
        }

        private void HealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        public void Die()
        {
            Died?.Invoke();
            _windowService.Open<DeathWindow>(WindowId.Death);
        }
    }
}