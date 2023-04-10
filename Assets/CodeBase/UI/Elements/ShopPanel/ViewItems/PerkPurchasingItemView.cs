using System;
using CodeBase.Data.Perks;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Items;
using CodeBase.UI.Services;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
{
    public class PerkPurchasingItemView : BasePurchasingItemView
    {
        private PerkItemData _perkItemData;
        private PerkStaticData _perkStaticData;

        public override event Action ShopItemClicked;

        public void Construct(PerkItemData perkItemData, IPlayerProgressService playerProgressService)
        {
            base.Construct(playerProgressService);
            _perkItemData = perkItemData;
            FillData();
        }

        protected override void FillData()
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

        protected override void Clicked()
        {
            if (IsMoneyEnough(_perkStaticData.Cost))
            {
                ReduceMoney(_perkStaticData.Cost);
                Progress.PerksData.LevelUp(_perkStaticData.PerkTypeId);
                ShopItemClicked?.Invoke();
            }

            ClearData();
        }
    }
}