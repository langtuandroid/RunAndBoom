using System.Collections;
using CodeBase.Hero;
using CodeBase.Projectiles.Movement;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class HeroWeaponAppearance : BaseWeaponAppearance
    {
        [SerializeField] private HeroDeath _death;

        private HeroReloading _heroReloading;
        private HeroWeaponSelection _heroWeaponSelection;
        private HeroWeaponTypeId _heroWeaponTypeId;

        private void OnEnable() =>
            _death.Died += NotShoot;

        private void OnDisable() =>
            _death.Died -= NotShoot;

        public void Construct(HeroReloading heroReloading, HeroWeaponSelection heroWeaponSelection)
        {
            _heroReloading = heroReloading;
            _heroWeaponSelection = heroWeaponSelection;

            _heroWeaponSelection.WeaponSelected += InitializeSelectedWeapon;
        }

        private void InitializeSelectedWeapon(GameObject weaponPrefab, HeroWeaponStaticData weaponStaticData,
            TrailStaticData trailStaticData)
        {
            base.Construct(weaponStaticData.ShotVfxLifeTime, weaponStaticData.Cooldown,
                weaponStaticData.ProjectileTypeId, weaponStaticData.ShotVfxTypeId);

            _heroWeaponTypeId = weaponStaticData.WeaponTypeId;

            _heroReloading.OnStopReloading += ReadyToShoot;
            // _heroShooting.OnStartReloading += NotReadyToShoot;
            _heroWeaponSelection.WeaponSelected += ReadyToShoot;
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

        public void ShootTo(Vector3 enemyPosition)
        {
            for (int i = 0; i < ProjectilesRespawns.Length; i++)
            {
                StartCoroutine(CoroutineShootTo(enemyPosition));
                ShotVfxsContainer.ShowShotVfx(ShotVfxsRespawns[i]);
            }

            Released();
        }

        private IEnumerator CoroutineShootTo(Vector3? targetPosition)
        {
            ProjectileMovement projectileMovement = GetMovement();

            if (targetPosition != null && projectileMovement is BombMovement)
                (projectileMovement as BombMovement)?.SetTargetPosition((Vector3)targetPosition);

            Launch();
            yield return LaunchProjectileCooldown;
        }

        protected override GameObject GetProjectile()
        {
            Debug.Log($"hero weapon type: {_heroWeaponTypeId}");
            return ObjectsPoolService.GetHeroProjectile(_heroWeaponTypeId.ToString());
        }
    }
}