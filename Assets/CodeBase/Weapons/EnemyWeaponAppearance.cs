using System.Collections;
using CodeBase.Projectiles.Movement;
using CodeBase.StaticData.ProjectileTrace;
using CodeBase.StaticData.Weapon;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class EnemyWeaponAppearance : BaseWeaponAppearance
    {
        public void Construct(EnemyWeaponStaticData weaponStaticData, ProjectileTraceStaticData projectileTraceStaticData)
        {
            base.Construct(weaponStaticData.MuzzleVfx, weaponStaticData.MuzzleVfxLifeTime, weaponStaticData.Cooldown, projectileTraceStaticData);

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

            SetPosition(CurrentProjectileIndex);
            SetInitialVisibility();
        }

        protected override void CreateProjectileMovement(GameObject projectileObject)
        {
            ProjectileMovement projectileMovement = projectileObject.GetComponent<ProjectileMovement>();
            SetBulletMovement(ref projectileMovement);
            ProjectileMovements.Add(projectileMovement);
        }

        public void Shoot() =>
            StartCoroutine(CoroutineShootTo());

        private IEnumerator CoroutineShootTo()
        {
            ProjectileObjects[CurrentProjectileIndex].transform.SetParent(null);
            ProjectileObjects[CurrentProjectileIndex].SetActive(true);

            ProjectileMovements[CurrentProjectileIndex].Launch();
            Debug.Log("Launched");
            Debug.Log($"index {CurrentProjectileIndex}");
            ProjectileTraces[CurrentProjectileIndex].CreateTrace();

            LaunchShotVfx();

            yield return LaunchProjectileCooldown;
            SetPosition(CurrentProjectileIndex);
        }
    }
}