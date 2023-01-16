using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Weapon;
using CodeBase.UI.Services;
using CodeBase.Weapons;
using UnityEngine;

namespace CodeBase.UI.Screens.Armory.WeaponItems
{
    public class AvailableArmoryWeaponItem : ArmoryWeaponItem
    {
        public void Construct(IPlayerProgressService progressService,
            GameObject armoryWeaponItemsContainerGameObject, Sprite sprite, WeaponTypeId typeId, bool isSelected,
            WeaponArmoryDescription description)
        {
            base.Construct(progressService, armoryWeaponItemsContainerGameObject, sprite, typeId, description);
            IsSelected = isSelected;
        }

        public new void Initialize()
        {
            base.Initialize();
            SetAlpha();
        }

        protected override void OnClick()
        {
            ChangeIconAlpha();
            WeaponItemsContainer.OnItemClick(WeaponTypeId);
        }

        private void ChangeIconAlpha()
        {
            SetAlpha();
            IsSelected = !IsSelected;
        }

        private void SetAlpha()
        {
            float alpha = IsSelected ? Constants.AlphaSelectedItem : Constants.AlphaUnselectedItem;
            Icon.ChangeImageAlpha(alpha);
        }
    }
}