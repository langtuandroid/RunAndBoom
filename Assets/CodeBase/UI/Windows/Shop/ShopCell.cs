using System;
using CodeBase.UI.Windows.Shop.ViewItems;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopCell : MonoBehaviour
    {
        [SerializeField] private AmmoPurchasingItemView _ammoPurchasingItem;
        [SerializeField] private ItemPurchasingItemView _itemPurchasingItem;
        [SerializeField] private UpgradePurchasingItemView _upgradePurchasingItem;
        [SerializeField] private PerkPurchasingItemView _perkPurchasingItem;
        [SerializeField] private WeaponPurchasingItemView _weaponPurchasingItem;

        Type _ammoType = typeof(AmmoPurchasingItemView);
        Type _itemType = typeof(ItemPurchasingItemView);
        Type _upgradeType = typeof(UpgradePurchasingItemView);
        Type _perkType = typeof(PerkPurchasingItemView);
        Type _weaponType = typeof(WeaponPurchasingItemView);

        public BaseItemView GetView(Type type) // where T : BaseItemView
        {
            if (type == _ammoType)
            {
                _ammoPurchasingItem.gameObject.SetActive(true);
                _itemPurchasingItem.gameObject.SetActive(false);
                _upgradePurchasingItem.gameObject.SetActive(false);
                _perkPurchasingItem.gameObject.SetActive(false);
                _weaponPurchasingItem.gameObject.SetActive(false);
                return _ammoPurchasingItem;
            }

            if (type == _itemType)
            {
                _itemPurchasingItem.gameObject.SetActive(true);
                _ammoPurchasingItem.gameObject.SetActive(false);
                _upgradePurchasingItem.gameObject.SetActive(false);
                _perkPurchasingItem.gameObject.SetActive(false);
                _weaponPurchasingItem.gameObject.SetActive(false);
                return _itemPurchasingItem;
            }

            if (type == _upgradeType)
            {
                _upgradePurchasingItem.gameObject.SetActive(true);
                _ammoPurchasingItem.gameObject.SetActive(false);
                _itemPurchasingItem.gameObject.SetActive(false);
                _perkPurchasingItem.gameObject.SetActive(false);
                _weaponPurchasingItem.gameObject.SetActive(false);
                return _upgradePurchasingItem;
            }

            if (type == _perkType)
            {
                _perkPurchasingItem.gameObject.SetActive(true);
                _ammoPurchasingItem.gameObject.SetActive(false);
                _itemPurchasingItem.gameObject.SetActive(false);
                _upgradePurchasingItem.gameObject.SetActive(false);
                _weaponPurchasingItem.gameObject.SetActive(false);
                return _perkPurchasingItem;
            }

            if (type == _weaponType)
            {
                _weaponPurchasingItem.gameObject.SetActive(true);
                _ammoPurchasingItem.gameObject.SetActive(false);
                _itemPurchasingItem.gameObject.SetActive(false);
                _upgradePurchasingItem.gameObject.SetActive(false);
                _perkPurchasingItem.gameObject.SetActive(false);
                return _weaponPurchasingItem;
            }
            else return _weaponPurchasingItem;


            // switch (type)
            // {
            //     case ammotype:
            //         _ammoPurchasingItem.gameObject.SetActive(true);
            //         _itemPurchasingItem.gameObject.SetActive(false);
            //         _upgradePurchasingItem.gameObject.SetActive(false);
            //         _perkPurchasingItem.gameObject.SetActive(false);
            //         _weaponPurchasingItem.gameObject.SetActive(false);
            //         return _ammoPurchasingItem;
            //
            //     case ItemPurchasingItemView:
            //         _itemPurchasingItem.gameObject.SetActive(true);
            //         _ammoPurchasingItem.gameObject.SetActive(false);
            //         _upgradePurchasingItem.gameObject.SetActive(false);
            //         _perkPurchasingItem.gameObject.SetActive(false);
            //         _weaponPurchasingItem.gameObject.SetActive(false);
            //         return _itemPurchasingItem;
            //
            //     case UpgradePurchasingItemView:
            //         _upgradePurchasingItem.gameObject.SetActive(true);
            //         _ammoPurchasingItem.gameObject.SetActive(false);
            //         _itemPurchasingItem.gameObject.SetActive(false);
            //         _perkPurchasingItem.gameObject.SetActive(false);
            //         _weaponPurchasingItem.gameObject.SetActive(false);
            //         return _upgradePurchasingItem;
            //
            //     case PerkPurchasingItemView:
            //         _perkPurchasingItem.gameObject.SetActive(true);
            //         _ammoPurchasingItem.gameObject.SetActive(false);
            //         _itemPurchasingItem.gameObject.SetActive(false);
            //         _upgradePurchasingItem.gameObject.SetActive(false);
            //         _weaponPurchasingItem.gameObject.SetActive(false);
            //         return _perkPurchasingItem;
            //
            //     case WeaponPurchasingItemView:
            //         _weaponPurchasingItem.gameObject.SetActive(true);
            //         _ammoPurchasingItem.gameObject.SetActive(false);
            //         _itemPurchasingItem.gameObject.SetActive(false);
            //         _upgradePurchasingItem.gameObject.SetActive(false);
            //         _perkPurchasingItem.gameObject.SetActive(false);
            //         return _weaponPurchasingItem;
            //
            //     default:
            //         return _weaponPurchasingItem;
            // }
        }
    }
}