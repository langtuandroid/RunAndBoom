using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float _max;

        private float _previousCurrent;
        private float _current;

        public float Current => _current;
        public float Max => _max;

        private void Awake() =>
            _current = _max;

        public void SetMaxHealth(int max) =>
            _max = max;

        public event Action HealthChanged;

        public void TakeDamage(float damage)
        {
            _current -= damage;
            HealthChanged?.Invoke();
        }
    }
}