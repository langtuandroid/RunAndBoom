using System;
using CodeBase.Data.Perks;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Items;
using UnityEngine.UI;

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
            GetComponent<Image>().color = Constants.ShopItemPerk;
            _perkStaticData = StaticDataService.ForPerk(_perkItemData.PerkTypeId, _perkItemData.LevelTypeId);
            MainIcon.sprite = _perkStaticData.MainImage;

            if (_perkStaticData.LevelImage != null)
                LevelIcon.sprite = _perkStaticData.LevelImage;

            CostText.text = $"{_perkStaticData.Cost} $";
            CostText.color = Constants.ShopItemPerk;
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
        }
    }
}