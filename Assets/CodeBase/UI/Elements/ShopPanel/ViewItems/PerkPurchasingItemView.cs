using CodeBase.Data;
using CodeBase.Data.Perks;
using CodeBase.StaticData.Items;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class PerkPurchasingItemView : BaseItemView
    {
        [SerializeField] private Button _button;

        private PerkItemData _perkItemData;
        private PerkStaticData _perkStaticData;

        // private void OnEnable() =>
        //     _button?.onClick.AddListener(Clicked);
        //
        // private void OnDisable() =>
        //     _button?.onClick.RemoveListener(Clicked);

        public void Construct(PerkItemData perkItemData, PlayerProgress progress)
        {
            // _button?.onClick.AddListener(Clicked);
            base.Construct(progress);
            _perkItemData = perkItemData;
            FillData();
        }

        public void ChangeClickability(bool isClickable) =>
            _button.interactable = isClickable;

        protected override void FillData()
        {
            _perkStaticData = StaticDataService.ForPerk(_perkItemData.PerkTypeId, _perkItemData.LevelTypeId);

            BackgroundIcon.color = Constants.ShopItemPerk;
            BackgroundIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            MainIcon.sprite = _perkStaticData.MainImage;
            MainIcon.ChangeImageAlpha(Constants.AlphaActiveItem);
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (_perkStaticData.ILevel != null)
                LevelIcon.sprite = _perkStaticData.ILevel;

            LevelIcon.ChangeImageAlpha(_perkStaticData.ILevel != null
                ? Constants.AlphaActiveItem
                : Constants.AlphaInactiveItem);

            CostText.text = $"{_perkStaticData.Cost} $";
            CountText.text = "";
            // CostText.color = Constants.ShopItemPerk;
            TitleText.text = _perkStaticData.IRuTitle;
        }

        public void Clicked()
        {
            if (IsMoneyEnough(_perkStaticData.Cost))
            {
                ReduceMoney(_perkStaticData.Cost);
                Progress.PerksData.LevelUp(_perkStaticData.PerkTypeId);
                ClearData();
            }
        }
    }
}