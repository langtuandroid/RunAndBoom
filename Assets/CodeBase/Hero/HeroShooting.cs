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
        [SerializeField] private HeroWeaponSelection _heroWeaponSelection;
        [SerializeField] private EnemiesChecker _enemiesChecker;

        private IPlatformInputService _platformInputService;
        private WeaponAppearance _weaponAppearance;
        private bool _enemySpotted = false;
        private float _currentAttackCooldown = 0f;
        private float _weaponCooldown = 0f;
        private Vector3 _enemyPosition;

        private void Awake()
        {
            _heroWeaponSelection.WeaponSelected += GetCurrentWeaponObject;
            _enemiesChecker.FoundClosestEnemy += EnemySpotted;
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
        public void Construct(IPlatformInputService platformInputService) =>
            _platformInputService = platformInputService;

        private void UpdateCooldown()
        {
            if (!CooldownUp())
                _currentAttackCooldown -= Time.deltaTime;
        }

        private bool CooldownUp() =>
            _currentAttackCooldown <= 0;

        private void EnemyNotSpotted() =>
            _enemySpotted = false;

        // private void OnDisable()
        // {
        //     _enemiesChecker.FoundClosestEnemy -= EnemySpotted;
        //     _enemiesChecker.EnemyNotFound -= EnemyNotSpotted;
        //     _platformInputService.Shot -= TryShoot;
        // }

        private void EnemySpotted(GameObject enemy)
            // private void EnemySpotted(EnemyHealth enemy)
        {
            _enemyPosition = enemy.gameObject.transform.position;
            _enemySpotted = true;
        }

        private void TryShoot()
        {
            if (_enemySpotted && CooldownUp())
                Shoot();
        }

        private void Shoot()
        {
            Debug.Log("Shoot");
            _currentAttackCooldown = _weaponCooldown;
            _weaponAppearance.LaunchProjectile(_enemyPosition);
        }
    }
}