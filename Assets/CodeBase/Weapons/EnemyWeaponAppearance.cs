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

        public void Shoot(int projectilesCount, int shotVfxCount)
        {
            for (int i = 0; i < projectilesCount; i++)
                StartCoroutine(CoroutineShootTo());

            for (int i = 0; i < shotVfxCount; i++)
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