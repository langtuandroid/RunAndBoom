using CodeBase.Services.Input.Platforms;
using CodeBase.StaticData.ProjectileTrace;
using CodeBase.StaticData.Weapon;
using CodeBase.Weapons;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroShooting : MonoBehaviour
    {
        private IPlatformInputService _platformInputService;

        private HeroWeaponSelection _heroWeaponSelection;
        private EnemiesChecker _enemiesChecker;
        private WeaponAppearance _weaponAppearance;
        private bool _enemySpotted = false;
        private float _currentAttackCooldown = 0f;
        private float _weaponCooldown = 0f;

        private void Awake()
        {
            _heroWeaponSelection = transform.gameObject.GetComponentInChildren<HeroWeaponSelection>();
            _enemiesChecker = GetComponent<EnemiesChecker>();
            _heroWeaponSelection.WeaponSelected += GetCurrentWeaponObject;
            _enemiesChecker.EnemyVisibilityChecked += EnemySpotted;
            _enemiesChecker.EnemyNotFound += EnemyNotSpotted;
            _platformInputService.Shot += TryShoot;
        }

        private void GetCurrentWeaponObject(GameObject weaponPrefab, WeaponStaticData weaponStaticData, ProjectileTraceStaticData projectileTraceStaticData)
        {
            _weaponAppearance = weaponPrefab.GetComponent<WeaponAppearance>();
            _weaponCooldown = weaponStaticData.Cooldown;
        }

        private void Update() =>
            UpdateCooldown();

        [Inject]
        public void Construct(IPlatformInputService platformInputService)
        {
            _platformInputService = platformInputService;
        }

        private void UpdateCooldown()
        {
            if (!CooldownUp())
                _currentAttackCooldown -= Time.deltaTime;
        }

        private bool CooldownUp() =>
            _currentAttackCooldown <= 0;

        private void EnemyNotSpotted() =>
            _enemySpotted = false;

        private void OnDisable()
        {
            _enemiesChecker.EnemyVisibilityChecked -= EnemySpotted;
            _enemiesChecker.EnemyNotFound -= EnemyNotSpotted;
            _platformInputService.Shot -= TryShoot;
        }

        private void EnemySpotted() =>
            _enemySpotted = true;

        private void TryShoot()
        {
            if (_enemySpotted && CooldownUp())
                Shoot();
        }

        private void Shoot()
        {
            Debug.Log("Shoot");
            _currentAttackCooldown = _weaponCooldown;
            _weaponAppearance.LaunchProjectile();
        }
    }
}