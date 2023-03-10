using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.Items
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "StaticData/Items/Shop/Item")]
    public class ItemStaticData : BaseShopStaticData
    {
        public ItemTypeId TypeId;
    }
}