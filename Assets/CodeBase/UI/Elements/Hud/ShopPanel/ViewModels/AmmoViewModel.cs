using UnityEngine;

namespace CodeBase.UI.Elements.Hud.ShopPanel.ViewModels
{
    public class AmmoViewModel
    {
        public Sprite MainImage { get; }
        public int Cost { get; }
        public int Count { get; }
        public string RuTitle { get; }
        public string EnTitle { get; }
        public string TrTitle { get; }

        public AmmoViewModel(Sprite mainImage, int cost, int count, string ruTitle, string enTitle, string trTitle)
        {
            MainImage = mainImage;
            Cost = cost;
            Count = count;
            RuTitle = ruTitle;
            EnTitle = enTitle;
            TrTitle = trTitle;
        }
    }
}