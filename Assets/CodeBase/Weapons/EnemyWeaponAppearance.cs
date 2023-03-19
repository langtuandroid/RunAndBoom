using System.Collections;
using CodeBase.Projectiles;
using CodeBase.Projectiles.Movement;
using CodeBase.StaticData.ProjectileTraces;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class EnemyWeaponAppearance : BaseWeaponAppearance
    {
        public void Construct(EnemyWeaponStaticData weaponStaticData, ProjectileTraceStaticData projectileTraceStaticData)
        {
            base.Construct(weaponStaticData.MuzzleVfx, weaponStaticData.MuzzleVfxLifeTime, weaponStaticData.Cooldown, weaponStaticData.ProjectileSpeed,
                weaponStaticData.MovementLifeTime, weaponStaticData.Damage, projectileTraceStaticData);

            CreateShotVfx();
            CreateProjectiles();
        }

        protected override void CreateProjectiles()
        {
            for (int i = 0; i < _projectilesRespawns.Length; i++)
            {
                var projectileObject = CreateProjectileObject(i);
                CreateProjectileMovement(projectileObject);
                CreateProjectileTrace(projectileObject);
            }

            SetPosition(CurrentProjectileIndex, transform);
            SetInitialVisibility();
        }

        protected override void CreateProjectileMovement(GameObject projectileObject)
        {
            ProjectileMovement projectileMovement = projectileObject.GetComponent<ProjectileMovement>();
            SetBulletMovement(ref projectileMovement);
            ProjectileMovements.Add(projectileMovement);
        }

        public void Shoot(Vector3? targetPosition)
        {
            if (CanShoot)
            {
                for (int i = 0; i < _projectilesRespawns.Length; i++)
                    StartCoroutine(CoroutineShootTo(targetPosition));

                for (int i = 0; i < _muzzlesRespawns.Length; i++)
                    LaunchShotVfx();
            }
        }

        private IEnumerator CoroutineShootTo(Vector3? targetPosition)
        {
            int index = -1;

            bool found = GetIndexNotActiveProjectile(ref index);

            if (found)
            {
                CanShoot = false;
                SetPosition(index, null);
                ProjectileObjects[index].SetActive(true);

                if (targetPosition != null && ProjectileMovements[index] is BulletMovement)
                    (ProjectileMovements[index] as BulletMovement)?.SetTargetPosition((Vector3)targetPosition);

                ProjectileMovements[index].Launch();
                Debug.Log($"index: {index}");
                Debug.Log("Launched");
                ProjectileObjects[index].GetComponent<ProjectileTrace>().CreateTrace();

                LaunchShotVfx();

                yield return LaunchProjectileCooldown;

                SetNextProjectileReady(index);
                CanShoot = true;
            }
        }
    }
}