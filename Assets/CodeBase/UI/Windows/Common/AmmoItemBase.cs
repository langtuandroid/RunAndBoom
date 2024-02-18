using CodeBase.Data;
using CodeBase.Data.Progress;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.UI.Services;

namespace CodeBase.UI.Windows.Common
{
    public abstract class AmmoItemBase : ItemBase
    {
        private AmmoCountType _countType;
        protected AmmoItem _ammoItem;
        protected ShopAmmoStaticData _shopAmmoStaticData;

        private void OnEnable() =>
            _button?.onClick.AddListener(Clicked);

        private void OnDisable() =>
            _button?.onClick.RemoveListener(Clicked);

        protected void Construct(AmmoItem ammoItem, ProgressData progressData)
        {
            base.Construct(progressData);
            _ammoItem = ammoItem;
            FillData();
        }

        protected override void FillData()
        {
            _shopAmmoStaticData = _staticDataService.ForShopAmmo(_ammoItem.WeaponTypeId, _ammoItem.CountType);

            _backgroundIcon.ChangeImageAlpha(Constants.Visible);
            _backgroundIcon.color = Constants.ShopItemAmmo;
            _mainIcon.sprite = _shopAmmoStaticData.MainImage;
            _mainIcon.ChangeImageAlpha(Constants.Visible);
            _levelIcon.ChangeImageAlpha(Constants.Invisible);
            _additionalIcon.ChangeImageAlpha(Constants.Invisible);

            if (_costText != null)
                _costText.text = $"{_shopAmmoStaticData.Cost}";

            int count = _inputService.GetCount(_shopAmmoStaticData.Count);

            _countText.text = $"{count}";
            _titleText.text =
                _localizationService.GetText(russian: $"{count} {_shopAmmoStaticData.RuTitle}",
                    turkish: $"{count} {_shopAmmoStaticData.TrTitle}",
                    english: $"{count} {_shopAmmoStaticData.EnTitle}");
        }
    }
}