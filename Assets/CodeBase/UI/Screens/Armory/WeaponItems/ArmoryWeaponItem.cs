using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Weapon;
using CodeBase.Weapons;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Screens.Armory.WeaponItems
{
    public abstract class ArmoryWeaponItem : WeaponItem
    {
        [SerializeField] private TextMeshProUGUI _nameText;

        [SerializeField] private TextMeshProUGUI _mainFireCostText;
        // [SerializeField] private TextMeshProUGUI _secondaryFireCountText;

        public new void Construct(IPlayerProgressService progressService,
            GameObject armoryWeaponItemsContainerGameObject, Sprite sprite, WeaponTypeId weaponTypeId,
            WeaponArmoryDescription description)
        {
            base.Construct(progressService, armoryWeaponItemsContainerGameObject, sprite, weaponTypeId, description);
        }

        protected new void Initialize()
        {
            base.Initialize();
            _nameText.text = Description.Name;
            _mainFireCostText.text = $"{Description.MainFireCost} Points";
        }
    }
}