namespace CodeBase.UI.Screens.Armory.WeaponItemsContainers
{
    public abstract class ArmoryWeaponItemsContainer : WeaponItemsContainer
    {
        // protected new void Construct(IPersistentProgressService progressService, IStaticDataService staticData,
        //     IUIFactory uiFactory)
        // {
        // base.Construct(progressService, staticData, uiFactory);
        // }

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