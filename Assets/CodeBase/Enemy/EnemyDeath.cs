using System;
using System.Collections;
using CodeBase.Enemy.Attacks;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Elements.Hud;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AgentMoveToHero))]
    [RequireComponent(typeof(Attack))]
    public class EnemyDeath : MonoBehaviour, IDeath
    {
        [SerializeField] private BoxCollider _hitBox;
        [SerializeField] private BoxCollider _diedBox;

        private const float UpForce = 500f;

        private IHealth _health;
        private AgentMoveToHero _agentMoveToHero;
        private Rigidbody _rigidbody;
        private float _deathDelay = 3f;
        private int _reward;
        private bool _isDead;
        private IPlayerProgressService _progressService;

        public event Action Died;

        private void Awake()
        {
            _agentMoveToHero = GetComponent<AgentMoveToHero>();
            _rigidbody = GetComponent<Rigidbody>();
            _health = GetComponent<IHealth>();
            _health.HealthChanged += HealthChanged;
        }

        private void Start()
        {
            _diedBox.enabled = false;
        }

        private void OnDestroy() =>
            _health.HealthChanged -= HealthChanged;

        [Inject]
        public void Construct(IPlayerProgressService progressService)
        {
            _progressService = progressService;
        }

        private void HealthChanged()
        {
            if (!_isDead && _health.Current <= 0)
                Die();
        }

        public void SetReward(int reward) =>
            _reward = reward;

        private void ForceUp()
        {
            transform.GetComponentInChildren<TargetMovement>().Hide();
            _rigidbody.AddForce(Vector3.up * UpForce, ForceMode.Force);
            StartCoroutine(CoroutineDestroyTimer());
        }

        public void Die()
        {
            _isDead = true;
            Died?.Invoke();

            ForceUp();
            _progressService.Progress.CurrentLevelStats.ScoreData.AddScore(_reward);
            _agentMoveToHero.Stop();
            _agentMoveToHero.enabled = false;

            GetComponent<Attack>().enabled = false;
            _diedBox.enabled = true;
            // _hitBox.enabled = false;
            GetComponent<EnemyAnimator>().enabled = false;
            GetComponent<RotateToHero>().enabled = false;
        }

        private IEnumerator CoroutineDestroyTimer()
        {
            yield return new WaitForSeconds(_deathDelay);
            Destroy(gameObject);
        }
    }
}