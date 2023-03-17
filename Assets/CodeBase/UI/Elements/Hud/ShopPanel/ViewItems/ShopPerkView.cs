using System;
using CodeBase.Data.Perks;
using CodeBase.StaticData.Items;

namespace CodeBase.UI.Elements.Hud.ShopPanel.ViewItems
{
    public class ShopPerkView : BaseShopView, IShopItem
    {
        private PerkItemData _perkItemData;
        private PerkStaticData _perkStaticData;

        public event Action ShopItemClicked;

        public void Construct(PerkItemData perkItemData)
        {
            base.Construct();
            _perkItemData = perkItemData;
            FillData();
        }

        protected override void FillData()
        {
            _perkStaticData = StaticDataService.ForPerk(_perkItemData.PerkTypeId, _perkItemData.LevelTypeId);
            MainIcon.sprite = _perkStaticData.MainImage;
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