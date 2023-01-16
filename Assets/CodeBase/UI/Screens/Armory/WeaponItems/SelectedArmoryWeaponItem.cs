namespace CodeBase.UI.Screens.Armory.WeaponItems
{
    public class SelectedArmoryWeaponItem : ArmoryWeaponItem
    {
        public new void Initialize()
        {
            base.Initialize();
            ClickItemButton.onClick.AddListener(OnClick);
        }

        protected override void OnClick()
        {
            WeaponItemsContainer.OnItemClick(WeaponTypeId);
        }
    }
}