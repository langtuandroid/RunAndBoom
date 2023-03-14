using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.ProjectileTraces;
using CodeBase.StaticData.Weapons;
using CodeBase.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Hero
{
    public class HeroShooting : MonoBehaviour
    {
        [SerializeField] private HeroWeaponSelection _heroWeaponSelection;

        [FormerlySerializedAs("_enemiesChecker")] [SerializeField]
        private EnemiesCheckerView enemiesCheckerView;

        private IPlayerProgressService _progressService;
        private HeroWeaponAppearance _heroWeaponAppearance;
        private bool _enemySpotted = false;
        private float _currentAttackCooldown = 0f;
        private float _weaponCooldown = 0f;
        private Vector3 _enemyPosition;

        private void Awake()
        {
            _progressService = AllServices.Container.Single<IPlayerProgressService>();

            _heroWeaponSelection.WeaponSelected += GetCurrentWeaponObject;
            enemiesCheckerView.FoundClosestEnemy += EnemySpotted;
            enemiesCheckerView.EnemyNotFound += EnemyNotSpotted;
        }

        private void GetCurrentWeaponObject(GameObject weaponPrefab, HeroWeaponStaticData heroWeaponStaticData,
            ProjectileTraceStaticData projectileTraceStaticData)
        {
            _heroWeaponAppearance = weaponPrefab.GetComponent<HeroWeaponAppearance>();
            _weaponCooldown = heroWeaponStaticData.Cooldown;
        }

        private void Update()
        {
            UpdateCooldown();

            if (Input.GetMouseButton(0))
                TryShoot();
        }

        private void UpdateCooldown()
        {
            if (!CooldownUp())
                _currentAttackCooldown -= Time.deltaTime;
        }

        private void EnemyNotSpotted() =>
            _enemySpotted = false;

        private void OnDisable()
        {
            enemiesCheckerView.FoundClosestEnemy -= EnemySpotted;
            enemiesCheckerView.EnemyNotFound -= EnemyNotSpotted;
        }

        private void EnemySpotted(GameObject enemy)
        {
            _enemyPosition = enemy.gameObject.transform.position;
            _enemySpotted = true;
        }

        private void TryShoot()
        {
            if (_enemySpotted && CooldownUp() && IsAvailableAmmo())
                Shoot();
        }

        private bool CooldownUp() =>
            _currentAttackCooldown <= 0;

        private bool IsAvailableAmmo() =>
            _progressService.Progress.WeaponsData.WeaponsAmmo.IsAmmoAvailable();

        private void Shoot()
        {
            _progressService.Progress.WeaponsData.WeaponsAmmo.ReduceAmmo();
            _currentAttackCooldown = _weaponCooldown;
            _heroWeaponAppearance.ShootTo(_enemyPosition);
        }
    }
}