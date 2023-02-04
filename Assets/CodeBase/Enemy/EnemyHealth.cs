using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float _max;

        private EnemyAnimator _animator;
        private float _current;

        private void Awake()
        {
            _animator = GetComponent<EnemyAnimator>();
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

        public event Action HealthChanged;

        public void TakeDamage(int damage)
        {
            Current -= damage;

            _animator.PlayHit();

            HealthChanged?.Invoke();
        }

        public int DeathPoints { get; set; }
    }
}