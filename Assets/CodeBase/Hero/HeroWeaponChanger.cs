using CodeBase.Data;
using CodeBase.StaticData.Weapon;
using CodeBase.Weapons;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroWeaponChanger : MonoBehaviour
    {
        [SerializeField] private WeaponModel _weaponModel;

        private HeroRotating _heroRotating;
        private WeaponArmoryDescription _weaponArmoryDescription;
        private LevelStats _currentLevelStats;

        public void Construct()
        {
            // _weaponItemsContainer.Construct(AllServices.Container.Single<IPersistentProgressService>(),
            //     AllServices.Container.Single<IStaticDataService>(),
            //     AllServices.Container.Single<IUIFactory>());
        }

        private void OnEnable()
        {
        }

        private void ChangeWeaponItem(WeaponTypeId typeId)
        {
            if (_weaponModel.WeaponTypeId == typeId)
                return;

            // TODO(Finish it)
        }
    }
}