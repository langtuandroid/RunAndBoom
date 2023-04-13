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
        private const float MaxDistance = 15f;

        private HeroShooting _heroShooting;
        private HeroWeaponSelection _heroWeaponSelection;
        private HeroWeaponTypeId _heroWeaponTypeId;
        private Transform _heroTransform;
        private RaycastHit[] results = new RaycastHit[1];

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

        private void ReadyToShoot(GameObject arg1, HeroWeaponStaticData arg2, TrailStaticData arg3) =>
            ReadyToShoot();

        private void ShowTrajectory()
        {
        }

        public void ShootTo()
        {
            for (int i = 0; i < ProjectilesRespawns.Length; i++)
            {
                StartCoroutine(CoroutineShootTo());
                ShotVfxsContainer.ShowShotVfx(ShotVfxsRespawns[i]);
            }

            Released();
        }

        protected virtual IEnumerator CoroutineShootTo()
        {
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