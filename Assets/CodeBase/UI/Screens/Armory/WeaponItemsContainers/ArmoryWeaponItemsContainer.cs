using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using Zenject;

namespace CodeBase.UI.Screens.Armory.WeaponItemsContainers
{
    public abstract class ArmoryWeaponItemsContainer : WeaponItemsContainer
    {
        protected void Construct(IPlayerProgressService progressService, IStaticDataService staticData, IUIFactory uiFactory)
        {
            base.Construct(progressService, staticData, uiFactory);
        }

        protected new void Initialize()
        {
            ClearWeaponItems();
        }

        private void ClearWeaponItems()
        {
            // foreach (GameObject weaponItem in WeaponItemGameObjects)
            //     Destroy(weaponItem);
        }
    }
}