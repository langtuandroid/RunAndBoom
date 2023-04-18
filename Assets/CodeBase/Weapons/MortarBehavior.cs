using CodeBase.Hero;
using CodeBase.Projectiles.Movement;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class MortarBehavior : HeroWeaponAppearance
    {
        [SerializeField] private WeaponRotation _weaponRotation;
        [SerializeField] private DrawTarget _drawTarget;

        private void Awake() =>
            _weaponRotation.GotTarget += SetTarget;

        private void SetTarget(Vector3 targetPosition)
        {
            _drawTarget.Draw(targetPosition);
            (GetMovement() as BombMovement)?.SetTargetPosition(targetPosition);
        }
    }
}