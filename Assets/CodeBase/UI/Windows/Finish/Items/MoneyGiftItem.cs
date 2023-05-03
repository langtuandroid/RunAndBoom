using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class MoneyGiftItem : MoneyItemBase
    {
        protected override void Clicked()
        {
            GiftsItemBalance.AddMoney(_moneyStaticData.Value);
            ClearData();
        }
    }
}