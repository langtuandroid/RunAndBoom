using System;
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

namespace CodeBase.Hero
{
    public class HeroReloading : MonoBehaviour, IProgressReader
    {
        [SerializeField] private HeroWeaponSelection _heroWeaponSelection;
        [SerializeField] private HeroShooting _heroShooting;

        private const float BaseRatio = 1f;

        private IStaticDataService _staticDataService;
        private HeroWeaponTypeId _weaponTypeId;
        private float _currentAttackCooldown = 0f;
        private float _initialCooldown = 2f;
        private float _baseCooldown = 0f;
        private float _cooldown = 0f;
        private float _reloadingRatio = 0f;
        private bool _startReloaded;
        private UpgradeItemData _reloadingItemData;

        public Action<float> OnStartReloading;
        public Action OnStopReloading;
        private PlayerProgress _progress;

        private void Awake()
        {
            _heroWeaponSelection.WeaponSelected += GetCurrentWeaponObject;
            _heroShooting.Shot += StartCooldown;
        }

        private void OnEnable()
        {
            if (_reloadingItemData != null)
                _reloadingItemData.LevelChanged += ChangeCooldown;
        }

        private void OnDisable()
        {
            if (_reloadingItemData != null)
                _reloadingItemData.LevelChanged -= ChangeCooldown;
        }

        public void Construct(IPlayerProgressService progressService, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            // _progress = progressService.Progress;
            // SetCooldown();
        }

        private void SetCooldown()
        {
            _reloadingItemData = _progress.WeaponsData.UpgradesData.UpgradeItemDatas.First(x =>
                x.WeaponTypeId == _weaponTypeId && x.UpgradeTypeId == UpgradeTypeId.Reloading);
            _reloadingItemData.LevelChanged += ChangeCooldown;
            ChangeCooldown();
        }

        private void ChangeCooldown()
        {
            _reloadingItemData = _progress.WeaponsData.UpgradesData.UpgradeItemDatas.First(x =>
                x.WeaponTypeId == _weaponTypeId && x.UpgradeTypeId == UpgradeTypeId.Reloading);

            if (_reloadingItemData.LevelTypeId == LevelTypeId.None)
                _reloadingRatio = BaseRatio;
            else
                _reloadingRatio = _staticDataService
                    .ForUpgradeLevelsInfo(_reloadingItemData.UpgradeTypeId, _reloadingItemData.LevelTypeId).Value;

            _cooldown = _baseCooldown * _reloadingRatio;
        }

        private void StartCooldown() =>
            _currentAttackCooldown = _baseCooldown;

        private void Update()
        {
            UpdateInitialCooldown();
            UpdateCooldown();
        }

        private void GetCurrentWeaponObject(GameObject weaponPrefab, HeroWeaponStaticData heroWeaponStaticData,
            TrailStaticData trailStaticData)
        {
            _weaponTypeId = heroWeaponStaticData.WeaponTypeId;
            _baseCooldown = heroWeaponStaticData.Cooldown;
            _currentAttackCooldown = 0f;
            // OnStopReloading?.Invoke();

            if (_progress != null)
                SetCooldown();
        }

        private void UpdateCooldown()
        {
            if (!CooldownUp())
            {
                OnStartReloading?.Invoke(_currentAttackCooldown / _cooldown);
                _currentAttackCooldown -= Time.deltaTime;
            }
            else
                OnStopReloading?.Invoke();
        }

        private bool CooldownUp() =>
            _currentAttackCooldown <= 0;

        private bool InitialCooldownUp() =>
            _initialCooldown <= 0f;

        private void UpdateInitialCooldown()
        {
            if (InitialCooldownUp() && !_startReloaded)
            {
                OnStopReloading?.Invoke();
                _startReloaded = true;
            }

            _initialCooldown -= Time.deltaTime;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;
            SetCooldown();
        }
    }
}