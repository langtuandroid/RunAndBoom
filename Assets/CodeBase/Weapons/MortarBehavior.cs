using System;
using CodeBase.Hero;
using CodeBase.Projectiles.Movement;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class MortarBehavior : HeroWeaponAppearance
    {
        [SerializeField] private DrawProjection _drawProjection;
        [SerializeField] private WeaponRotation _weaponRotation;

        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Awake() => 
            _weaponRotation.GotTarget += SetTarget;

        private void SetTarget(Vector3 targetPosition) => 
            (GetMovement() as BombMovement)?.SetTargetPosition(targetPosition);

        private void FixedUpdate()
        {
            // if (ProjectilesRespawns[0] != null)
            //     _drawProjection.UpdateTrajectory(GetMovement() as BombMovement);
        }

        private void Update()
        {
            if ((BombMovement)GetMovement() != null) 
                _drawProjection.ShowTrajectory(GetMovement() as BombMovement);
        }
        // private Vector3? _targetPosition = null;
        //
        // public void SetTarget(Vector3 targetPosition) =>
        //     _targetPosition = targetPosition;
        //
        // protected virtual IEnumerator CoroutineShootTo()
        // {
        //     if (_targetPosition != null && GetMovement() is BombMovement)
        //         (GetMovement() as BombMovement)?.SetTargetPosition((Vector3)_targetPosition);
        //
        //     Launch();
        //     yield return LaunchProjectileCooldown;
        // }
    }
}