using System.Collections;
using CodeBase.Data;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.Weapons;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroShooting : MonoBehaviour, IProgressSaver
    {
        [SerializeField] private GameObject _shootVfx;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _shootPosition;

        private IStaticDataService _staticDataService;

        private HeroRotating _heroRotating;
        private WeaponArmoryDescription _weaponArmoryDescription;
        private LevelStats _currentLevelStats;
        private EnemiesChecker _enemiesChecker;

        private void Awake()
        {
            _heroRotating = GetComponent<HeroRotating>();
            _enemiesChecker = GetComponent<EnemiesChecker>();

            _heroRotating.ShootEnemy += ShootEnemy;
            _enemiesChecker.DirectionForEnemyChecked += ShootEnemy;
        }

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        private void OnDisable()
        {
            _heroRotating.ShootEnemy -= ShootEnemy;
        }

        private void ShootEnemy(IHealth enemyHealth)
        {
            if (_currentLevelStats.ScoreData.Score <= _weaponArmoryDescription.MainFireCost)
                return;

            StartCoroutine(DoShootEnemy(enemyHealth));
        }

        private IEnumerator DoShootEnemy(IHealth enemyHealth)
        {
            CreateShotVfx();
            _currentLevelStats.ScoreData.ReduceScore(_weaponArmoryDescription.MainFireCost);
            enemyHealth?.TakeDamage(_weaponArmoryDescription.MainFireDamage);
            WaitForSeconds wait = new WaitForSeconds(_weaponArmoryDescription.MainFireCooldown);
            yield return wait;
        }

        private void CreateShotVfx()
        {
            Instantiate(_shootVfx, _shootPosition.position, Quaternion.identity);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _currentLevelStats = progress.CurrentLevelStats;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }
    }
}