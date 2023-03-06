using System;
using CodeBase.Data;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth, IProgressSaver
    {
        private float _max;
        private float _current;
        private PlayerProgress _playerProgress;

        public float Current => _current;
        public float Max => _max;

        public event Action Died;
        public event Action HealthChanged;

        public void Construct() =>
            HealthChanged?.Invoke();

        public void TakeDamage(float damage)
        {
            if (_current <= 0)
                return;

            _current -= damage;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _max = progress.HealthState.MaxHp;
            _current = progress.HealthState.CurrentHp;
            _playerProgress = progress;
            _playerProgress.HealthState.MaxHpChanged += ChangeMaxHp;
            _playerProgress.HealthState.CurrentHpChanged += ChangeCurrentHp;
        }

        private void ChangeMaxHp() =>
            _max = _playerProgress.HealthState.MaxHp;

        private void ChangeCurrentHp() =>
            _current = _playerProgress.HealthState.CurrentHp;

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HealthState.ChangeCurrentHP(_current);
            progress.HealthState.ChangeMaxHP(_max);
        }
    }
}