using CodeBase.Data;
using CodeBase.StaticData.Weapon;
using CodeBase.UI.Screens.Level;
using CodeBase.Weapons;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroWeaponChanger : MonoBehaviour
    {
        [SerializeField] private WeaponModel _weaponModel;
        [SerializeField] private HudWeaponItemsContainer _weaponItemsContainer;

        private HeroRotating _heroRotating;
        private WeaponArmoryDescription _weaponArmoryDescription;
        private LevelStats _currentLevelStats;

        public void Construct()
        {
            // _weaponItemsContainer.Construct(AllServices.Container.Single<IPersistentProgressService>(),
            //     AllServices.Container.Single<IStaticDataService>(),
            //     AllServices.Container.Single<IUIFactory>());
            _weaponItemsContainer.Initialize();
        }

        private void OnEnable()
        {
            _weaponItemsContainer.ItemClicked += ChangeWeaponItem;
        }

        private void ChangeWeaponItem(WeaponTypeId typeId)
        {
            if (_weaponModel.WeaponTypeId == typeId)
                return;

            // TODO(Finish it)
        }
    }
}