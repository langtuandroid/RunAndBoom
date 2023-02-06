using System;
using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.StaticData.Weapon;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroAiming : MonoBehaviour
    {
        [SerializeField] private LayerMask _enemyLayerMask;
        [SerializeField] private LayerMask _visibleObstaclesLayerMask;

        private HeroRotating _heroRotating;
        private HeroWeaponSelection _heroWeaponSelection;

        private int _enemiesHitsCount = 10;
        private float _sphereDistance = 0f;
        private float _distanceToEnemy = 0f;
        private float _sphereRadius = 1f;
        private float _checkEnemiesDelay = 0.1f;
        private float _fixedDeltaTimeCounter = 0;
        private List<EnemyHealth> _enemies = new List<EnemyHealth>();

        public event Action<EnemyHealth> FoundClosestEnemy;
        public event Action EnemyVisibilityChecked;

        [Inject]
        public void Construct()
        {
            _heroWeaponSelection = GetComponent<HeroWeaponSelection>();
            _heroRotating = GetComponent<HeroRotating>();
            _heroWeaponSelection.WeaponSelected += SetWeaponRange;
            _heroRotating.ShootDirection += CheckEnemyVisibility;
        }

        private void SetWeaponRange(WeaponStaticData weaponStaticData, Transform transform) =>
            _sphereRadius = weaponStaticData.Range;

        private void FixedUpdate()
        {
            UpFixedTime();

            if (CheckFixedTimeCounter())
                CheckEnemiesAround();
        }

        private void UpFixedTime() =>
            _fixedDeltaTimeCounter += Time.fixedDeltaTime;

        private bool CheckFixedTimeCounter() =>
            _fixedDeltaTimeCounter >= _checkEnemiesDelay;

        private void CheckEnemiesAround()
        {
            _fixedDeltaTimeCounter = 0f;
            _enemies.Clear();
            RaycastHit[] enemiesHits = new RaycastHit[_enemiesHitsCount];
            int enemiesHitsCount = GetEnemiesHits(enemiesHits);

            CheckEnemiesHits(enemiesHitsCount, enemiesHits);
        }

        private int GetEnemiesHits(RaycastHit[] enemiesHits) =>
            Physics.SphereCastNonAlloc(transform.position, _sphereRadius, transform.forward, enemiesHits, _sphereDistance, _enemyLayerMask,
                QueryTriggerInteraction.UseGlobal);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Debug.DrawLine(transform.position, transform.position + Vector3.forward * _distanceToEnemy);
            Gizmos.DrawWireSphere(transform.position + Vector3.forward * _sphereDistance, _sphereRadius);
        }

        private void CheckEnemiesHits(int enemiesHitsCount, RaycastHit[] enemiesHits)
        {
            if (enemiesHitsCount > 0)
            {
                for (int i = 0; i < enemiesHitsCount; i++)
                {
                    EnemyHealth enemyHealth = enemiesHits[i].transform.gameObject.GetComponent<EnemyHealth>();

                    if (enemyHealth.Current > 0)
                        _enemies.Add(enemyHealth);
                }

                if (_enemies.Count > 0)
                    FindClosestEnemy(_enemies);
            }
        }

        private void FindClosestEnemy(List<EnemyHealth> visibleEnemies)
        {
            EnemyHealth closestEnemy = visibleEnemies[0];
            float minDistance = Vector3.Distance(visibleEnemies[0].transform.position, transform.position);

            foreach (EnemyHealth enemy in visibleEnemies)
            {
                float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);

                if (distanceToEnemy < minDistance)
                    closestEnemy = enemy;
            }

            FoundClosestEnemy?.Invoke(closestEnemy);
        }

        private void CheckEnemyVisibility(Vector3 enemyPosition)
        {
            Vector3 direction = (enemyPosition - transform.position).normalized;
            _distanceToEnemy = Vector3.Distance(enemyPosition, transform.position);
            RaycastHit[] raycastHits = Physics.RaycastAll(transform.position, direction, _distanceToEnemy, _visibleObstaclesLayerMask,
                QueryTriggerInteraction.UseGlobal);

            if (raycastHits.Length == 0)
                EnemyVisibilityChecked?.Invoke();
        }
    }
}