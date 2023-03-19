using System;

namespace CodeBase.Data
{
    [Serializable]
    public class HealthState
    {
        public float CurrentHp { get; private set; }
        public float MaxHp { get; private set; }

        public HealthState()
        {
            MaxHp = Constants.InitialMaxHP;
            CurrentHp = MaxHp;
        }

        public void ChangeCurrentHP(float value) =>
            CurrentHp = value;

        public void ChangeMaxHP(float value) =>
            MaxHp = value;
    }
}