using System.Collections;
using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class MortarBehavior : HeroWeaponAppearance
    {
        [SerializeField] private WeaponRotation _weaponRotation;

        private void Awake() =>
            _weaponRotation.GotTarget += SetTarget;

        private void SetTarget(Vector3 targetPosition) =>
            TargetPosition = targetPosition;

        protected override IEnumerator CoroutineShootTo()
        {
            Launch(TargetPosition);
            yield return LaunchProjectileCooldown;
        }
    }
}