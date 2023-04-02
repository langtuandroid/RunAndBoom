using System.Collections;
using CodeBase.Projectiles.Movement;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class EnemyWeaponAppearance : BaseWeaponAppearance
    {
        private EnemyWeaponTypeId _enemyWeaponTypeId;

        public void Construct(EnemyWeaponStaticData weaponStaticData)
        {
            base.Construct(weaponStaticData.MuzzleVfxLifeTime, weaponStaticData.Cooldown, weaponStaticData.ProjectileTypeId, weaponStaticData.ShotVfxTypeId);

            _enemyWeaponTypeId = weaponStaticData.WeaponTypeId;
            ReadyToShoot();
        }

        public void Shoot(Vector3? targetPosition)
        {
            for (int i = 0; i < ProjectilesRespawns.Length; i++)
            {
                StartCoroutine(CoroutineShootTo(targetPosition));
            }

            Released();
        }

        private IEnumerator CoroutineShootTo(Vector3? targetPosition)
        {
            if (targetPosition != null && GetMovement() is BulletMovement)
                (GetMovement() as BulletMovement)?.SetTargetPosition((Vector3)targetPosition);

            Launch();
            ShotVfxsContainer.ShowShotVfx(ShotVfxsRespawns[0]);
            yield return LaunchProjectileCooldown;

            ReadyToShoot();
        }

        protected override GameObject GetProjectile()
        {
            // Debug.Log($"enemy weapon type: {_enemyWeaponTypeId}");
            return PoolService.GetEnemyProjectile(_enemyWeaponTypeId.ToString());
        }
    }
}