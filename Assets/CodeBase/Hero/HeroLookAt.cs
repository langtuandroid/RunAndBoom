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
        private EnemyDeath _death;

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
            _enemy = enemy;
            _death = enemy.GetComponent<EnemyDeath>();
            _death.Died += EnemyDied;
            LookedAtEnemy?.Invoke();
        }

        private void EnemyDied() =>
            _enemy = null;

        private void NotLookAtTarget() =>
            _enemy = null;
    }
}