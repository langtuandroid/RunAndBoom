using CodeBase.Data.Progress;
using CodeBase.Data.Progress.Perks;
using CodeBase.StaticData.Items;
using CodeBase.UI.Services;

namespace CodeBase.UI.Windows.Common
{
    public abstract class PerkItemBase : ItemBase
    {
        private PerkItemData _perkItemData;
        protected PerkStaticData _perkStaticData;

        private void OnEnable() =>
            _button?.onClick.AddListener(Clicked);

        private void OnDisable() =>
            _button?.onClick.RemoveListener(Clicked);

        public void Construct(PerkItemData perkItemData, ProgressData progressData)
        {
            base.Construct(progressData);
            _perkItemData = perkItemData;
            FillData();
        }

        protected override void FillData()
        {
            _perkStaticData = _staticDataService.ForPerk(_perkItemData.PerkTypeId, _perkItemData.LevelTypeId);

            _backgroundIcon.color = Constants.ShopItemPerk;
            _backgroundIcon.ChangeImageAlpha(Constants.Visible);
            _mainIcon.sprite = _perkStaticData.MainImage;
            _mainIcon.ChangeImageAlpha(Constants.Visible);
            _additionalIcon.ChangeImageAlpha(Constants.Invisible);

            if (_perkStaticData.ILevel != null)
                _levelIcon.sprite = _perkStaticData.ILevel;

            _levelIcon.ChangeImageAlpha(_perkStaticData.ILevel != null
                ? Constants.Visible
                : Constants.Invisible);

            if (_costText != null)
                _costText.text = $"{_perkStaticData.Cost}";

            _countText.text = "";
            // CostText.color = Constants.ShopItemPerk;
            _titleText.text =
                $"{_localizationService.GetText(russian: _perkStaticData.RuTitle, turkish: _perkStaticData.TrTitle, english: _perkStaticData.EnTitle)}";
        }
    }
}