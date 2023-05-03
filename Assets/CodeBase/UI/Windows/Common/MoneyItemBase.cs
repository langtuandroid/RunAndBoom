using CodeBase.Data;
using CodeBase.StaticData.Items.Gifts;
using CodeBase.UI.Services;

namespace CodeBase.UI.Windows.Common
{
    public abstract class MoneyItemBase : ItemBase
    {
        private MoneyTypeId _moneyTypeId;
        protected MoneyStaticData _moneyStaticData;

        private void OnEnable() =>
            Button?.onClick.AddListener(Clicked);

        private void OnDisable() =>
            Button?.onClick.RemoveListener(Clicked);

        public void Construct(MoneyTypeId moneyTypeId, PlayerProgress progress)
        {
            Button?.onClick.AddListener(Clicked);
            base.Construct(progress);
            _moneyTypeId = moneyTypeId;
            FillData();
        }

        protected override void FillData()
        {
            _moneyStaticData = StaticDataService.ForMoney(_moneyTypeId);

            BackgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            BackgroundIcon.color = Constants.ShopItemAmmo;
            MainIcon.sprite = _moneyStaticData.MainImage;
            MainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            LevelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            CostText.text = "";
            // CostText.color = Constants.ShopItemPerk;
            CountText.text = "";
            // CountText.color = Constants.ShopItemCountField;
            TitleText.text = $"+{_moneyStaticData.Value} $";
        }
    }
}