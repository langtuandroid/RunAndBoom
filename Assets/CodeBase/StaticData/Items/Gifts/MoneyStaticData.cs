using UnityEngine;

namespace CodeBase.StaticData.Items.Gifts
{
    [CreateAssetMenu(fileName = "MoneyData", menuName = "StaticData/Items/Gifts/Money")]
    public class MoneyStaticData : BaseItemStaticData
    {
        public MoneyTypeId TypeId;
        [Range(0, 50)] public int Value;
    }
}