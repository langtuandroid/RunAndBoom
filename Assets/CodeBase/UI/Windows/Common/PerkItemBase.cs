using CodeBase.Data;
using CodeBase.Data.Perks;
using CodeBase.StaticData.Items;
using CodeBase.UI.Services;

namespace CodeBase.UI.Windows.Common
{
    public abstract class PerkItemBase : ItemBase
    {
        private PerkItemData _perkItemData;
        protected PerkStaticData _perkStaticData;

        private void OnEnable() =>
            Button?.onClick.AddListener(Clicked);

        private void OnDisable() =>
            Button?.onClick.RemoveListener(Clicked);

        public void Construct(PerkItemData perkItemData, PlayerProgress progress)
        {
            base.Construct(progress);
            _perkItemData = perkItemData;
            FillData();
        }

        protected override void FillData()
        {
            _perkStaticData = StaticDataService.ForPerk(_perkItemData.PerkTypeId, _perkItemData.LevelTypeId);

            BackgroundIcon.color = Constants.ShopItemPerk;
            BackgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            MainIcon.sprite = _perkStaticData.MainImage;
            MainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (_perkStaticData.ILevel != null)
                LevelIcon.sprite = _perkStaticData.ILevel;

            LevelIcon.ChangeImageAlpha(_perkStaticData.ILevel != null
                ? Constants.AlphaActiveItem
                : Constants.AlphaInactiveItem);

            if (CostText != null)
                CostText.text = $"{_perkStaticData.Cost} $";

            CountText.text = "";
            // CostText.color = Constants.ShopItemPerk;
            TitleText.text = $"{_perkStaticData.RuTitle} {_perkStaticData.Level}";
        }
    }
}