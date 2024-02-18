using CodeBase.Data.Progress;
using CodeBase.Hero;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Services;

namespace CodeBase.UI.Windows.Common
{
    public abstract class ItemItemBase : ItemBase
    {
        protected ShopItemStaticData _itemStaticData;
        private ItemTypeId _typeId;
        protected HeroHealth Health;

        private void OnEnable() =>
            _button?.onClick.AddListener(Clicked);

        private void OnDisable() =>
            _button?.onClick.RemoveListener(Clicked);

        protected void Construct(ItemTypeId typeId, HeroHealth health, ProgressData progressData)
        {
            Health = health;
            _typeId = typeId;
            base.Construct(progressData);
            FillData();
        }

        protected override void FillData()
        {
            _itemStaticData = _staticDataService.ForShopItem(_typeId);

            _backgroundIcon.color = Constants.ShopItemItem;
            _backgroundIcon.ChangeImageAlpha(Constants.Visible);
            _mainIcon.sprite = _itemStaticData.MainImage;
            _mainIcon.ChangeImageAlpha(Constants.Visible);
            _levelIcon.ChangeImageAlpha(Constants.Invisible);
            _additionalIcon.ChangeImageAlpha(Constants.Invisible);

            if (_costText != null)
                _costText.text = $"{_itemStaticData.Cost}";

            // CostText.color = Constants.ShopItemPerk;
            _countText.text = "";
            _titleText.text =
                $"{_localizationService.GetText(russian: _itemStaticData.RuTitle, turkish: _itemStaticData.TrTitle, english: _itemStaticData.EnTitle)}";
        }
    }
}