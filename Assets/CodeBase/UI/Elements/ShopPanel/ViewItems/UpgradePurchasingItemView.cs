using System;
using CodeBase.Data.Upgrades;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.UI.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class UpgradePurchasingItemView : MonoBehaviour
    {
        [FormerlySerializedAs("BackgroundIcon")] [SerializeField]
        private Image _backgroundIcon;

        [FormerlySerializedAs("MainIcon")] [SerializeField]
        private Image _mainIcon;

        [FormerlySerializedAs("LevelIcon")] [SerializeField]
        private Image _levelIcon;

        [FormerlySerializedAs("AdditionalIcon")] [SerializeField]
        private Image _additionalIcon;

        [FormerlySerializedAs("CostText")] [SerializeField]
        private TextMeshProUGUI _costText;

        [FormerlySerializedAs("CountText")] [SerializeField]
        private TextMeshProUGUI _countText;

        [FormerlySerializedAs("TitleText")] [SerializeField]
        private TextMeshProUGUI _titleText;

        [SerializeField] private Button _button;

        private IStaticDataService StaticDataService;
        private IPlayerProgressService PlayerProgressService;
        private UpgradeItemData _upgradeItemData;
        private ShopUpgradeStaticData _upgradeStaticData;
        private UpgradableWeaponStaticData _upgradableWeaponStaticData;
        private UpgradeLevelInfoStaticData _upgradeLevelInfoStaticData;
        private ShopUpgradeLevelStaticData _shopUpgradeLevelStaticData;

        public event Action ShopItemClicked;

        private void OnEnable()
        {
            _button?.onClick.AddListener(Clicked);
        }

        private void OnDisable()
        {
            _button?.onClick.RemoveListener(Clicked);
        }

        public void Construct(UpgradeItemData upgradeItemData, IPlayerProgressService playerProgressService)
        {
            _button?.onClick.AddListener(Clicked);
            PlayerProgressService = playerProgressService;
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
            _upgradeItemData = upgradeItemData;
            FillData();
        }

        public void ClearData()
        {
            if (_backgroundIcon != null)
                _backgroundIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (_mainIcon != null)
                _mainIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (_levelIcon != null)
                _levelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (_additionalIcon != null)
                _additionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (_costText != null)
                _costText.text = "";

            if (_countText != null)
                _countText.text = "";

            if (_titleText != null)
                _titleText.text = "";
        }

        private bool IsMoneyEnough(int value)
        {
            return PlayerProgressService.Progress.CurrentLevelStats.MoneyData.IsMoneyEnough(value);
        }

        private void ReduceMoney(int value)
        {
            PlayerProgressService.Progress.CurrentLevelStats.MoneyData.ReduceMoney(value);
        }

        public void ChangeClickability(bool isClickable)
        {
            _button.interactable = isClickable;
        }

        private void FillData()
        {
            _backgroundIcon.color = Constants.ShopItemUpgrade;
            _backgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            _upgradableWeaponStaticData = StaticDataService.ForUpgradableWeapon(_upgradeItemData.WeaponTypeId);
            _upgradeStaticData = StaticDataService.ForShopUpgrade(_upgradeItemData.UpgradeTypeId);
            _upgradeLevelInfoStaticData = StaticDataService.ForUpgradeLevelsInfo(_upgradeItemData.UpgradeTypeId, _upgradeItemData.LevelTypeId);
            _shopUpgradeLevelStaticData = StaticDataService.ForShopUpgradeLevel(_upgradeItemData.LevelTypeId);

            _mainIcon.sprite = _upgradableWeaponStaticData.MainImage;
            _mainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);

            if (_shopUpgradeLevelStaticData.MainImage != null)
                _levelIcon.sprite = _shopUpgradeLevelStaticData.MainImage;

            _additionalIcon.sprite = _upgradeStaticData.MainImage;
            _additionalIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            _costText.text = $"{_upgradeLevelInfoStaticData.Cost} $";
            // CostText.color = Constants.ShopItemPerk;
            _countText.text = "";
            _titleText.text = $"{_upgradeStaticData.IRuTitle} {_shopUpgradeLevelStaticData.Level} {_upgradableWeaponStaticData.IRuTitle}";
        }

        private void Clicked()
        {
            if (IsMoneyEnough(_upgradeLevelInfoStaticData.Cost))
            {
                ReduceMoney(_upgradeLevelInfoStaticData.Cost);
                PlayerProgressService.Progress.WeaponsData.WeaponUpgradesData.LevelUp(_upgradableWeaponStaticData.WeaponTypeId,
                    _upgradeStaticData.UpgradeTypeId);
                ShopItemClicked?.Invoke();
            }

            ClearData();
        }
    }
}