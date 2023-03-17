using UnityEngine;

namespace CodeBase.StaticData.Items.Shop
{
    public abstract class BaseShopStaticData : BaseItemStaticData, IShopItemCost, IShopItemTitle
    {
        [Range(1, 50)] public int Cost;
        [SerializeField] private string RuTitle;
        [SerializeField] private string EnTitle;
        [SerializeField] private string TrTitle;

        public int ICost => Cost;
        public string IRuTitle => RuTitle;
        public string IEnTitle => EnTitle;
        public string ITrTitle => TrTitle;
    }
}