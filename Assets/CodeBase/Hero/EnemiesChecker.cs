using System;
using System.Collections.Generic;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Hero
{
    public class EnemiesChecker : MonoBehaviour
    {
        [SerializeField] private float _sphereRadius = 20f;
        [SerializeField] private float _maxDistance = 0f;
        [SerializeField] private int _enemiesHitsCount = 20;
        [SerializeField] private LayerMask _enemyLayerMask;
        [SerializeField] private LayerMask _visibleObstaclesLayerMask;

        private HeroRotating _heroRotating;

        public event Action<EnemyHealth> FoundClosestEnemy;
        public event Action<EnemyHealth> DirectionForEnemyChecked;

        private void Awake()
        {
            _heroRotating = GetComponent<HeroRotating>();
            _heroRotating.ShootDirection += CheckDirectionForEnemy;
        }

        private void FixedUpdate() =>
            CheckEnemiesAround();

        private void CheckEnemiesAround()
        {
            List<EnemyHealth> enemies = new List<EnemyHealth>();
            RaycastHit[] enemiesHits = new RaycastHit[_enemiesHitsCount];
            int enemiesHitsCount = GetEnemiesHits(enemiesHits);

            CheckEnemiesHits(enemiesHitsCount, enemiesHits, enemies);
        }

        private int GetEnemiesHits(RaycastHit[] enemiesHits) =>
            Physics.SphereCastNonAlloc(transform.position, _sphereRadius, transform.forward, enemiesHits, _maxDistance, _enemyLayerMask,
                QueryTriggerInteraction.UseGlobal);

        private void CheckEnemiesHits(int enemiesHitsCount, RaycastHit[] enemiesHits,
            List<EnemyHealth> visibleEnemies)
        {
            if (enemiesHitsCount > 0)
            {
                for (int i = 0; i < enemiesHitsCount; i++)
                {
                    EnemyHealth enemyHealth = enemiesHits[i].transform.parent.gameObject.GetComponent<EnemyHealth>();

                    if (enemiesHits.Length == 0 && enemyHealth.Current > 0)
                        visibleEnemies.Add(enemyHealth);
                }

                if (visibleEnemies.Count > 0)
                {
                    EnemyHealth closestEnemy = FindClosestEnemy(visibleEnemies);
                    FoundClosestEnemy?.Invoke(closestEnemy);
                }
            }
        }

        private EnemyHealth FindClosestEnemy(List<EnemyHealth> visibleEnemies)
        {
            EnemyHealth closestEnemy = visibleEnemies[0];
            float minDistance =
                Vector3.Distance(visibleEnemies[0].transform.position, transform.position);

            foreach (EnemyHealth enemy in visibleEnemies)
            {
                float toEnemyDistance =
                    Vector3.Distance(enemy.transform.position, transform.position);

                if (toEnemyDistance < minDistance)
                    closestEnemy = enemy;
            }

            return closestEnemy;
        }

        private void CheckDirectionForEnemy(Vector3 playerPosition)
        {
            Vector3 direction = (playerPosition - transform.position).normalized;
            float distance = Vector3.Distance(playerPosition, transform.position) + 10f;
            RaycastHit[] raycastHits = Physics.RaycastAll(transform.position, direction, distance,
                _visibleObstaclesLayerMask, QueryTriggerInteraction.UseGlobal);

            EnemyHealth enemyHealth = null;

            if (raycastHits.Length > 0)
                enemyHealth = raycastHits[0].transform.parent.gameObject.GetComponent<EnemyHealth>();

            DirectionForEnemyChecked?.Invoke(enemyHealth);
        }
    }
}