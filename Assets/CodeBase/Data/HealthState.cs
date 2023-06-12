using System;

namespace CodeBase.Data
{
    [Serializable]
    public class HealthState
    {
        public float CurrentHp;
        public float MaxHp;

        public HealthState()
        {
            MaxHp = Constants.InitialMaxHp;
            CurrentHp = MaxHp;
        }
    }
}