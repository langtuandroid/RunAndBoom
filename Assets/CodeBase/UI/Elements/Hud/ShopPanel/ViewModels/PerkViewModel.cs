using UnityEngine;

namespace CodeBase.UI.Elements.Hud.ShopPanel.ViewModels
{
    public class PerkViewModel
    {
        public Sprite MainImage { get; }
        public Sprite LevelImage { get; }
        public int Cost { get; }
        public string RuTitle { get; }
        public string EnTitle { get; }
        public string TrTitle { get; }

        public PerkViewModel(Sprite mainImage, Sprite levelImage, int cost, string ruTitle, string enTitle, string trTitle)
        {
            MainImage = mainImage;
            LevelImage = levelImage;
            Cost = cost;
            RuTitle = ruTitle;
            EnTitle = enTitle;
            TrTitle = trTitle;
        }
    }
}