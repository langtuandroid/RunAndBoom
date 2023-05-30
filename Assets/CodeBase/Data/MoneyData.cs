using System;

namespace CodeBase.Data
{
    [Serializable]
    public class MoneyData
    {
        public int AvailableMoney;
        public int AllMoney;

        public event Action MoneyChanged;

        public MoneyData()
        {
            AvailableMoney = 0;
            AllMoney = 0;
        }

        public void AddMoney(int value)
        {
            AvailableMoney += value;
            AllMoney += value;
            MoneyChanged?.Invoke();
        }

        public bool IsMoneyEnough(int value) =>
            value <= AvailableMoney;

        public void ReduceMoney(int value)
        {
            AvailableMoney -= value;
            MoneyChanged?.Invoke();
        }
    }
}