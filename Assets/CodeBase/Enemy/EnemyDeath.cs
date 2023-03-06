using System.Collections;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Elements.Hud;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour, IDeath
    {
        private const float UpForce = 500f;

        private AgentMoveToHero _agentMoveToHero;
        private IHealth _health;
        private Rigidbody _rigidbody;
        private float _deathDelay = 5f;
        private int _reward;
        private bool _isDead;

        private IPlayerProgressService _progressService;

        private void Awake()
        {
            _agentMoveToHero = GetComponent<AgentMoveToHero>();
            _rigidbody = GetComponent<Rigidbody>();
            _health = GetComponent<IHealth>();
            _health.Died += ForceUp;
        }

        private void Start() =>
            _health.HealthChanged += HealthChanged;

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
            _agentMoveToHero.Stop();
            GetComponentInChildren<BoxCollider>().enabled = false;
            _progressService.Progress.CurrentLevelStats.ScoreData.AddScore(_reward);
        }

        private IEnumerator CoroutineDestroyTimer()
        {
            yield return new WaitForSeconds(_deathDelay);
            Destroy(gameObject);
        }
    }
}