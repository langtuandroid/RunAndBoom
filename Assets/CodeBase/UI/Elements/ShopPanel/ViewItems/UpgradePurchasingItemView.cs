using System;
using CodeBase.Data.Upgrades;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using CodeBase.UI.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class UpgradePurchasingItemView : MonoBehaviour
    {
        [SerializeField] private Image BackgroundIcon;
        [SerializeField] private Image MainIcon;
        [SerializeField] private Image LevelIcon;
        [SerializeField] private Image AdditionalIcon;
        [SerializeField] private TextMeshProUGUI CostText;
        [SerializeField] private TextMeshProUGUI CountText;
        [SerializeField] private TextMeshProUGUI TitleText;
        [SerializeField] private Button _button;

        private IStaticDataService StaticDataService;
        private IPlayerProgressService PlayerProgressService;
        private UpgradeItemData _upgradeItemData;
        private ShopUpgradeStaticData _upgradeStaticData;
        private UpgradableWeaponStaticData _upgradableWeaponStaticData;
        private UpgradeLevelInfoStaticData _upgradeLevelInfoStaticData;
        private ShopUpgradeLevelStaticData _shopUpgradeLevelStaticData;

        public event Action ShopItemClicked;

        public void Construct(UpgradeItemData upgradeItemData, IPlayerProgressService playerProgressService)
        {
            PlayerProgressService = playerProgressService;
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
            _button.onClick.AddListener(Clicked);
            _upgradeItemData = upgradeItemData;
            FillData();
        }

        public void ClearData()
        {
            if (BackgroundIcon != null)
                BackgroundIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (MainIcon != null)
                MainIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (LevelIcon != null)
                LevelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (AdditionalIcon != null)
                AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (CostText != null)
                CostText.text = "";

            if (CountText != null)
                CountText.text = "";

            if (TitleText != null)
                TitleText.text = "";
        }

        private bool IsMoneyEnough(int value) =>
            PlayerProgressService.Progress.CurrentLevelStats.MoneyData.IsMoneyEnough(value);

        private void ReduceMoney(int value) =>
            PlayerProgressService.Progress.CurrentLevelStats.MoneyData.ReduceMoney(value);

        public void ChangeClickability(bool isClickable) =>
            _button.interactable = isClickable;

        private void FillData()
        {
            BackgroundIcon.color = Constants.ShopItemUpgrade;
            BackgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            _upgradableWeaponStaticData = StaticDataService.ForUpgradableWeapon(_upgradeItemData.WeaponTypeId);
            _upgradeStaticData = StaticDataService.ForShopUpgrade(_upgradeItemData.UpgradeTypeId);
            _upgradeLevelInfoStaticData = StaticDataService.ForUpgradeLevelsInfo(_upgradeItemData.UpgradeTypeId, _upgradeItemData.LevelTypeId);
            _shopUpgradeLevelStaticData = StaticDataService.ForShopUpgradeLevel(_upgradeItemData.LevelTypeId);

            MainIcon.sprite = _upgradableWeaponStaticData.MainImage;
            MainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);

            if (_shopUpgradeLevelStaticData.MainImage != null)
                LevelIcon.sprite = _shopUpgradeLevelStaticData.MainImage;

            AdditionalIcon.sprite = _upgradeStaticData.MainImage;
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            CostText.text = $"{_upgradeLevelInfoStaticData.Cost} $";
            // CostText.color = Constants.ShopItemPerk;
            CountText.text = "";
            TitleText.text = $"{_upgradeStaticData.IRuTitle} {_shopUpgradeLevelStaticData.Level} {_upgradableWeaponStaticData.IRuTitle}";
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