using System.Collections;
using CodeBase.Enemy;
using CodeBase.Projectiles.Movement;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class EnemyWeaponAppearance : BaseWeaponAppearance
    {
        private EnemyWeaponTypeId _enemyWeaponTypeId;
        private Transform _currentProjectileRespawn;

        public void Construct(EnemyDeath death, EnemyWeaponStaticData weaponStaticData)
        {
            base.Construct(death, weaponStaticData.MuzzleVfxLifeTime, weaponStaticData.Cooldown,
                weaponStaticData.ProjectileTypeId, weaponStaticData.ShotVfxTypeId);

            _enemyWeaponTypeId = weaponStaticData.WeaponTypeId;
        }

        public void Shoot(Vector3 targetPosition)
        {
            foreach (var t in ProjectilesRespawns)
            {
                _currentProjectileRespawn = t;
                StartCoroutine(CoroutineShootTo(targetPosition));
            }

            ShotVfxsContainer.ShowShotVfx(ShotVfxsRespawns[0]);
        }

        public void Shoot()
        {
            foreach (var respawn in ProjectilesRespawns)
            {
                _currentProjectileRespawn = respawn;
                StartCoroutine(CoroutineShootTo());
            }

            ShotVfxsContainer.ShowShotVfx(ShotVfxsRespawns[0]);
        }

        private IEnumerator CoroutineShootTo(Vector3 targetPosition)
        {
            Launch(targetPosition);
            yield return LaunchProjectileCooldown;
        }

        private IEnumerator CoroutineShootTo()
        {
            Launch();
            yield return LaunchProjectileCooldown;
        }

        protected override void Launch()
        {
            GameObject projectile = SetNewProjectile(_currentProjectileRespawn);
            ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();
            TuneProjectileBeforeLaunch(projectile, projectileMovement);
        }

        protected override void Launch(Vector3 targetPosition)
        {
            GameObject projectile = SetNewProjectile(_currentProjectileRespawn);
            ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();
            TuneProjectileBeforeLaunch(projectile, projectileMovement);
        }

        protected override GameObject GetProjectile() =>
            PoolService.GetEnemyProjectile(_enemyWeaponTypeId.ToString());
    }
}