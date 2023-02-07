using System;

namespace CodeBase.Logic
{
    public interface IHealth
    {
        int Current { get; set; }
        int Max { get; set; }
        event Action Died;
        event Action HealthChanged;
        void TakeDamage(int damage);
    }
}