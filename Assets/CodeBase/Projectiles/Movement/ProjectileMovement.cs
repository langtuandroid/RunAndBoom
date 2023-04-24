using System;
using System.Collections;
using System.Linq;
using CodeBase.Data;
using CodeBase.Data.Upgrades;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Projectiles.Movement
{
    public abstract class ProjectileMovement : MonoBehaviour
    {
        private const float BaseRatio = 1f;

        private IStaticDataService _staticDataService;
        private float _movementTimeLimit = 3f;
        private float _launchCounter = 0f;
        private float _baseSpeed;
        private UpgradeItemData _speedItemData;
        private float _speedRatio = BaseRatio;
        private HeroWeaponTypeId? _weaponTypeId;
        private PlayerProgress _progress;

        protected float Speed { get; private set; }
        protected bool IsMove { get; set; }

        public abstract event Action Stoped;

        private void OnEnable()
        {
            if (_speedItemData != null)
                _speedItemData.LevelChanged += ChangeSpeed;
        }

        private void OnDisable()
        {
            if (_speedItemData != null)
                _speedItemData.LevelChanged -= ChangeSpeed;
        }

        public void Construct(IPlayerProgressService progressService, IStaticDataService staticDataService,
            ProjectileTypeId projectileTypeId, HeroWeaponTypeId? heroWeaponTypeId = null)
        {
            if (heroWeaponTypeId != null)
                _weaponTypeId = heroWeaponTypeId;

            _staticDataService = staticDataService;
            _progress = progressService.Progress;
            ProjectileStaticData projectileStaticData = _staticDataService.ForProjectile(projectileTypeId);
            Speed = projectileStaticData.Speed;
            _baseSpeed = projectileStaticData.Speed;
            _movementTimeLimit = projectileStaticData.MovementLifeTime;
            IsMove = false;

            if (_weaponTypeId != null)
                SetSpeed();
        }

        private void SetSpeed()
        {
            _speedItemData = _progress.WeaponsData.UpgradesData.UpgradeItemDatas.First(x =>
                x.WeaponTypeId == _weaponTypeId && x.UpgradeTypeId == UpgradeTypeId.Speed);
            _speedItemData.LevelChanged += ChangeSpeed;
            ChangeSpeed();
        }

        private void ChangeSpeed()
        {
            _speedItemData = _progress.WeaponsData.UpgradesData.UpgradeItemDatas.First(x =>
                x.WeaponTypeId == _weaponTypeId && x.UpgradeTypeId == UpgradeTypeId.Speed);

            if (_speedItemData.LevelTypeId == LevelTypeId.None)
                _speedRatio = BaseRatio;
            else
                _speedRatio = _staticDataService
                    .ForUpgradeLevelsInfo(_speedItemData.UpgradeTypeId, _speedItemData.LevelTypeId).Value;

            Speed = _baseSpeed * _speedRatio;
        }

        public abstract void Launch();
        public abstract void Stop();

        protected void OffMove() =>
            IsMove = false;

        protected IEnumerator LaunchTime()
        {
            _launchCounter = _movementTimeLimit;
            IsMove = true;

            while (_launchCounter > 0f)
            {
                _launchCounter -= Time.deltaTime;
                yield return null;
            }

            if (_launchCounter <= 0f)
            {
                Stop();
            }
        }
    }
}