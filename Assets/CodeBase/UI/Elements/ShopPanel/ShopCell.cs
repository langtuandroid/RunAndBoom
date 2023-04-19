using CodeBase.UI.Elements.ShopPanel.ViewItems;
using UnityEngine;

namespace CodeBase.UI.Elements.ShopPanel
{
    public class ShopCell : MonoBehaviour
    {
        [SerializeField] private AmmoPurchasingItemView _ammoPurchasingItem;
        [SerializeField] private ItemPurchasingItemView _itemPurchasingItem;
        [SerializeField] private UpgradePurchasingItemView _upgradePurchasingItem;
        [SerializeField] private PerkPurchasingItemView _perkPurchasingItem;
        [SerializeField] private WeaponPurchasingItemView _weaponPurchasingItem;

        private void HideAll()
        {
            _ammoPurchasingItem.gameObject.SetActive(false);
            _itemPurchasingItem.gameObject.SetActive(false);
            _upgradePurchasingItem.gameObject.SetActive(false);
            _perkPurchasingItem.gameObject.SetActive(false);
            _weaponPurchasingItem.gameObject.SetActive(false);
        }

        public void Show(BasePurchasingItemView view)
        {
            switch (view)
            {
                case AmmoPurchasingItemView:
                    _ammoPurchasingItem.gameObject.SetActive(true);
                    _itemPurchasingItem.gameObject.SetActive(false);
                    _upgradePurchasingItem.gameObject.SetActive(false);
                    _perkPurchasingItem.gameObject.SetActive(false);
                    _weaponPurchasingItem.gameObject.SetActive(false);
                    break;

                case ItemPurchasingItemView:
                    _itemPurchasingItem.gameObject.SetActive(true);
                    _ammoPurchasingItem.gameObject.SetActive(false);
                    _upgradePurchasingItem.gameObject.SetActive(false);
                    _perkPurchasingItem.gameObject.SetActive(false);
                    _weaponPurchasingItem.gameObject.SetActive(false);
                    break;

                case UpgradePurchasingItemView:
                    _upgradePurchasingItem.gameObject.SetActive(true);
                    _ammoPurchasingItem.gameObject.SetActive(false);
                    _itemPurchasingItem.gameObject.SetActive(false);
                    _perkPurchasingItem.gameObject.SetActive(false);
                    _weaponPurchasingItem.gameObject.SetActive(false);
                    break;

                case PerkPurchasingItemView:
                    _perkPurchasingItem.gameObject.SetActive(true);
                    _ammoPurchasingItem.gameObject.SetActive(false);
                    _itemPurchasingItem.gameObject.SetActive(false);
                    _upgradePurchasingItem.gameObject.SetActive(false);
                    _weaponPurchasingItem.gameObject.SetActive(false);
                    break;

                case WeaponPurchasingItemView:
                    _weaponPurchasingItem.gameObject.SetActive(true);
                    _ammoPurchasingItem.gameObject.SetActive(false);
                    _itemPurchasingItem.gameObject.SetActive(false);
                    _upgradePurchasingItem.gameObject.SetActive(false);
                    _perkPurchasingItem.gameObject.SetActive(false);
                    break;
            }
        }
    }
}