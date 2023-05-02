namespace CodeBase.UI.Windows.Common
{
    public abstract class GiftItemBase : ItemBase
    {
        protected void AddMoney(int value) =>
            Progress.CurrentLevelStats.MoneyData.AddMoney(value);
    }
}