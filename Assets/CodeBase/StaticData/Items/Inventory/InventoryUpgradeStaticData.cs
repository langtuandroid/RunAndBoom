using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using UnityEngine;

namespace CodeBase.StaticData.Items.Inventory
{
    [CreateAssetMenu(fileName = "InventoryUpgradeData", menuName = "StaticData/Items/Inventory/Upgrade")]
    public class InventoryUpgradeStaticData : BaseItemStaticData
    {
        public UpgradeTypeId UpgradeTypeId;
    }
}