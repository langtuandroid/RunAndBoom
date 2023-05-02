using System;
using CodeBase.UI.Windows.Common;
using CodeBase.UI.Windows.Shop.Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopCell : MonoBehaviour
    {
        [FormerlySerializedAs("ammoShopItemShopItem")]
        [FormerlySerializedAs("ammoPurchasingShopItem")]
        [FormerlySerializedAs("_ammoPurchasingItem")]
        [SerializeField]
        private AmmoShopItem ammoItemItem;

        [FormerlySerializedAs("shopShopItemPurchasingShopShopItem")]
        [FormerlySerializedAs("shopItemPurchasingShopItem")]
        [FormerlySerializedAs("_itemPurchasingItem")]
        [SerializeField]
        private ItemShopItem itemShopItemItemShopItem;

        [FormerlySerializedAs("upgradePurchasingShopItem")]
        [FormerlySerializedAs("_upgradePurchasingItem")]
        [SerializeField]
        private UpgradeShopItem upgradeShopItemShopItem;

        [FormerlySerializedAs("perkPurchasingShopItem")] [FormerlySerializedAs("_perkPurchasingItem")] [SerializeField]
        private PerkShopItem perkShopItemShopItem;

        [FormerlySerializedAs("weaponPurchasingShopItem")]
        [FormerlySerializedAs("_weaponPurchasingItem")]
        [SerializeField]
        private WeaponShopItem weaponShopItemShopItem;

        Type _ammoType = typeof(AmmoShopItem);
        Type _itemType = typeof(ItemShopItem);
        Type _upgradeType = typeof(UpgradeShopItem);
        Type _perkType = typeof(PerkShopItem);
        Type _weaponType = typeof(WeaponShopItem);

        public ItemBase GetView(Type type) // where T : BaseItemView
        {
            if (type == _ammoType)
            {
                ammoItemItem.gameObject.SetActive(true);
                itemShopItemItemShopItem.gameObject.SetActive(false);
                upgradeShopItemShopItem.gameObject.SetActive(false);
                perkShopItemShopItem.gameObject.SetActive(false);
                weaponShopItemShopItem.gameObject.SetActive(false);
                return ammoItemItem;
            }

            if (type == _itemType)
            {
                itemShopItemItemShopItem.gameObject.SetActive(true);
                ammoItemItem.gameObject.SetActive(false);
                upgradeShopItemShopItem.gameObject.SetActive(false);
                perkShopItemShopItem.gameObject.SetActive(false);
                weaponShopItemShopItem.gameObject.SetActive(false);
                return itemShopItemItemShopItem;
            }

            if (type == _upgradeType)
            {
                upgradeShopItemShopItem.gameObject.SetActive(true);
                ammoItemItem.gameObject.SetActive(false);
                itemShopItemItemShopItem.gameObject.SetActive(false);
                perkShopItemShopItem.gameObject.SetActive(false);
                weaponShopItemShopItem.gameObject.SetActive(false);
                return upgradeShopItemShopItem;
            }

            if (type == _perkType)
            {
                perkShopItemShopItem.gameObject.SetActive(true);
                ammoItemItem.gameObject.SetActive(false);
                itemShopItemItemShopItem.gameObject.SetActive(false);
                upgradeShopItemShopItem.gameObject.SetActive(false);
                weaponShopItemShopItem.gameObject.SetActive(false);
                return perkShopItemShopItem;
            }

            if (type == _weaponType)
            {
                weaponShopItemShopItem.gameObject.SetActive(true);
                ammoItemItem.gameObject.SetActive(false);
                itemShopItemItemShopItem.gameObject.SetActive(false);
                upgradeShopItemShopItem.gameObject.SetActive(false);
                perkShopItemShopItem.gameObject.SetActive(false);
                return weaponShopItemShopItem;
            }
            else return weaponShopItemShopItem;


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