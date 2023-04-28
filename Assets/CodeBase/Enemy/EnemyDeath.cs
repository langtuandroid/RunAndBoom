using System;
using System.Collections;
using CodeBase.Enemy.Attacks;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(Attack))]
    public class EnemyDeath : MonoBehaviour, IDeath
    {
        [SerializeField] private GameObject _diedBox;

        private Rigidbody _rigidbody;

        private const float UpForce = 100f;
        private const float DestroyDelay = 30f;

        private IPlayerProgressService _progressService;
        private IHealth _health;
        private AgentMoveToHero _agentMoveToHero;

        // private TargetMovement _targetMovement;
        private int _reward;
        private bool _isDead;
        private EnemyAnimator _enemyAnimator;

        public event Action Died;

        private void Awake()
        {
            _progressService = AllServices.Container.Single<IPlayerProgressService>();

            _enemyAnimator = GetComponent<EnemyAnimator>();
            _agentMoveToHero = GetComponent<AgentMoveToHero>();
            // _targetMovement = GetComponentInChildren<TargetMovement>();
            _health = GetComponent<IHealth>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable() =>
            _health.HealthChanged += HealthChanged;

        private void OnDisable() =>
            _health.HealthChanged -= HealthChanged;

        private void Start() =>
            _diedBox.SetActive(false);

        private void OnDestroy() =>
            _health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (!_isDead && _health.Current <= 0)
                Die();
        }

        public void SetReward(int reward) =>
            _reward = reward;

        public void Die()
        {
            Died?.Invoke();
            _isDead = true;
            // _rigidbody.isKinematic = false;

            _progressService.Progress.CurrentLevelStats.MoneyData.AddMoney(_reward);
            _enemyAnimator.PlayDeath();
            _agentMoveToHero.Stop();
            GetComponent<Rigidbody>().AddForce(Vector3.up * UpForce, ForceMode.Force);
            StartCoroutine(CoroutineDestroyTimer());
            Destroy(GetComponent<RotateToHero>());
            Destroy(GetComponent<Aggro>());
            Destroy(GetComponent<AnimateAlongAgent>());
            Destroy(GetComponent<CheckAttackRange>());
            Destroy(GetComponent<StopMovingOnAttack>());
            Destroy(GetComponent<NavMeshAgent>(), 1);
            Destroy(GetComponent<AgentMoveToHero>());
            // _targetMovement.Hide();
            // _hitBox.SetActive(false);
            // _diedBox.SetActive(true);
            // _diedBox.enabled = true;
            // _hitBox.enabled = false;
        }

        private IEnumerator CoroutineDestroyTimer()
        {
            yield return new WaitForSeconds(DestroyDelay);
            Destroy(gameObject);
        }
    }
}