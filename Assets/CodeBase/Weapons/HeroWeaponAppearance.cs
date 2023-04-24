using System.Collections;
using CodeBase.Hero;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class HeroWeaponAppearance : BaseWeaponAppearance
    {
        [SerializeField] private HeroDeath _death;
        private const float MaxDistance = 15f;

        private HeroReloading _heroReloading;
        private HeroWeaponSelection _heroWeaponSelection;
        private HeroWeaponTypeId _heroWeaponTypeId;
        private Transform _heroTransform;
        private RaycastHit[] results = new RaycastHit[1];

        private void OnEnable() =>
            _death.Died += NotShoot;

        private void OnDisable() =>
            _death.Died -= NotShoot;

        public void Construct(HeroReloading heroReloading, HeroWeaponSelection heroWeaponSelection)
        {
            _heroReloading = heroReloading;
            _heroWeaponSelection = heroWeaponSelection;
            _heroTransform = heroReloading.gameObject.transform;

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
            }

            Released();
        }

        private IEnumerator CoroutineShootTo()
        {
            Launch();
            yield return LaunchProjectileCooldown;
        }

        protected override GameObject GetProjectile() =>
            ObjectsPoolService.GetHeroProjectile(_heroWeaponTypeId.ToString());
    }
}