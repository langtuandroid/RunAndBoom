using CodeBase.Data;
using CodeBase.StaticData.Items.Gifts;
using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class MoneyGiftItem : MoneyItemBase
    {
        private GiftsGenerator _generator;

        public void Construct(MoneyTypeId moneyTypeId, PlayerProgress progress, GiftsGenerator generator)
        {
            _generator = generator;
            base.Construct(moneyTypeId, progress);
        }

        protected override void Clicked()
        {
            GiftsItemBalance.AddMoney(_moneyStaticData.Value);
            ClearData();
            _generator.Clicked();
        }
    }
}