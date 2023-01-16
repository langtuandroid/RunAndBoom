using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Weapon;
using CodeBase.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens
{
    public abstract class WeaponItem : MonoBehaviour
    {
        public Image Icon;
        public Button ClickItemButton;
        public WeaponItemsContainer WeaponItemsContainer;
        private GameObject _weaponItemsContainerGameObject;

        // protected WeaponItemsContainer WeaponItemsContainer { get; private set; }
        public WeaponTypeId WeaponTypeId;

        protected IPlayerProgressService ProgressService;
        private Sprite _sprite;
        protected WeaponArmoryDescription Description;
        protected bool IsSelected;

        private void Start()
        {
            ClickItemButton.onClick.AddListener(OnClick);
        }

        public void RemoveListener()
        {
            ClickItemButton.onClick.RemoveListener(OnClick);
        }

        protected void Construct(IPlayerProgressService progressService,
            GameObject weaponItemsContainerGameObject, Sprite sprite,
            WeaponTypeId weaponTypeId, WeaponArmoryDescription description)
        {
            ProgressService = progressService;
            // WeaponItemsContainer = weaponItemsContainerGameObject.GetComponent<WeaponItemsContainer>();
            // WeaponItemsContainer = weaponItemsContainer;
            _sprite = sprite;
            WeaponTypeId = weaponTypeId;
            Description = description;
        }

        protected void Initialize()
        {
            Icon.sprite = _sprite;
        }

        protected abstract void OnClick();
    }
}