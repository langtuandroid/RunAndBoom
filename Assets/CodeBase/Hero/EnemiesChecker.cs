using System;
using System.Collections.Generic;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Hero
{
    public class EnemiesChecker : MonoBehaviour
    {
        [SerializeField] private float _sphereRadius = 20f;
        [SerializeField] private float _maxDistance = 20f;
        [SerializeField] private int _enemiesHitsCount = 20;
        [SerializeField] private LayerMask _enemyLayerMask;
        [SerializeField] private LayerMask _visibleObstaclesLayerMask;

        private GameObject _rotatingBody;
        private HeroRotating _heroRotating;
        private HeroShooting _heroShooting;

        public event Action<EnemyHealth> FoundClosestEnemy;
        public event Action<EnemyHealth> DirectionForEnemyChecked;

        private void Awake()
        {
            _rotatingBody = transform.GetChild(0).gameObject;
            _heroRotating = GetComponent<HeroRotating>();
            _heroShooting = GetComponent<HeroShooting>();
            _heroRotating.ShootDirection += CheckDirectionForEnemy;
        }

        private void FixedUpdate()
        {
            CheckMonstersAround();
        }

        private void CheckMonstersAround()
        {
            List<EnemyHealth> visibleEnemies = new List<EnemyHealth>();
            RaycastHit[] enemiesHits = new RaycastHit[_enemiesHitsCount];
            int enemiesHitsCount = GetEnemiesHits(enemiesHits);

            CheckEnemiesHits(enemiesHitsCount, enemiesHits, visibleEnemies);
        }

        private int GetEnemiesHits(RaycastHit[] enemiesHits) =>
            Physics.SphereCastNonAlloc(_rotatingBody.transform.position, _sphereRadius,
                _rotatingBody.transform.forward, enemiesHits, _maxDistance, _enemyLayerMask,
                QueryTriggerInteraction.UseGlobal);

        private void CheckEnemiesHits(int enemiesHitsCount, RaycastHit[] enemiesHits,
            List<EnemyHealth> visibleEnemies)
        {
            if (enemiesHitsCount > 0)
            {
                for (int i = 0; i < enemiesHitsCount; i++)
                {
                    EnemyHealth monsterHit = CheckMonsterVisibility(enemiesHits, i, out var raycastHits);

                    if (raycastHits.Length == 0 && monsterHit.Current > 0)
                        visibleEnemies.Add(monsterHit);
                }

                if (visibleEnemies.Count > 0)
                {
                    EnemyHealth closestEnemy = FindClosestEnemy(visibleEnemies);
                    FoundClosestEnemy?.Invoke(closestEnemy);
                }
            }
        }

        private EnemyHealth CheckMonsterVisibility(RaycastHit[] enemiesHits, int i, out RaycastHit[] raycastHits)
        {
            EnemyHealth monsterHit = enemiesHits[i].transform.parent.gameObject.GetComponent<EnemyHealth>();
            Vector3 direction = (monsterHit.transform.position - _rotatingBody.transform.position).normalized;
            float distance = Vector3.Distance(monsterHit.transform.position, _rotatingBody.transform.position);
            raycastHits = Physics.RaycastAll(_rotatingBody.transform.position, direction, distance,
                _visibleObstaclesLayerMask,
                QueryTriggerInteraction.UseGlobal);
            return monsterHit;
        }

        private EnemyHealth FindClosestEnemy(List<EnemyHealth> visibleEnemies)
        {
            EnemyHealth closestEnemy = visibleEnemies[0];
            float minDistance =
                Vector3.Distance(visibleEnemies[0].transform.position, _rotatingBody.transform.position);

            foreach (EnemyHealth enemy in visibleEnemies)
            {
                float toEnemyDistance =
                    Vector3.Distance(enemy.transform.position, _rotatingBody.transform.position);

                if (toEnemyDistance < minDistance)
                    closestEnemy = enemy;
            }

            return closestEnemy;
        }

        private void CheckDirectionForEnemy(Vector3 playerPosition)
        {
            Vector3 direction = (playerPosition - _rotatingBody.transform.position).normalized;
            float distance = Vector3.Distance(playerPosition, _rotatingBody.transform.position) + 10f;
            RaycastHit[] raycastHits = Physics.RaycastAll(_rotatingBody.transform.position, direction, distance,
                _visibleObstaclesLayerMask, QueryTriggerInteraction.UseGlobal);

            EnemyHealth enemyHealth = null;

            if (raycastHits.Length > 0)
                enemyHealth = raycastHits[0].transform.parent.gameObject.GetComponent<EnemyHealth>();

            DirectionForEnemyChecked?.Invoke(enemyHealth);
        }
    }
}