using System.Collections;
using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Projectiles.Movement;
using CodeBase.StaticData.Enemies;
using CodeBase.StaticData.Weapons;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class EnemyWeaponAppearance : BaseWeaponAppearance
    {
        private EnemyWeaponTypeId _enemyWeaponTypeId;
        private Transform _currentProjectileRespawn;
        private EnemyStaticData _enemyStaticData;
        private EnemyTypeId _enemyTypeId;
        private EnemyWeaponStaticData _enemyWeaponStaticData;

        public void Construct(EnemyDeath death, EnemyTypeId enemyTypeId, EnemyWeaponStaticData weaponStaticData)
        {
            base.Construct(death, weaponStaticData.MuzzleVfxLifeTime, weaponStaticData.Cooldown,
                weaponStaticData.ProjectileTypeId, weaponStaticData.ShotVfxTypeId);

            _enemyTypeId = enemyTypeId;
            _enemyWeaponTypeId = weaponStaticData.WeaponTypeId;
        }

        public void Shoot(Vector3 targetPosition)
        {
            foreach (var t in _projectilesRespawns)
            {
                _currentProjectileRespawn = t;
                StartCoroutine(CoroutineShootTo(targetPosition));
            }

            _shotVfxsContainer.ShowShotVfx(_shotVfxsRespawns[0]);
            PlayShootSound();
        }

        public void Shoot()
        {
            foreach (var respawn in _projectilesRespawns)
            {
                _currentProjectileRespawn = respawn;
                StartCoroutine(CoroutineShootTo());
            }

            _shotVfxsContainer.ShowShotVfx(_shotVfxsRespawns[0]);
        }

        private IEnumerator CoroutineShootTo(Vector3 targetPosition)
        {
            Launch(targetPosition);
            yield return _launchProjectileCooldown;
        }

        private IEnumerator CoroutineShootTo()
        {
            Launch();
            yield return _launchProjectileCooldown;
        }

        protected override async void Launch()
        {
            GameObject projectile = await SetNewProjectile(_currentProjectileRespawn);
            ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();
            TuneProjectileBeforeLaunch(projectile, projectileMovement);
        }

        protected override async void Launch(Vector3 targetPosition)
        {
            GameObject projectile = await SetNewProjectile(_currentProjectileRespawn, targetPosition);
            ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();
            TuneProjectileBeforeLaunch(projectile, projectileMovement);
        }

        protected override async Task<GameObject> GetProjectile()
        {
            _projectile = await _poolService.GetEnemyProjectile(_projectileTypeId.ToString());
            _enemyStaticData = _staticDataService.ForEnemy(_enemyTypeId);
            _enemyWeaponStaticData = _staticDataService.ForEnemyWeapon(_enemyWeaponTypeId);
            _constructorService.ConstructEnemyProjectile(_projectile, _enemyStaticData.Damage,
                _enemyWeaponStaticData.ProjectileTypeId);
            return _projectile;
        }

        protected override void PlayShootSound()
        {
            switch (_enemyWeaponTypeId)
            {
                case EnemyWeaponTypeId.Pistol:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.ShotPistol),
                        transform: transform, _volume, _audioSource);
                    break;
                case EnemyWeaponTypeId.Shotgun:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.ShotShotgun),
                        transform: transform, _volume, _audioSource);
                    break;
                case EnemyWeaponTypeId.SniperRifle:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.ShotSr), transform: transform,
                        _volume, _audioSource);
                    break;
                case EnemyWeaponTypeId.SMG:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.ShotSmg), transform: transform,
                        _volume, _audioSource);
                    break;
                case EnemyWeaponTypeId.MG:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.ShotMg), transform: transform,
                        _volume, _audioSource);
                    break;
            }
        }
    }
}