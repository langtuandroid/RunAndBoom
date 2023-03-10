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

            SetPosition(CurrentProjectileIndex);
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
            for (int i = 0; i < _projectilesRespawns.Length; i++)
                StartCoroutine(CoroutineShootTo());

            for (int i = 0; i < _muzzlesRespawns.Length; i++)
                LaunchShotVfx();
        }

        private IEnumerator CoroutineShootTo()
        {
            int index = CurrentProjectileIndex;
            ChangeProjectileIndex();
            ProjectileObjects[index].transform.SetParent(null);
            ProjectileObjects[index].SetActive(true);

            ProjectileMovements[index].Launch();
            Debug.Log("Launched");
            Debug.Log($"index {index}");

            ProjectileTraces[index]?.CreateTrace();

            yield return LaunchProjectileCooldown;
            ProjectileObjects[index].SetActive(false);
            SetPosition(index);
        }
    }
}