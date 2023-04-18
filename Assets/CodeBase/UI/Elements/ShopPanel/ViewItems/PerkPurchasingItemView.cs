using System;
using CodeBase.Data.Perks;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using CodeBase.UI.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class PerkPurchasingItemView : MonoBehaviour
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
        private PerkItemData _perkItemData;
        private PerkStaticData _perkStaticData;

        public event Action ShopItemClicked;

        private void OnEnable() =>
            _button?.onClick.AddListener(Clicked);

        private void OnDisable() =>
            _button?.onClick.RemoveListener(Clicked);

        public void Construct(PerkItemData perkItemData, IPlayerProgressService playerProgressService)
        {
            _button?.onClick.AddListener(Clicked);
            PlayerProgressService = playerProgressService;
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
            _perkItemData = perkItemData;
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

        private bool IsMoneyEnough(int value) =>
            PlayerProgressService.Progress.CurrentLevelStats.MoneyData.IsMoneyEnough(value);

        private void ReduceMoney(int value) =>
            PlayerProgressService.Progress.CurrentLevelStats.MoneyData.ReduceMoney(value);

        public void ChangeClickability(bool isClickable) =>
            _button.interactable = isClickable;

        private void FillData()
        {
            _backgroundIcon.color = Constants.ShopItemPerk;
            _backgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            _perkStaticData = StaticDataService.ForPerk(_perkItemData.PerkTypeId, _perkItemData.LevelTypeId);
            _mainIcon.sprite = _perkStaticData.MainImage;
            _mainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);

            if (_perkStaticData.LevelImage != null)
                _levelIcon.sprite = _perkStaticData.LevelImage;

            _costText.text = $"{_perkStaticData.Cost} $";
            // CostText.color = Constants.ShopItemPerk;
            _titleText.text = _perkStaticData.IRuTitle;
        }

        private void Clicked()
        {
            if (IsMoneyEnough(_perkStaticData.Cost))
            {
                ReduceMoney(_perkStaticData.Cost);
                PlayerProgressService.Progress.PerksData.LevelUp(_perkStaticData.PerkTypeId);
                ShopItemClicked?.Invoke();
            }

            ClearData();
        }
    }
}