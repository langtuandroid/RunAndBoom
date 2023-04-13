using System.Collections;
using CodeBase.Projectiles.Movement;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class MortarBehavior : HeroWeaponAppearance
    {
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