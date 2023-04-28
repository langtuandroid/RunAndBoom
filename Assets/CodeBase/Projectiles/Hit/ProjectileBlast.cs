using System.Collections;
using System.Linq;
using CodeBase.Data;
using CodeBase.Data.Upgrades;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Projectiles.Hit
{
    public class ProjectileBlast : BaseProjectileHit
    {
        [SerializeField] private DestroyWithBlast _destroyWithBlast;

        private const float BaseRatio = 1f;
        private const float BlastDuration = 2f;

        private IStaticDataService _staticDataService;
        private PlayerProgress _progress;
        private float _baseBlastRadius;
        private UpgradeItemData _blastItemData;
        private float _blastRadiusRatio = BaseRatio;
        private GameObject _prefab;
        private float _sphereRadius;
        private ParticleSystem _particleSystem;
        private GameObject _blastVfx;
        private float _damage;
        private CapsuleCollider _hitCollider;
        private HeroWeaponTypeId? _weaponTypeId;

        private void Awake() =>
            _hitCollider = GetComponent<CapsuleCollider>();

        public void OffCollider() =>
            _hitCollider.enabled = false;

        public void OnCollider() =>
            _hitCollider.enabled = transform;

        private void OnEnable()
        {
            HideBlast();

            if (_blastItemData != null)
                _blastItemData.LevelChanged += ChangeBlastSize;
        }

        private void OnDisable()
        {
            if (_blastItemData != null)
                _blastItemData.LevelChanged -= ChangeBlastSize;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTriggerEnter");
            string targetTag = other.gameObject.tag;

            if (IsTargetTag(targetTag))
            {
                Debug.Log("Targeted tag");
                if (_prefab != null)
                {
                    ShowBlast();
                    Trail?.HideTrace();
                    _destroyWithBlast.HitAllAround(_sphereRadius, _damage);
                    StartCoroutine(DestroyBlast());
                    Movement.Stop();
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("OnCollisionEnter");
            string targetTag = other.gameObject.tag;

            if (IsTargetTag(targetTag))
            {
                Debug.Log("Targeted tag");
                if (_prefab != null)
                {
                    ShowBlast();
                    Trail?.HideTrace();
                    _destroyWithBlast.HitAllAround(_sphereRadius, _damage);
                    StartCoroutine(DestroyBlast());
                    Movement.Stop();
                }
            }
        }

        public void Construct(IPlayerProgressService progressService, IStaticDataService staticDataService,
            GameObject prefab, float radius, float damage,
            HeroWeaponTypeId? heroWeaponTypeId = null)
        {
            _progress = progressService.Progress;
            _staticDataService = staticDataService;

            if (heroWeaponTypeId != null)
            {
                _weaponTypeId = heroWeaponTypeId;
                SetBlastSize();
            }

            _prefab = prefab;
            _sphereRadius = radius;
            _damage = damage;
        }

        private void SetBlastSize()
        {
            _blastItemData = _progress.WeaponsData.UpgradesData.UpgradeItemDatas.First(x =>
                x.WeaponTypeId == _weaponTypeId && x.UpgradeTypeId == UpgradeTypeId.BlastSize);
            _blastItemData.LevelChanged += ChangeBlastSize;
            ChangeBlastSize();
        }

        private void ChangeBlastSize()
        {
            _blastItemData = _progress.WeaponsData.UpgradesData.UpgradeItemDatas.First(x =>
                x.WeaponTypeId == _weaponTypeId && x.UpgradeTypeId == UpgradeTypeId.BlastSize);

            if (_blastItemData.LevelTypeId == LevelTypeId.None)
                _blastRadiusRatio = BaseRatio;
            else
                _blastRadiusRatio = _staticDataService
                    .ForUpgradeLevelsInfo(_blastItemData.UpgradeTypeId, _blastItemData.LevelTypeId).Value;

            _sphereRadius = _baseBlastRadius * _blastRadiusRatio;
        }

        private void ShowBlast()
        {
            if (_particleSystem == null)
            {
                _blastVfx = Instantiate(_prefab, transform.position, Quaternion.identity, null);
                _particleSystem = _blastVfx.GetComponent<ParticleSystem>();
            }
            else
            {
                _blastVfx.transform.position = transform.position;
            }

            _particleSystem.Play(true);
        }

        private void HideBlast()
        {
            if (_particleSystem != null)
                _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        private IEnumerator DestroyBlast()
        {
            yield return new WaitForSeconds(BlastDuration);
            HideBlast();
        }
    }
}