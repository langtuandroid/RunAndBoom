using CodeBase.Data;
using CodeBase.Data.Perks;
using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class PerkGiftItem : PerkItemBase
    {
        private GiftsGenerator _generator;

        public void Construct(PerkItemData perkItemData, PlayerProgress progress, GiftsGenerator generator)
        {
            _generator = generator;
            base.Construct(perkItemData, progress);
        }

        protected override void Clicked()
        {
            Progress.PerksData.LevelUp(_perkStaticData.PerkTypeId);
            ClearData();
            _generator.Clicked();
        }
    }
}