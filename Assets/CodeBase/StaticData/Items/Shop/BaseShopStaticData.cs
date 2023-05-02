using UnityEngine;

namespace CodeBase.StaticData.Items.Shop
{
    public abstract class BaseShopStaticData : BaseItemStaticData
    {
        [Range(1, 50)] public int Cost;
        [SerializeField] public string RuTitle;
        [SerializeField] public string EnTitle;
        [SerializeField] public string TrTitle;
    }
}