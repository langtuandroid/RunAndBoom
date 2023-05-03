using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class PerkGiftItem : PerkItemBase
    {
        protected override void Clicked()
        {
            Progress.PerksData.LevelUp(_perkStaticData.PerkTypeId);
            ClearData();
        }
    }
}