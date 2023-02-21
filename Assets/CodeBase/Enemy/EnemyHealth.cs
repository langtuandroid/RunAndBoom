using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float _max;

        private EnemyAnimator _animator;
        private float _previousCurrent;
        private float _current;

        private void Awake()
        {
            // _animator = GetComponent<EnemyAnimator>();
            _current = _max;
        }

        public int Current
        {
            get => (int)_current;
            set => _current = value;
        }

        public int Max
        {
            get => (int)_max;
            set => _max = value;
        }

        public event Action Died;
        public event Action HealthChanged;

        public void TakeDamage(int damage)
        {
            _previousCurrent = _current;
            _current -= damage;

            if (_current <= 0 && _previousCurrent > 0)
            {
                Debug.Log($"died {transform.gameObject.name}");
                Died?.Invoke();
            }

            if (_current > 0)
            {
                // _animator.PlayHit();
                HealthChanged?.Invoke();
            }
        }
    }
}