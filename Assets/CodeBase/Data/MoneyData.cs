using System;
using Tayx.Graphy.Utils.NumString;

namespace CodeBase.Data
{
    [Serializable]
    public class MoneyData
    {
        public int Money;

        public event Action MoneyChanged;

        public MoneyData()
        {
            Money = 0;
        }

        public void AddMoney(int value)
        {
            Money += value;
            MoneyChanged?.Invoke();
        }

        public bool IsMoneyEnough(int value) =>
            value <= Money;

        public void ReduceMoney(int value)
        {
            Money -= value;
            MoneyChanged?.Invoke();
        }

        public void Clear() =>
            Money = Constants.Zero.ToInt();
    }
}