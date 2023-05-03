using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Services;

namespace CodeBase.UI.Windows.Common
{
    public abstract class ItemItemBase : ItemBase
    {
        protected ShopItemStaticData _itemStaticData;
        private ItemTypeId _typeId;
        protected HeroHealth _health;

        private void OnEnable() =>
            Button?.onClick.AddListener(Clicked);

        private void OnDisable() =>
            Button?.onClick.RemoveListener(Clicked);

        public void Construct(ItemTypeId typeId, PlayerProgress progress, HeroHealth health)
        {
            Button?.onClick.AddListener(Clicked);
            _health = health;
            _typeId = typeId;
            base.Construct(progress);
            FillData();
        }

        protected override void FillData()
        {
            _itemStaticData = StaticDataService.ForShopItem(_typeId);

            BackgroundIcon.color = Constants.ShopItemItem;
            BackgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            MainIcon.sprite = _itemStaticData.MainImage;
            MainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            LevelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            CostText.text = $"{_itemStaticData.Cost} $";
            // CostText.color = Constants.ShopItemPerk;
            CountText.text = "";
            TitleText.text = $"{_itemStaticData.RuTitle}";
        }
    }
}