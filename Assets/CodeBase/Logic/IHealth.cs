using System;

namespace CodeBase.Logic
{
    public interface IHealth
    {
        float Current { get; }
        float Max { get; }
        event Action Died;
        event Action HealthChanged;
        void TakeDamage(float damage);
    }
}