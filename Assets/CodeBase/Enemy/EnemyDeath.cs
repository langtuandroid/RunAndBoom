using System;
using System.Collections;
using CodeBase.Enemy.Attacks;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Elements.Enemy;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(AgentMoveToHero))]
    [RequireComponent(typeof(Attack))]
    public class EnemyDeath : MonoBehaviour, IDeath
    {
        [SerializeField] private GameObject _hitBox;
        [SerializeField] private GameObject _diedBox;

        private const float UpForce = 100f;

        private IPlayerProgressService _progressService;
        private IHealth _health;
        private AgentMoveToHero _agentMoveToHero;
        private TargetMovement _targetMovement;
        private float _deathDelay = 30f;
        private int _reward;
        private bool _isDead;
        private EnemyAnimator _enemyAnimator;

        public event Action Died;

        private void Awake()
        {
            _progressService = AllServices.Container.Single<IPlayerProgressService>();

            _enemyAnimator = GetComponent<EnemyAnimator>();
            _agentMoveToHero = GetComponent<AgentMoveToHero>();
            _targetMovement = GetComponentInChildren<TargetMovement>();
            _health = GetComponent<IHealth>();
            _health.HealthChanged += HealthChanged;
        }

        private void Start()
        {
            _diedBox.SetActive(false);
        }

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
            _isDead = true;
            Died?.Invoke();

            _progressService.Progress.CurrentLevelStats.MoneyData.AddMoney(_reward);
            GetComponent<Aggro>().enabled = false;
            GetComponent<StopMovingOnAttack>().enabled = false;
            GetComponent<CheckAttackRange>().enabled = false;
            _agentMoveToHero.Stop();
            _targetMovement.Hide();
            _enemyAnimator.PlayDeath();

            // _hitBox.SetActive(false);
            // _diedBox.SetActive(true);
            GetComponent<Rigidbody>().AddForce(Vector3.up * UpForce, ForceMode.Force);
            StartCoroutine(CoroutineDestroyTimer());
            // _diedBox.enabled = true;
            // _hitBox.enabled = false;
        }

        private IEnumerator CoroutineDestroyTimer()
        {
            yield return new WaitForSeconds(_deathDelay);
            Destroy(gameObject);
        }
    }
}