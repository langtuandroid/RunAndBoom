using System;

namespace CodeBase.Data
{
    [Serializable]
    public class State
    {
        public int CurrentHP { get; private set; }
        public int MaxHP { get; private set; }

        public void ResetHP() => CurrentHP = MaxHP;

        public State(int maxHP)
        {
            MaxHP = maxHP;
            ResetHP();
        }
    }
}