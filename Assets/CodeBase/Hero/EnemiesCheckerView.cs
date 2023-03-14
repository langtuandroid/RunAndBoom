using System;
using CodeBase.Logic;
using CodeBase.StaticData.ProjectileTraces;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Hero
{
    public class EnemiesCheckerView : MonoBehaviour, IView
    {
        [SerializeField] private LayerMask _enemyLayerMask;
        [SerializeField] private LayerMask _visibleObstaclesLayerMask;
        [SerializeField] private HeroWeaponSelection _heroWeaponSelection;

        private float _checkEnemiesDelay = 0.2f;
        private float _checkEnemiesTimer = 0f;
        private float _aimRange = 1f;
        private float _sphereDistance = 0f;
        private float _distanceToEnemy = 0f;

        private EnemiesCheckerPresenter _presenter;

        public event Action<GameObject> FoundClosestEnemy;
        public event Action EnemyNotFound;

        private void Awake()
        {
            _presenter = new EnemiesCheckerPresenter(this, transform, _enemyLayerMask, _visibleObstaclesLayerMask);
            _heroWeaponSelection.WeaponSelected += SetWeaponAimRange;
        }

        private void FixedUpdate()
        {
            UpFixedTime();

            if (IsCheckEnemiesTimerReached())
                _presenter.CheckEnemiesAround();
        }

        private void UpFixedTime() =>
            _checkEnemiesTimer += Time.fixedDeltaTime;

        private bool IsCheckEnemiesTimerReached() =>
            _checkEnemiesTimer >= _checkEnemiesDelay;

        private void SetWeaponAimRange(GameObject weaponPrefab, HeroWeaponStaticData heroWeaponStaticData,
            ProjectileTraceStaticData projectileTraceStaticData) =>
            _presenter.SetWeaponAimRange(heroWeaponStaticData.AimRange);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Debug.DrawLine(transform.position, transform.position + Vector3.forward * _distanceToEnemy);
            Gizmos.DrawWireSphere(transform.position + Vector3.forward * _sphereDistance, _aimRange);
        }

        internal void CallFoundClosestEnemy(GameObject enemy) =>
            FoundClosestEnemy?.Invoke(enemy);

        internal void CallEnemyNotFound() =>
            EnemyNotFound?.Invoke();
    }
}