using System;
using CodeBase.Data;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth, IHeal, IProgressSaver
    {
        public float Current { get; private set; }
        public float Max { get; private set; }

        public event Action HealthChanged;

        public void Construct() =>
            HealthChanged?.Invoke();

        public void TakeDamage(float damage)
        {
            Current -= damage;
            HealthChanged?.Invoke();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Max = progress.HealthState.MaxHp;
            Current = progress.HealthState.CurrentHp;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HealthState.ChangeCurrentHP(Current);
            progress.HealthState.ChangeMaxHP(Max);
        }

        public void UpMaxHp(float value)
        {
            Max = value;
            HealthChanged?.Invoke();
        }

        public void Heal()
        {
            Current = Max;
            HealthChanged?.Invoke();
        }
    }
}