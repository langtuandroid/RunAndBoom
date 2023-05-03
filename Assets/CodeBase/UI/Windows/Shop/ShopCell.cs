using System;
using CodeBase.UI.Windows.Common;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopCell : MonoBehaviour
    {
        [SerializeField] private AmmoItemBase _ammoItemBase;
        [SerializeField] private ItemItemBase _itemItemBase;
        [SerializeField] private PerkItemBase _perkItemBase;
        [SerializeField] private UpgradeItemBase _upgradeItemBase;
        [SerializeField] private WeaponItemBase _weaponItemBase;

        Type _ammoType = typeof(AmmoItemBase);
        Type _itemType = typeof(ItemItemBase);
        Type _perkType = typeof(PerkItemBase);
        Type _upgradeType = typeof(UpgradeItemBase);
        Type _weaponType = typeof(WeaponItemBase);

        public ItemBase GetView(Type type) // where T : BaseItemView
        {
            if (type == _ammoType)
            {
                _ammoItemBase.gameObject.SetActive(true);
                _itemItemBase.gameObject.SetActive(false);
                _upgradeItemBase.gameObject.SetActive(false);
                _perkItemBase.gameObject.SetActive(false);
                _weaponItemBase.gameObject.SetActive(false);
                return _ammoItemBase;
            }

            if (type == _itemType)
            {
                _itemItemBase.gameObject.SetActive(true);
                _ammoItemBase.gameObject.SetActive(false);
                _upgradeItemBase.gameObject.SetActive(false);
                _perkItemBase.gameObject.SetActive(false);
                _weaponItemBase.gameObject.SetActive(false);
                return _itemItemBase;
            }

            if (type == _upgradeType)
            {
                _upgradeItemBase.gameObject.SetActive(true);
                _ammoItemBase.gameObject.SetActive(false);
                _itemItemBase.gameObject.SetActive(false);
                _perkItemBase.gameObject.SetActive(false);
                _weaponItemBase.gameObject.SetActive(false);
                return _upgradeItemBase;
            }

            if (type == _perkType)
            {
                _perkItemBase.gameObject.SetActive(true);
                _ammoItemBase.gameObject.SetActive(false);
                _itemItemBase.gameObject.SetActive(false);
                _upgradeItemBase.gameObject.SetActive(false);
                _weaponItemBase.gameObject.SetActive(false);
                return _perkItemBase;
            }

            if (type == _weaponType)
            {
                _weaponItemBase.gameObject.SetActive(true);
                _ammoItemBase.gameObject.SetActive(false);
                _itemItemBase.gameObject.SetActive(false);
                _upgradeItemBase.gameObject.SetActive(false);
                _perkItemBase.gameObject.SetActive(false);
                return _weaponItemBase;
            }
            else return _weaponItemBase;


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