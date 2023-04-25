using System.Collections;
using CodeBase.Hero;
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
            TargetPosition = targetPosition;
            _drawTarget.Draw(targetPosition);
        }

        protected override IEnumerator CoroutineShootTo()
        {
            Debug.Log("MortarBehavior CoroutineShootTo");
            Launch(TargetPosition);
            yield return LaunchProjectileCooldown;
        }
    }
}