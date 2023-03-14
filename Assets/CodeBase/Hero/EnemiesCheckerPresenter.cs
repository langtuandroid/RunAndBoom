using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Logic;
using CodeBase.UI.Elements.Enemy;
using UnityEngine;

namespace CodeBase.Hero
{
    public class EnemiesCheckerPresenter : IPresenter
    {
        private EnemiesCheckerView _view;
        private readonly Transform _transform;
        private int _enemiesHitsCount = 10;
        private float _sphereDistance = 0f;
        private float _distanceToEnemy = 0f;
        private float _aimRange = 1f;
        private float _checkEnemiesDelay = 0.2f;
        private float _checkEnemiesTimer = 0f;
        private List<EnemyHealth> _enemies = new List<EnemyHealth>();
        private string _targetEnemyId = null;
        private EnemyHealth _targetEnemy = null;
        private string _enemyId = null;
        private bool _enemyNotFound = false;
        private int _enemyLayerMask;
        private int _visibleObstaclesLayerMask;

        public EnemiesCheckerPresenter(EnemiesCheckerView view, Transform transform, int enemyLayerMask, int visibleObstaclesLayerMask)
        {
            _view = view;
            _transform = transform;
            _enemyLayerMask = enemyLayerMask;
            _visibleObstaclesLayerMask = visibleObstaclesLayerMask;
        }

        public void SetWeaponAimRange(float aimRange) =>
            _aimRange = aimRange;

        public void CheckEnemiesAround()
        {
            _checkEnemiesTimer = 0f;
            _enemies.Clear();
            RaycastHit[] enemiesHits = new RaycastHit[_enemiesHitsCount];
            int enemiesHitsCount = GetEnemiesHits(enemiesHits);

            if (enemiesHitsCount > 0)
                CheckEnemiesHits(enemiesHitsCount, enemiesHits);
            else
                NotFound();
        }

        private int GetEnemiesHits(RaycastHit[] enemiesHits) =>
            Physics.SphereCastNonAlloc(_transform.position, _aimRange, _transform.forward, enemiesHits, _sphereDistance, _enemyLayerMask,
                QueryTriggerInteraction.UseGlobal);

        private void CheckEnemiesHits(int enemiesHitsCount, RaycastHit[] enemiesHits)
        {
            for (int i = 0; i < enemiesHitsCount; i++)
            {
                EnemyHealth enemyHealth = enemiesHits[i].transform.gameObject.GetComponent<EnemyHealth>();

                if (enemyHealth.Current > 0)
                    _enemies.Add(enemyHealth);
            }

            if (_enemies.Count > 0)
            {
                _enemyNotFound = false;
                FindClosestEnemy(_enemies);
            }
            else
            {
                NotFound();
            }
        }

        private void FindClosestEnemy(List<EnemyHealth> visibleEnemies)
        {
            EnemyHealth closestEnemy = GetClosestEnemy(visibleEnemies);

            if (closestEnemy != null)
            {
                string id = closestEnemy.gameObject.GetComponent<UniqueId>().Id;

                if (_targetEnemyId != id || _targetEnemyId == null)
                {
                    _targetEnemyId = id;
                    _targetEnemy = closestEnemy;
                    CheckEnemyVisibility(closestEnemy.gameObject);
                }
            }
            else
            {
                NotFound();
            }
        }

        private void NotFound()
        {
            if (_enemyNotFound == false)
            {
                _enemyNotFound = true;
                _targetEnemyId = null;
                _targetEnemy = null;
                _view.CallEnemyNotFound();
            }
        }

        private EnemyHealth GetClosestEnemy(List<EnemyHealth> visibleEnemies)
        {
            float minDistance = _aimRange;
            EnemyHealth closestEnemy = null;

            foreach (EnemyHealth enemy in visibleEnemies)
            {
                _distanceToEnemy = Vector3.Distance(enemy.transform.position, _transform.position);

                if (_distanceToEnemy < minDistance)
                {
                    minDistance = _distanceToEnemy;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }

        private void CheckEnemyVisibility(GameObject enemy)
        {
            Vector3 direction = (enemy.transform.position - _transform.position).normalized;
            RaycastHit[] raycastHits = Physics.RaycastAll(_transform.position, direction, _distanceToEnemy, _visibleObstaclesLayerMask,
                QueryTriggerInteraction.UseGlobal);

            if (raycastHits.Length == 0)
            {
                TurnOffAnotherTargets(_enemies);
                TurnOnTarget();
                _view.CallFoundClosestEnemy(enemy);
            }
            else
            {
                NotFound();
            }
        }

        private void TurnOffAnotherTargets(List<EnemyHealth> visibleEnemies)
        {
            foreach (EnemyHealth enemy in visibleEnemies)
                if (_targetEnemyId != enemy.gameObject.GetComponent<UniqueId>().Id)
                    enemy.transform.GetComponentInChildren<TargetMovement>().Hide();
        }

        private void TurnOnTarget() =>
            _targetEnemy.transform.GetComponentInChildren<TargetMovement>().Show();
    }
}