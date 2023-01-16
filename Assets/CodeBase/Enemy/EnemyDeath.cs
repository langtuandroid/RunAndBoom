using System;
using System.Collections;
using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour, IProgressSaver
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private GameObject _deathVFX;

        private int _deathPoints;
        private LevelStats _levelStats;

        public event Action Died;

        private void Start() =>
            _health.HealthChanged += HealthChanged;

        private void OnDestroy() =>
            _health.HealthChanged -= HealthChanged;

        public void Construct(int deathPoints)
        {
            _deathPoints = deathPoints;
        }

        private void HealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _health.HealthChanged -= HealthChanged;
            _animator.PlayDeath();
            SpawnDeathVfx();
            StartCoroutine(DestroyTimer());
            _levelStats.ScoreData.AddScore(_deathPoints);
            Died?.Invoke();
        }

        private void SpawnDeathVfx() =>
            Instantiate(_deathVFX, transform.position, Quaternion.identity);

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(0f);
            Destroy(gameObject);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _levelStats = progress.CurrentLevelStats;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }
    }
}