using System;
using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.StaticData.ProjectileTrace;
using CodeBase.StaticData.Weapon;
using CodeBase.UI.Elements.Hud;
using UnityEngine;

namespace CodeBase.Hero
{
    public class EnemiesChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask _enemyLayerMask;
        [SerializeField] private LayerMask _visibleObstaclesLayerMask;

        private HeroWeaponSelection _heroWeaponSelection;

        private int _enemiesHitsCount = 10;
        private float _sphereDistance = 0f;
        private float _distanceToEnemy = 0f;
        private float _aimRange = 1f;
        private float _checkEnemiesDelay = 0.2f;
        private float _checkEnemiesTimer = 0f;
        private List<EnemyHealth> _enemies = new List<EnemyHealth>();
        private EnemyHealth _targetEnemy = null;
        private Vector3 _targetPosition;

        public event Action<EnemyHealth> FoundClosestEnemy;
        public event Action EnemyNotFound;

        private void Awake()
        {
            _heroWeaponSelection = transform.gameObject.GetComponentInChildren<HeroWeaponSelection>();
            _heroWeaponSelection.WeaponSelected += SetWeaponAimRange;
        }

        private void SetWeaponAimRange(GameObject weaponPrefab, WeaponStaticData weaponStaticData, ProjectileTraceStaticData projectileTraceStaticData) =>
            _aimRange = weaponStaticData.AimRange;

        private void FixedUpdate()
        {
            UpFixedTime();

            if (IsCheckEnemiesTimerReached())
                CheckEnemiesAround();
        }

        private void UpFixedTime() =>
            _checkEnemiesTimer += Time.fixedDeltaTime;

        private bool IsCheckEnemiesTimerReached() =>
            _checkEnemiesTimer >= _checkEnemiesDelay;

        private void CheckEnemiesAround()
        {
            _checkEnemiesTimer = 0f;
            _enemies.Clear();
            RaycastHit[] enemiesHits = new RaycastHit[_enemiesHitsCount];
            int enemiesHitsCount = GetEnemiesHits(enemiesHits);

            CheckEnemiesHits(enemiesHitsCount, enemiesHits);
        }

        private int GetEnemiesHits(RaycastHit[] enemiesHits) =>
            Physics.SphereCastNonAlloc(transform.position, _aimRange, transform.forward, enemiesHits, _sphereDistance, _enemyLayerMask,
                QueryTriggerInteraction.UseGlobal);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Debug.DrawLine(transform.position, transform.position + Vector3.forward * _distanceToEnemy);
            Gizmos.DrawWireSphere(transform.position + Vector3.forward * _sphereDistance, _aimRange);
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
                else
                    EnemyNotFound?.Invoke();
            }
        }

        private void FindClosestEnemy(List<EnemyHealth> visibleEnemies)
        {
            EnemyHealth closestEnemy = GetClosestEnemy(visibleEnemies);

            if (closestEnemy != null)
            {
                if (_targetEnemy != closestEnemy || _targetEnemy == null)
                {
                    _targetEnemy = closestEnemy;
                    _targetPosition = new Vector3(closestEnemy.transform.position.x, closestEnemy.transform.position.y, closestEnemy.transform.position.z);
                    CheckEnemyVisibility(closestEnemy);
                }

                if (_targetEnemy == closestEnemy && _targetPosition != closestEnemy.transform.position)
                    CheckEnemyVisibility(closestEnemy);
            }
            else
                EnemyNotFound?.Invoke();
        }

        private EnemyHealth GetClosestEnemy(List<EnemyHealth> visibleEnemies)
        {
            float minDistance = _aimRange;

            if (visibleEnemies.Count > 0)
            {
                EnemyHealth closestEnemy = null;

                foreach (EnemyHealth enemy in visibleEnemies)
                {
                    _distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);

                    if (_distanceToEnemy < minDistance)
                    {
                        minDistance = _distanceToEnemy;
                        closestEnemy = enemy;
                    }
                }

                return closestEnemy;
            }

            return null;
        }

        private void CheckEnemyVisibility(EnemyHealth enemy)
        {
            Vector3 direction = (enemy.gameObject.transform.position - transform.position).normalized;
            RaycastHit[] raycastHits = Physics.RaycastAll(transform.position, direction, _distanceToEnemy, _visibleObstaclesLayerMask,
                QueryTriggerInteraction.UseGlobal);

            if (raycastHits.Length == 0)
            {
                TurnOffAnotherTargets(_enemies);
                TurnOnTarget();
                FoundClosestEnemy?.Invoke(enemy);
            }
            else
                EnemyNotFound?.Invoke();
        }

        private void TurnOffAnotherTargets(List<EnemyHealth> visibleEnemies)
        {
            foreach (EnemyHealth enemy in visibleEnemies)
                if (_targetEnemy != enemy)
                    enemy.transform.GetComponentInChildren<Target>().Hide();
        }

        private void TurnOnTarget() =>
            _targetEnemy.transform.GetComponentInChildren<Target>().Show();
    }
}