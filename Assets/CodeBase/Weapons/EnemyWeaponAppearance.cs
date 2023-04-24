using System.Collections;
using CodeBase.Enemy;
using CodeBase.Projectiles.Movement;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class EnemyWeaponAppearance : BaseWeaponAppearance
    {
        [SerializeField] private EnemyDeath _death;

        private EnemyWeaponTypeId _enemyWeaponTypeId;

        private void OnEnable() =>
            _death.Died += NotShoot;

        private void OnDisable() =>
            _death.Died -= NotShoot;

        public void Construct(EnemyWeaponStaticData weaponStaticData)
        {
            base.Construct(weaponStaticData.MuzzleVfxLifeTime, weaponStaticData.Cooldown,
                weaponStaticData.ProjectileTypeId, weaponStaticData.ShotVfxTypeId);

            _enemyWeaponTypeId = weaponStaticData.WeaponTypeId;
            // ReadyToShoot();
        }

        public void Shoot(Vector3? targetPosition)
        {
            if (CanShoot)
            {
                ReadyToShoot();

                for (int i = 0; i < ProjectilesRespawns.Length; i++)
                    StartCoroutine(CoroutineShootTo(targetPosition));

                Released();
            }
        }

        private IEnumerator CoroutineShootTo(Vector3? targetPosition)
        {
            ProjectileMovement projectileMovement = GetMovement();

            if (targetPosition != null && projectileMovement is BulletMovement)
                (projectileMovement as BulletMovement)?.SetTargetPosition((Vector3)targetPosition);

            Launch();
            ShotVfxsContainer.ShowShotVfx(ShotVfxsRespawns[0]);
            yield return LaunchProjectileCooldown;

            // ReadyToShoot();
        }

        protected override GameObject GetProjectile()
        {
            Debug.Log($"enemy weapon type: {_enemyWeaponTypeId}");
            return ObjectsPoolService.GetEnemyProjectile(_enemyWeaponTypeId.ToString());
        }
    }
}