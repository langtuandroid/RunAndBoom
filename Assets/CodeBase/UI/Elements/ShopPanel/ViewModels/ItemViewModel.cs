using UnityEngine;

namespace CodeBase.UI.Elements.ShopPanel.ViewModels
{
    public class ItemViewModel
    {
        public Sprite MainImage { get; }
        public int Cost { get; }
        public string RuTitle { get; }
        public string EnTitle { get; }
        public string TrTitle { get; }

        public ItemViewModel(Sprite mainImage, int cost, string ruTitle, string enTitle, string trTitle)
        {
            MainImage = mainImage;
            Cost = cost;
            RuTitle = ruTitle;
            EnTitle = enTitle;
            TrTitle = trTitle;
        }
    }
}