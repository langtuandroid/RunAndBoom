using CodeBase.Data.Progress;
using CodeBase.StaticData.Items.Gifts;
using CodeBase.UI.Services;

namespace CodeBase.UI.Windows.Common
{
    public abstract class MoneyItemBase : ItemBase
    {
        private MoneyTypeId _moneyTypeId;
        protected MoneyStaticData _moneyStaticData;

        private void OnEnable() =>
            _button?.onClick.AddListener(Clicked);

        private void OnDisable() =>
            _button?.onClick.RemoveListener(Clicked);

        protected void Construct(MoneyTypeId moneyTypeId, ProgressData progressData)
        {
            base.Construct(progressData);
            _moneyTypeId = moneyTypeId;
            FillData();
        }

        protected override void FillData()
        {
            _moneyStaticData = _staticDataService.ForMoney(_moneyTypeId);

            _backgroundIcon.ChangeImageAlpha(Constants.Visible);
            _backgroundIcon.color = Constants.ShopItemAmmo;
            _mainIcon.sprite = _moneyStaticData.MainImage;
            _mainIcon.ChangeImageAlpha(Constants.Visible);
            _levelIcon.ChangeImageAlpha(Constants.Invisible);
            _additionalIcon.ChangeImageAlpha(Constants.Invisible);

            if (_costText != null)
                _costText.text = "";

            // CostText.color = Constants.ShopItemPerk;
            _countText.text = "";
            // CountText.color = Constants.ShopItemCountField;
            _titleText.text = $"+{_moneyStaticData.Value}";
        }
    }
}