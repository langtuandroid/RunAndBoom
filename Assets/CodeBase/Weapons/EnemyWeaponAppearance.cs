using System.Collections;
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

        public void Shoot()
        {
            if (CanShoot)
            {
                for (int i = 0; i < _projectilesRespawns.Length; i++)
                    StartCoroutine(CoroutineShootTo());

                for (int i = 0; i < _muzzlesRespawns.Length; i++)
                    LaunchShotVfx();
            }
        }

        private IEnumerator CoroutineShootTo()
        {
            int index = CurrentProjectileIndex;
            ChangeProjectileIndex();
            CanShoot = false;
            ProjectileObjects[index].transform.SetParent(null);
            ProjectileObjects[index].SetActive(true);

            ProjectileMovements[index].Launch();
            Debug.Log("Enemy launched");
            Debug.Log($"Enemy index {index}");

            ProjectileTraces[index]?.CreateTrace();

            yield return LaunchProjectileCooldown;
            ProjectileObjects[index].SetActive(false);
            SetPosition(index, transform);
            CanShoot = true;
        }
    }
}