using System;

namespace CodeBase.Data.Progress
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