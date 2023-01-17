using System;
using System.Collections.Generic;
using CodeBase.CustomClasses;
using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Weapon;
using CodeBase.UI.Services.Factory;
using CodeBase.Weapons;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Screens.Level
{
    public class HudWeaponItemsContainer : WeaponItemsContainer, IProgressSaver
    {
        public Action<WeaponTypeId> ItemClicked;
        private static List<GameObject> _weaponItemGameObjects = new List<GameObject>();

        [Inject]
        public  void Construct(IPlayerProgressService progressService, IStaticDataService staticData,
            IUIFactory uiFactory)
        {
            base.Construct(progressService, staticData, uiFactory);
        }

        public new void Initialize()
        {
            base.Initialize();
        }

        private async void FillWeaponItems(LinkedHashSet<WeaponTypeId> weaponTypeIds)
        {
            foreach (WeaponTypeId typeId in weaponTypeIds)
            {
                GameObject weaponItemPrefab = await UIFactory.CreateLevelWeaponItem(gameObject.transform);
                WeaponStaticData weaponStaticData = StaticData.ForWeaponUI(typeId);
                LevelWeaponItem levelWeaponItem = weaponItemPrefab.GetComponent<LevelWeaponItem>();

                WeaponArmoryDescription description = new WeaponArmoryDescription(name: weaponStaticData.Name,
                    mainFireDamage: weaponStaticData.MainFireDamage, mainFireCost: weaponStaticData.MainFireCost,
                    mainFireCooldown: weaponStaticData.MainFireCooldown,
                    mainFireBarrels: weaponStaticData.MainFireBarrels, mainFireRange: weaponStaticData.MainFireRange,
                    mainFireBulletSpeed: weaponStaticData.MainFireBulletSpeed,
                    mainFireRotatingSpeed: weaponStaticData.MainFireRotationSpeed);

                levelWeaponItem.Construct(ProgressService, gameObject, weaponStaticData.Icon, typeId, description);
                levelWeaponItem.Initialize();
                levelWeaponItem.Subscribe();
                _weaponItemGameObjects.Add(levelWeaponItem.gameObject);
                // _weaponItemGameObjects.Add(levelWeaponItem.gameObject);
            }
        }

        public override void OnItemClick(WeaponTypeId typeId) =>
            ItemClicked?.Invoke(typeId);

        public void LoadProgress(PlayerProgress progress)
        {
            FillWeaponItems(progress.SelectedWeaponTypeIds);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }
    }
}