using System;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroLookAt : MonoBehaviour
    {
        [SerializeField] private HeroRotating _heroRotating;
        [SerializeField] private EnemiesChecker _enemiesChecker;

        private GameObject _enemy;
        private EnemyHealth _health;

        private Coroutine _lookAtCoroutine;

        public event Action LookedAtEnemy;

        private void Awake()
        {
            _heroRotating.EndedRotatingToEnemy += LookAt;
            _heroRotating.StartedRotating += NotLookAtTarget;
            _enemiesChecker.EnemyNotFound += NotLookAtTarget;
        }

        private void Update()
        {
            if (_enemy)
            {
                var position = _enemy.transform.position;
                transform.LookAt(new Vector3(position.x, position.y + Constants.AdditionYToEnemy, position.z));
            }
        }

        private void LookAt(GameObject enemy)
        {
            Debug.Log($"LookAt {enemy.transform.position}");
            _enemy = enemy;
            _health = enemy.GetComponent<EnemyHealth>();
            _health.Died += EnemyDied;
            LookedAtEnemy?.Invoke();
        }

        private void EnemyDied()
        {
            Debug.Log("EnemyDied");
            _enemy = null;
        }

        private void NotLookAtTarget() =>
            _enemy = null;
    }
}