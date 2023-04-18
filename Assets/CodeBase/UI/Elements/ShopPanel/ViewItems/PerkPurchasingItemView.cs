using System;
using CodeBase.Data.Perks;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using CodeBase.UI.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class PerkPurchasingItemView : MonoBehaviour
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
        private PerkItemData _perkItemData;
        private PerkStaticData _perkStaticData;

        public event Action ShopItemClicked;

        public void Construct(PerkItemData perkItemData, IPlayerProgressService playerProgressService)
        {
            PlayerProgressService = playerProgressService;
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
            _button.onClick.AddListener(Clicked);
            _perkItemData = perkItemData;
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
            BackgroundIcon.color = Constants.ShopItemPerk;
            BackgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            _perkStaticData = StaticDataService.ForPerk(_perkItemData.PerkTypeId, _perkItemData.LevelTypeId);
            MainIcon.sprite = _perkStaticData.MainImage;
            MainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);

            if (_perkStaticData.LevelImage != null)
                LevelIcon.sprite = _perkStaticData.LevelImage;

            CostText.text = $"{_perkStaticData.Cost} $";
            // CostText.color = Constants.ShopItemPerk;
            TitleText.text = _perkStaticData.IRuTitle;
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