using System.Collections;
using System.Collections.Generic;
using CodeBase.Hero;
using CodeBase.Projectiles.Hit;
using CodeBase.Projectiles.Movement;
using CodeBase.StaticData.ProjectileTrace;
using CodeBase.StaticData.Weapon;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class HeroWeaponAppearance : BaseWeaponAppearance
    {
        private HeroWeaponSelection _heroWeaponSelection;
        private List<ProjectileBlast> _blasts;
        private HeroWeaponTypeId _weaponTypeId;
        private float _blastRange;
        private GameObject _blastVfxPrefab;

        public void Construct(HeroWeaponSelection heroWeaponSelection)
        {
            _heroWeaponSelection = heroWeaponSelection;
            _heroWeaponSelection.WeaponSelected += InitializeSelectedWeapon;
            _blasts = new List<ProjectileBlast>(_projectilesRespawns.Length);
        }

        private void InitializeSelectedWeapon(GameObject weaponPrefab, HeroWeaponStaticData weaponStaticData,
            ProjectileTraceStaticData projectileTraceStaticData)
        {
            base.Construct(weaponStaticData.MuzzleVfx, weaponStaticData.MuzzleVfxLifeTime, weaponStaticData.Cooldown, weaponStaticData.ProjectileSpeed,
                weaponStaticData.MovementLifeTime, weaponStaticData.Damage, projectileTraceStaticData);

            _weaponTypeId = weaponStaticData.WeaponTypeId;
            _blastRange = weaponStaticData.BlastRange;
            _blastVfxPrefab = weaponStaticData.blastVfxPrefab;

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

                CreateProjectileBlast(projectileObject);
            }

            SetPosition(CurrentProjectileIndex);
            SetInitialVisibility();
        }

        private void CreateProjectileBlast(GameObject projectileObject)
        {
            ProjectileBlast projectileBlast = projectileObject.GetComponentInChildren<ProjectileBlast>();
            SetBlast(ref projectileBlast);
            _blasts.Add(projectileBlast);
        }

        protected override void CreateProjectileMovement(GameObject projectileObject)
        {
            ProjectileMovement projectileMovement = projectileObject.GetComponent<ProjectileMovement>();
            SetMovementType(ref projectileMovement);
            ProjectileMovements.Add(projectileMovement);
        }

        private void SetMovementType(ref ProjectileMovement movement)
        {
            switch (_weaponTypeId)
            {
                case HeroWeaponTypeId.GrenadeLauncher:
                    SetGrenadeMovement(ref movement);
                    break;

                case HeroWeaponTypeId.Mortar:
                    SetBombMovement(ref movement);
                    break;

                case HeroWeaponTypeId.RPG:
                    SetBulletMovement(ref movement);
                    break;

                case HeroWeaponTypeId.RocketLauncher:
                    SetBulletMovement(ref movement);
                    break;
            }
        }

        private void SetGrenadeMovement(ref ProjectileMovement movement) =>
            (movement as GrenadeMovement)?.Construct(ProjectileSpeed, transform, MovementLifeTime);

        private void SetBombMovement(ref ProjectileMovement movement) =>
            (movement as BombMovement)?.Construct(ProjectileSpeed, transform, MovementLifeTime);

        private void SetBlast(ref ProjectileBlast blast) =>
            blast.Construct(_blastVfxPrefab, _blastRange, Damage);

        public void ShootTo(Vector3 enemyPosition)
        {
            for (int i = 0; i < _projectilesRespawns.Length; i++)
                StartCoroutine(CoroutineShootTo(enemyPosition));

            for (int i = 0; i < _muzzlesRespawns.Length; i++)
                LaunchShotVfx();
        }

        private IEnumerator CoroutineShootTo(Vector3 targetPosition)
        {
            int index = CurrentProjectileIndex;
            ChangeProjectileIndex();
            ProjectileObjects[index].transform.SetParent(null);
            ProjectileObjects[index].SetActive(true);

            (ProjectileMovements[index] as BombMovement)?.SetTargetPosition(targetPosition);

            ProjectileMovements[index].Launch();
            Debug.Log("Launched");
            Debug.Log($"index {index}");
            ProjectileTraces[index].CreateTrace();

            LaunchShotVfx();

            yield return LaunchProjectileCooldown;
            SetPosition(index);
        }
    }
}