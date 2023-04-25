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

        public void Construct(EnemyDeath death, EnemyWeaponStaticData weaponStaticData)
        {
            base.Construct(death, weaponStaticData.MuzzleVfxLifeTime, weaponStaticData.Cooldown,
                weaponStaticData.ProjectileTypeId, weaponStaticData.ShotVfxTypeId);

            _enemyWeaponTypeId = weaponStaticData.WeaponTypeId;
        }

        public void Shoot(Vector3? targetPosition)
        {
            for (int i = 0; i < ProjectilesRespawns.Length; i++)
            {
                ReadyToShoot();
                StartCoroutine(CoroutineShootTo(targetPosition));
            }

            ShotVfxsContainer.ShowShotVfx(ShotVfxsRespawns[0]);
            Release();
        }

        private IEnumerator CoroutineShootTo(Vector3? targetPosition)
        {
            if (targetPosition != null && GetMovement() is BulletMovement)
                (GetMovement() as BulletMovement)?.SetTargetPosition((Vector3)targetPosition);

            Launch();
            yield return LaunchProjectileCooldown;

            ReadyToShoot();
        }

        protected override GameObject GetProjectile() =>
            PoolService.GetEnemyProjectile(_enemyWeaponTypeId.ToString());
    }
}