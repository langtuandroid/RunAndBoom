using System.Collections;
using System.Linq;
using CodeBase.Data;
using CodeBase.Data.Upgrades;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Projectiles.Hit
{
    public class ProjectileBlast : BaseProjectileHit, IProgressReader
    {
        [SerializeField] private DestroyWithBlast _destroyWithBlast;

        private const float BaseRatio = 1f;
        private const float BlastDuration = 2f;
        private const float SphereCastRadius = 1f;

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
        private HeroWeaponTypeId? _heroWeaponTypeId;

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
            string targetTag = other.gameObject.tag;

            if (IsTargetTag(targetTag))
            {
                if (_prefab != null)
                {
                    // Vector3 closestPointNormalized = other.ClosestPoint(transform.position).normalized;
                    // bool isRaycast = Physics.Raycast(other.transform.position, other.transform.forward,
                    //     out RaycastHit hit);
                    bool isRaycast = Physics.SphereCast(other.transform.position, SphereCastRadius,
                        other.transform.forward, out RaycastHit hit);

                    // if (isRaycast)
                    // {
                    //     Vector3 hitSum = hit.point + hit.normal;
                    // var direction = Vector3.Reflect(transform.forward, closestPoint);
                    // Vector3 direction = other.gameObject.transform.position - transform.position;
                    // float angle = Vector3.Angle(transform.forward, direction);
                    // other.ClosestPointOnBounds(transform.position);
                    ShowBlast();
                    // ShowBlast(hitSum);
                    Trail?.HideTrace();
                    _destroyWithBlast.HitAllAround(_sphereRadius, _damage);
                    StartCoroutine(DestroyBlast());
                    Movement.Stop();
                    // }
                }
            }
        }

        public void Construct(GameObject prefab, float radius, float damage, HeroWeaponTypeId? heroWeaponTypeId = null)
        {
            _heroWeaponTypeId = heroWeaponTypeId;
            _staticDataService = AllServices.Container.Single<IStaticDataService>();

            _prefab = prefab;
            _sphereRadius = radius;
            _damage = damage;
        }

        private void SetBlastSize()
        {
            _blastItemData = _progress.WeaponsData.UpgradesData.UpgradeItemDatas.First(x =>
                x.WeaponTypeId == _heroWeaponTypeId && x.UpgradeTypeId == UpgradeTypeId.BlastSize);
            _blastItemData.LevelChanged += ChangeBlastSize;
            ChangeBlastSize();
        }

        private void ChangeBlastSize()
        {
            _blastItemData = _progress.WeaponsData.UpgradesData.UpgradeItemDatas.First(x =>
                x.WeaponTypeId == _heroWeaponTypeId && x.UpgradeTypeId == UpgradeTypeId.BlastSize);

            if (_blastItemData.LevelTypeId == LevelTypeId.None)
                _blastRadiusRatio = BaseRatio;
            else
                _blastRadiusRatio = _staticDataService
                    .ForUpgradeLevelsInfo(_blastItemData.UpgradeTypeId, _blastItemData.LevelTypeId).Value;

            _sphereRadius = _baseBlastRadius * _blastRadiusRatio;
        }

        private void ShowBlast(Vector3 direction)
        {
            if (_particleSystem == null)
            {
                _blastVfx = Instantiate(_prefab, transform.position, Quaternion.identity, null);
                _particleSystem = _blastVfx.GetComponent<ParticleSystem>();
            }
            else
            {
                _blastVfx.transform.position = transform.position;
                _blastVfx.transform.rotation.SetLookRotation(direction);
            }

            _particleSystem.Play(true);
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

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;

            if (_heroWeaponTypeId != null)
                SetBlastSize();
        }
    }
}