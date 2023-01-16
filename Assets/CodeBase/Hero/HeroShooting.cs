using System.Collections;
using CodeBase.Data;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Weapon;
using CodeBase.UI.Screens.Level;
using CodeBase.Weapons;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroShooting : MonoBehaviour, IProgressSaver
    {
        [SerializeField] private WeaponModel _weaponModel;
        [SerializeField] private HudWeaponItemsContainer _weaponItemsContainer;

        private HeroRotating _heroRotating;
        private WeaponArmoryDescription _weaponArmoryDescription;
        private LevelStats _currentLevelStats;

        [Inject] private readonly IStaticDataService _staticData
            // = AllServices.Container.Single<IStaticDataService>()
            ;

        private EnemiesChecker _enemiesChecker;

        public void Construct()
        {
            // _weaponItemsContainer.Construct(AllServices.Container.Single<IPersistentProgressService>(),
            //     AllServices.Container.Single<IStaticDataService>(),
            //     AllServices.Container.Single<IUIFactory>());
            _weaponItemsContainer.Initialize();
        }

        private void Awake()
        {
            _heroRotating = GetComponent<HeroRotating>();
            _enemiesChecker = GetComponent<EnemiesChecker>();

            _heroRotating.ShootEnemy += ShootEnemy;
            _enemiesChecker.DirectionForEnemyChecked += ShootEnemy;
            _weaponItemsContainer.ItemClicked += ChangeWeaponItem;
        }

        private void Start()
        {
            CreateWeaponArmoryDescription();
        }

        private void ChangeWeaponItem(WeaponTypeId typeId)
        {
            if (_weaponModel.WeaponTypeId == typeId)
                return;

            // TODO(Finish it)
        }

        private void CreateWeaponArmoryDescription()
        {
            WeaponStaticData weaponStaticData = _staticData.ForWeaponUI(_weaponModel.WeaponTypeId);
            WeaponArmoryDescription description = new WeaponArmoryDescription(name: weaponStaticData.Name,
                mainFireDamage: weaponStaticData.MainFireDamage, mainFireCost: weaponStaticData.MainFireCost,
                mainFireCooldown: weaponStaticData.MainFireCooldown,
                mainFireBarrels: weaponStaticData.MainFireBarrels, mainFireRange: weaponStaticData.MainFireRange,
                mainFireBulletSpeed: weaponStaticData.MainFireBulletSpeed,
                mainFireRotatingSpeed: weaponStaticData.MainFireRotationSpeed);

            _weaponArmoryDescription = description;
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
            foreach (GameObject muzzle in _weaponModel.Muzzles)
            {
                Vector3 vfxPosition = muzzle.transform.position;
                Instantiate(_weaponModel.ShootVfx, vfxPosition, Quaternion.identity);
            }
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