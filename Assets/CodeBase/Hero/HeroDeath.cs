using System;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.Audio;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Death;
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
        }

        private void OnEnable() =>
            _health.HealthChanged += HealthChanged;

        private void OnDisable() =>
            _health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        public void Die()
        {
            Died?.Invoke();
            AllServices.Container.Single<IAudioService>().LaunchGameEventSound(GameEventSoundId.Death, transform);
            _windowService.Show<DeathWindow>(WindowId.Death);
        }
    }
}