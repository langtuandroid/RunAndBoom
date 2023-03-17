using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.Items
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "StaticData/Items/Shop/Item")]
    public class ShopItemStaticData : BaseShopStaticData
    {
        public ItemTypeId TypeId;
    }
}