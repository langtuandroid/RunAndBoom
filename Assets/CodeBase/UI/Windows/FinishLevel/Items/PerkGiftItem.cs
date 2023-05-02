using CodeBase.UI.Windows.Common;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.FinishLevel.Items
{
    public class PerkGiftItem : PerkItemBase
    {
        [SerializeField] private Image PriceCrossing;
        [SerializeField] private Image Free;

        protected override void Clicked()
        {
            Progress.PerksData.LevelUp(_perkStaticData.PerkTypeId);
            ClearData();
        }
    }
}