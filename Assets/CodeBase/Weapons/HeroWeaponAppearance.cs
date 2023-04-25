using System.Collections;
using CodeBase.Hero;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class HeroWeaponAppearance : BaseWeaponAppearance
    {
        [HideInInspector] protected Vector3 TargetPosition;

        private HeroReloading _heroReloading;
        private HeroWeaponSelection _heroWeaponSelection;
        private HeroWeaponTypeId _heroWeaponTypeId;

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
            _heroWeaponSelection.WeaponSelected += ReadyToShoot;
        }

        private void ReadyToShoot(GameObject arg1, HeroWeaponStaticData arg2, TrailStaticData arg3) =>
            ReadyToShoot();

        public void ShootTo()
        {
            for (int i = 0; i < ProjectilesRespawns.Length; i++)
            {
                StartCoroutine(CoroutineShootTo());
                ShotVfxsContainer.ShowShotVfx(ShotVfxsRespawns[i]);
                Release();
            }
        }

        protected virtual IEnumerator CoroutineShootTo()
        {
            Launch();
            yield return LaunchProjectileCooldown;
        }

        protected override GameObject GetProjectile() =>
            PoolService.GetHeroProjectile(_heroWeaponTypeId.ToString());
    }
}