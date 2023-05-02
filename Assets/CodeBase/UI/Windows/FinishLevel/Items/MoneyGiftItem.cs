using CodeBase.UI.Windows.Common;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.FinishLevel.Items
{
    public class MoneyGiftItem : MoneyItemBase
    {
        [SerializeField] private Image PriceCrossing;
        [SerializeField] private Image Free;

        protected override void Clicked()
        {
            GiftsItemBalance.AddMoney(_moneyStaticData.Value);
            ClearData();
        }
    }
}