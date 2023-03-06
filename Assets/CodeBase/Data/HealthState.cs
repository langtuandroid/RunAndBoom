using System;

namespace CodeBase.Data
{
    [Serializable]
    public class HealthState
    {
        public float CurrentHp { get; private set; }
        public float MaxHp { get; private set; }

        public event Action CurrentHpChanged;
        public event Action MaxHpChanged;

        public void ResetHP() => CurrentHp = MaxHp;

        public HealthState()
        {
            MaxHp = Constants.InitialMaxHP;
            ResetHP();
        }

        public void ChangeCurrentHP(float value)
        {
            CurrentHp = value;
            CurrentHpChanged?.Invoke();
        }

        public void ChangeMaxHP(float value)
        {
            MaxHp = value;
            MaxHpChanged?.Invoke();
        }
    }
}