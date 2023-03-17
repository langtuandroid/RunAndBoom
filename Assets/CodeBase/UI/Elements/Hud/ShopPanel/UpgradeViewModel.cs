using UnityEngine;

namespace CodeBase.UI.Elements.Hud.ShopPanel
{
    public class UpgradeViewModel
    {
        public Sprite MainImage { get; }
        public Sprite LevelImage { get; }
        public Sprite AdditionalImage { get; }
        public int Cost { get; }
        public string RuTitle { get; }
        public string EnTitle { get; }
        public string TrTitle { get; }

        public UpgradeViewModel(Sprite mainImage, Sprite levelImage, Sprite additionalImage, int cost, string ruTitle, string enTitle, string trTitle)
        {
            MainImage = mainImage;
            LevelImage = levelImage;
            AdditionalImage = additionalImage;
            Cost = cost;
            RuTitle = ruTitle;
            EnTitle = enTitle;
            TrTitle = trTitle;
        }
    }
}