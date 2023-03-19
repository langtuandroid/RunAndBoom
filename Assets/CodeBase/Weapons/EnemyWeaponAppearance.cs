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
            for (int i = 0; i < _projectilesRespawns.Length * ProjectilesRatio; i++)
            {
                var projectileObject = CreateProjectileObject(i);
                CreateProjectileMovement(projectileObject);
                CreateProjectileTrace(projectileObject);
            }

            SetPosition(CurrentProjectileIndex, transform);
            SetInitialVisibility();
            ResetRespawnIndex();
        }

        protected override void CreateProjectileMovement(GameObject projectileObject)
        {
            ProjectileMovement projectileMovement = projectileObject.GetComponent<ProjectileMovement>();

            if (projectileMovement is BulletMovement) SetBulletMovement(ref projectileMovement);
            else if (projectileMovement is ShotMovement) SetShotMovement(ref projectileMovement);

            ProjectileMovements.Add(projectileMovement);
        }

        public void Shoot(Vector3? targetPosition)
        {
            if (CanShoot)
            {
                for (int i = 0; i < _projectilesRespawns.Length; i++)
                    StartCoroutine(CoroutineShootTo(targetPosition));
            }
        }

        private IEnumerator CoroutineShootTo(Vector3? targetPosition)
        {
            int index = CurrentProjectileIndex;
            ChangeProjectileIndex();
            ProjectileObjects[index].transform.SetParent(null);
            ProjectileObjects[index].SetActive(true);

            if (targetPosition != null && ProjectileMovements[index] is BulletMovement)
                (ProjectileMovements[index] as BulletMovement)?.SetTargetPosition((Vector3)targetPosition);

            ProjectileMovements[index].Launch();
            Debug.Log("Launched");
            Debug.Log($"index {index}");

            ProjectileTraces[index]?.CreateTrace();

            yield return LaunchProjectileCooldown;
            // ProjectileObjects[index].SetActive(false);
            SetPosition(index, transform);
        }
    }
}