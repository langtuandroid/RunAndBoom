using System.Collections;
using System.Collections.Generic;
using CodeBase.Hero;
using CodeBase.Projectiles.Hit;
using CodeBase.Projectiles.Movement;
using CodeBase.StaticData.ProjectileTraces;
using CodeBase.StaticData.Weapons;
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
            CanShoot = true;
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
            for (int i = 0; i < _projectilesRespawns.Length * ProjectilesRatio; i++)
            {
                var projectileObject = CreateProjectileObject(i);

                CreateProjectileMovement(projectileObject);

                CreateProjectileTrace(projectileObject);

                CreateProjectileBlast(projectileObject);
            }

            SetPosition(CurrentProjectileIndex, transform);
            ResetRespawnIndex();
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
                    SetShotMovement(ref movement);
                    break;

                case HeroWeaponTypeId.RocketLauncher:
                    SetShotMovement(ref movement);
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
            if (CanShoot)
            {
                for (int i = 0; i < _projectilesRespawns.Length; i++)
                {
                    StartCoroutine(CoroutineShootTo(enemyPosition));
                    ProjectileObjects[InvisibleIndex(i)].SetActive(false);
                }
            }
        }

        private int InvisibleIndex(int index)
        {
            int result = index + ProjectileObjects.Count / ProjectilesRatio;

            if (result >= ProjectileObjects.Count)
                result = index;

            Debug.Log($"result index {result}");
            return result;
        }

        private IEnumerator CoroutineShootTo(Vector3? targetPosition)
        {
            int index = CurrentProjectileIndex;
            ChangeProjectileIndex();
            ProjectileObjects[index].transform.SetParent(null);
            ProjectileObjects[index].SetActive(true);

            if (targetPosition != null && ProjectileMovements[index] is BombMovement)
                (ProjectileMovements[index] as BombMovement)?.SetTargetPosition((Vector3)targetPosition);

            ProjectileMovements[index].Launch();
            CanShoot = false;
            Debug.Log("Launched");
            Debug.Log($"index {index}");
            ProjectileTraces[index].CreateTrace();

            LaunchShotVfx(index);

            yield return LaunchProjectileCooldown;
            SetPosition(index, transform);
            // ProjectileObjects[index].SetActive(_showProjectiles);

            CanShoot = true;
        }
    }
}