using System;

namespace CodeBase.Data
{
    [Serializable]
    public class HealthState
    {
        public float CurrentHp { get; private set; }
        public float BaseMaxHp { get; private set; }

        public HealthState()
        {
            BaseMaxHp = Constants.InitialMaxHP;
            CurrentHp = BaseMaxHp;
        }

        public void ChangeCurrentHP(float value) =>
            CurrentHp = value;

        public void ChangeMaxHP(float value) =>
            BaseMaxHp = value;
    }
}