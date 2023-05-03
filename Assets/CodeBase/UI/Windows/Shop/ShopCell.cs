using System;
using CodeBase.UI.Windows.Common;
using CodeBase.UI.Windows.Finish.Items;
using CodeBase.UI.Windows.Shop.Items;
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
        [SerializeField] private MoneyItemBase _moneyItemBase;

        Type _ammoShopType = typeof(AmmoShopItem);
        Type _itemShopType = typeof(ItemShopItem);
        Type _perkShopType = typeof(PerkShopItem);
        Type _upgradeShopType = typeof(UpgradeShopItem);
        Type _weaponShopType = typeof(WeaponShopItem);
        Type _ammoGiftType = typeof(AmmoGiftItem);
        Type _itemGiftType = typeof(ItemGiftItem);
        Type _perkGiftType = typeof(PerkGiftItem);
        Type _upgradeGiftType = typeof(UpgradeGiftItem);
        Type _weaponGiftType = typeof(WeaponGiftItem);
        Type _moneyGiftType = typeof(MoneyGiftItem);

        public ItemBase GetView(Type type)
        {
            if (type == _ammoShopType || type == _ammoGiftType)
            {
                _ammoItemBase.gameObject.SetActive(true);
                _itemItemBase.gameObject.SetActive(false);
                _upgradeItemBase.gameObject.SetActive(false);
                _perkItemBase.gameObject.SetActive(false);
                _weaponItemBase.gameObject.SetActive(false);
                _moneyItemBase?.gameObject.SetActive(false);
                return _ammoItemBase;
            }

            if (type == _itemShopType || type == _itemGiftType)
            {
                _itemItemBase.gameObject.SetActive(true);
                _ammoItemBase.gameObject.SetActive(false);
                _upgradeItemBase.gameObject.SetActive(false);
                _perkItemBase.gameObject.SetActive(false);
                _weaponItemBase.gameObject.SetActive(false);
                _moneyItemBase?.gameObject.SetActive(false);
                return _itemItemBase;
            }

            if (type == _upgradeShopType || type == _upgradeGiftType)
            {
                _upgradeItemBase.gameObject.SetActive(true);
                _ammoItemBase.gameObject.SetActive(false);
                _itemItemBase.gameObject.SetActive(false);
                _perkItemBase.gameObject.SetActive(false);
                _weaponItemBase.gameObject.SetActive(false);
                _moneyItemBase?.gameObject.SetActive(false);
                return _upgradeItemBase;
            }

            if (type == _perkShopType || type == _perkGiftType)
            {
                _perkItemBase.gameObject.SetActive(true);
                _ammoItemBase.gameObject.SetActive(false);
                _itemItemBase.gameObject.SetActive(false);
                _upgradeItemBase.gameObject.SetActive(false);
                _weaponItemBase.gameObject.SetActive(false);
                _moneyItemBase?.gameObject.SetActive(false);
                return _perkItemBase;
            }

            if (type == _weaponShopType || type == _weaponGiftType)
            {
                _weaponItemBase.gameObject.SetActive(true);
                _ammoItemBase.gameObject.SetActive(false);
                _itemItemBase.gameObject.SetActive(false);
                _upgradeItemBase.gameObject.SetActive(false);
                _perkItemBase.gameObject.SetActive(false);
                _moneyItemBase?.gameObject.SetActive(false);
                return _weaponItemBase;
            }

            if (type == _moneyGiftType)
            {
                _moneyItemBase.gameObject.SetActive(true);
                _weaponItemBase.gameObject.SetActive(false);
                _ammoItemBase.gameObject.SetActive(false);
                _itemItemBase.gameObject.SetActive(false);
                _upgradeItemBase.gameObject.SetActive(false);
                _perkItemBase.gameObject.SetActive(false);
                return _moneyItemBase;
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