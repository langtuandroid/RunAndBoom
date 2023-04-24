using System;
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

namespace CodeBase.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroRotating : MonoBehaviour
    {
        [SerializeField] private EnemiesChecker _enemiesChecker;
        [SerializeField] private HeroLookAt _heroLookAt;

        // private const float SmoothRotationSpeed = 0.04f;
        private const float AngleForFastRotating = 10f;
        private const float MaxAngleForLookAt = 1f;
        private const float BaseRatio = 1f;

        private IStaticDataService _staticDataService;
        private float _rotationRatio = BaseRatio;
        private float _baseRotationSpeed;
        private float _rotationSpeed;
        private Vector3 _shootPosition;
        private Vector3 _direction;
        private bool _toEnemy;
        private Quaternion _targetRotation;
        private Coroutine _rotatingCoroutine;
        private bool _canRotating = true;
        private PlayerProgress _progress;
        private UpgradeItemData _rotationItemData;
        private HeroWeaponTypeId _weaponTypeId;

        public event Action<GameObject> EndedRotatingToEnemy;
        public event Action StartedRotating;

        private void Awake()
        {
            _enemiesChecker.FoundClosestEnemy += RotateTo;
            _enemiesChecker.EnemyNotFound += RotateToForward;
            _heroLookAt.LookedAtEnemy += StopRotatingToEnemy;
        }

        private void OnEnable()
        {
            if (_progress != null)
                _progress.WeaponsData.CurrentWeaponChanged += WeaponChanged;

            if (_rotationItemData != null)
                _rotationItemData.LevelChanged += SetRotationSpeed;
        }

        private void OnDisable()
        {
            if (_progress != null)
                _progress.WeaponsData.CurrentWeaponChanged -= WeaponChanged;

            if (_rotationItemData != null)
                _rotationItemData.LevelChanged -= SetRotationSpeed;
        }

        public void Construct(IPlayerProgressService progressService, IStaticDataService staticDataService)
        {
            _progress = progressService.Progress;
            _staticDataService = staticDataService;
            _progress.WeaponsData.CurrentWeaponChanged += WeaponChanged;
            WeaponChanged();
        }

        private void WeaponChanged()
        {
            if (_rotationItemData != null)
                _rotationItemData.LevelChanged -= SetRotationSpeed;

            _weaponTypeId = _progress.WeaponsData.CurrentHeroWeaponTypeId;
            _baseRotationSpeed = _staticDataService.ForHeroWeapon(_weaponTypeId).RotationSpeed;
            _rotationItemData = _progress.WeaponsData.UpgradesData.UpgradeItemDatas.First(x =>
                x.WeaponTypeId == _weaponTypeId && x.UpgradeTypeId == UpgradeTypeId.Rotation);
            _rotationItemData.LevelChanged += SetRotationSpeed;
            SetRotationSpeed();
        }

        private void SetRotationSpeed()
        {
            _rotationItemData = _progress.WeaponsData.UpgradesData.UpgradeItemDatas.First(x =>
                x.WeaponTypeId == _weaponTypeId && x.UpgradeTypeId == UpgradeTypeId.Rotation);

            if (_rotationItemData.LevelTypeId == LevelTypeId.None)
                _rotationRatio = BaseRatio;
            else
                _rotationRatio = _staticDataService
                    .ForUpgradeLevelsInfo(_rotationItemData.UpgradeTypeId, _rotationItemData.LevelTypeId).Value;

            _rotationSpeed = _baseRotationSpeed * _rotationRatio;
        }

        public void TurnOff()
        {
            StopRotatingToEnemy();
        }

        private void RotateTo(GameObject enemy)
        {
            if (_rotatingCoroutine != null)
                StopCoroutine(_rotatingCoroutine);

            var position = enemy.transform.position;
            _shootPosition = new Vector3(position.x, position.y + Constants.AdditionYToEnemy, position.z);

            _rotatingCoroutine = StartCoroutine(CoroutineRotateTo(enemy));
        }

        private void RotateToForward()
        {
            if (_rotatingCoroutine != null)
                StopCoroutine(_rotatingCoroutine);

            _rotatingCoroutine = StartCoroutine(CoroutineRotateToForward());
        }

        private IEnumerator CoroutineRotateToForward()
        {
            StartedRotating?.Invoke();
            var position = transform.position;
            _shootPosition = new Vector3(position.x, position.y + Constants.AdditionYToEnemy, position.z + 50f);
            _direction = (_shootPosition - position).normalized;
            _targetRotation = Quaternion.LookRotation(_direction);
            float angle = Vector3.Angle(transform.forward, _direction);

            while (angle > 0f)
            {
                _shootPosition = new Vector3(position.x, position.y + Constants.AdditionYToEnemy, position.z + 50f);
                _direction = (_shootPosition - position).normalized;
                _targetRotation = Quaternion.LookRotation(_direction);
                angle = Vector3.Angle(transform.forward, _direction);

                // if (angle < AngleForFastRotating)
                //     transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation,
                //         SmoothRotationSpeed * _rotationRatio);
                // else
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _rotationSpeed);

                yield return null;
            }
        }

        private IEnumerator CoroutineRotateTo(GameObject enemy)
        {
            StartedRotating?.Invoke();
            _direction = (_shootPosition - transform.position).normalized;
            _targetRotation = Quaternion.LookRotation(_direction);
            float angle = Vector3.Angle(transform.forward, _direction);

            while (angle > 0f)
            {
                angle = Vector3.Angle(transform.forward, _direction);

                if (angle < AngleForFastRotating)
                {
                    // if (angle < MaxAngleForLookAt)
                    EndedRotatingToEnemy?.Invoke(enemy);
                    // else
                    //     transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, SmoothRotationSpeed);
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _rotationSpeed);
                }

                yield return null;
            }
        }

        private void StopRotatingToEnemy()
        {
            if (_rotatingCoroutine != null)
                StopCoroutine(_rotatingCoroutine);
        }
    }
}