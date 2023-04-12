using System;
using System.Collections;
using CodeBase.Hero;
using CodeBase.Projectiles.Movement;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.Weapons;
using UnityEngine;
using UnityEngine.Rendering;

namespace CodeBase.Weapons
{
    public class HeroWeaponAppearance : BaseWeaponAppearance
    {
        private const float MaxDistance = 15f;
        private const string EnemyTag = "Enemy";

        private HeroShooting _heroShooting;
        private HeroWeaponSelection _heroWeaponSelection;
        private HeroWeaponTypeId _heroWeaponTypeId;
        private Vector3 _targetPosition;
        private Transform _heroTransform;
        private RaycastHit[] results = new RaycastHit[8];

        public void Construct(HeroShooting heroShooting, HeroWeaponSelection heroWeaponSelection)
        {
            _heroShooting = heroShooting;
            _heroWeaponSelection = heroWeaponSelection;
            _heroTransform = heroShooting.gameObject.transform;

            _heroWeaponSelection.WeaponSelected += InitializeSelectedWeapon;
        }

        private void InitializeSelectedWeapon(GameObject weaponPrefab, HeroWeaponStaticData weaponStaticData, TrailStaticData trailStaticData)
        {
            base.Construct(weaponStaticData.ShotVfxLifeTime, weaponStaticData.Cooldown, weaponStaticData.ProjectileTypeId, weaponStaticData.ShotVfxTypeId);
            _heroWeaponTypeId = weaponStaticData.WeaponTypeId;

            _heroShooting.OnStopReloading += ReadyToShoot;
            _heroWeaponSelection.WeaponSelected += ReadyToShoot;
        }

        private void FixedUpdate()
        {
            if (_heroWeaponTypeId == HeroWeaponTypeId.Mortar && _heroTransform != null)
            {
                int count = Physics.RaycastNonAlloc(_heroTransform.position, _heroTransform.forward, results, MaxDistance);

                if (count > 0)
                {
                    float distance = 0f;
                    
                    foreach (RaycastHit raycastHit in results)
                    {
                        if (raycastHit.transform.gameObject.CompareTag(EnemyTag))
                        {
                            if (raycastHit.distance > distance)
                                distance = raycastHit.distance;
                        }
                    }
                    
                    // if(distance>0f)
                }
            }
        }

        private void ReadyToShoot(GameObject arg1, HeroWeaponStaticData arg2, TrailStaticData arg3) =>
            ReadyToShoot();

        // private void NotReadyToShoot(float cooldown)
        // {
        // for (int i = 0; i < ProjectilesRespawns.Length; i++)
        // {
        //     var projectile = SetNewProjectile(ProjectilesRespawns[i]);
        //
        //     if (ShowProjectiles)
        //         projectile.SetActive(true);
        // }
        // }

        public void ShootTo()
        {
            for (int i = 0; i < ProjectilesRespawns.Length; i++)
            {
                StartCoroutine(CoroutineShootTo());
                ShotVfxsContainer.ShowShotVfx(ShotVfxsRespawns[i]);
            }

            Released();
        }

        private IEnumerator CoroutineShootTo()
        {
            // if (targetPosition != null && GetMovement() is BombMovement)
            //     (GetMovement() as BombMovement)?.SetTargetPosition((Vector3)targetPosition);

            Launch();
            yield return LaunchProjectileCooldown;
        }

        protected override GameObject GetProjectile()
        {
            Debug.Log($"hero weapon type: {_heroWeaponTypeId}");
            return PoolService.GetHeroProjectile(_heroWeaponTypeId.ToString());
        }
    }
}