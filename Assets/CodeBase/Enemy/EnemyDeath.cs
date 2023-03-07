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
    [RequireComponent(typeof(AgentMoveToHero))]
    [RequireComponent(typeof(Attack))]
    public class EnemyDeath : MonoBehaviour, IDeath
    {
        [SerializeField] private GameObject _hitBox;
        [SerializeField] private GameObject _diedBox;

        private const float UpForce = 800f;

        private IHealth _health;
        private AgentMoveToHero _agentMoveToHero;
        private TargetMovement _targetMovement;
        private float _deathDelay = 3f;
        private int _reward;
        private bool _isDead;
        private IPlayerProgressService _progressService;

        public event Action Died;

        private void Awake()
        {
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

        [Inject]
        public void Construct(IPlayerProgressService progressService) =>
            _progressService = progressService;

        private void HealthChanged()
        {
            if (!_isDead && _health.Current <= 0)
                Die();
        }

        public void SetReward(int reward) =>
            _reward = reward;

        private void ForceUp()
        {
            _diedBox.GetComponent<Rigidbody>().AddForce(Vector3.up * UpForce, ForceMode.Force);
            StartCoroutine(CoroutineDestroyTimer());
        }

        public void Die()
        {
            _isDead = true;
            Died?.Invoke();

            _progressService.Progress.CurrentLevelStats.ScoreData.AddScore(_reward);
            _agentMoveToHero.Stop();
            _agentMoveToHero.enabled = false;
            _targetMovement.Hide();
            _targetMovement.enabled = false;
            GetComponent<Attack>().enabled = false;
            GetComponent<EnemyAnimator>().enabled = false;
            GetComponent<RotateToHero>().enabled = false;

            _hitBox.SetActive(false);
            _diedBox.SetActive(true);
            ForceUp();
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