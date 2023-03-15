using CodeBase.Data.Perks;
using CodeBase.StaticData.Items;

namespace CodeBase.UI.Elements.Hud.Perks
{
    public class PerkView : LevelingItemView
    {
        private PerkItemData _perkItemData;
        private PerkStaticData _perkStaticData;

        public void Construct(PerkItemData perkItemData)
        {
            base.Construct(perkItemData);
            _perkItemData = perkItemData;
            ItemData.LevelChanged += ChangeLevel;
            ChangeLevel();
        }

        private new void ChangeLevel()
        {
            _perkStaticData = StaticDataService.ForPerk(_perkItemData.PerkTypeId, _perkItemData.LevelTypeId);
            LevelingStaticData = _perkStaticData;

            MainType.sprite = _perkStaticData.MainImage;
            LevelType.sprite = _perkStaticData.ILevel;

            base.ChangeLevel();
        }
    }
}