using System;

namespace CodeBase.Data
{
    [Serializable]
    public class HealthState
    {
        public int CurrentHP { get; private set; }
        public int MaxHP { get; private set; }

        public void ResetHP() => CurrentHP = MaxHP;

        public HealthState()
        {
            MaxHP = Constants.InitialMaxHP;
            ResetHP();
        }
    }
}