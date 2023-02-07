using System;
using CodeBase.Data;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth, IProgressSaver
    {
        public int Current { get; set; }
        public int Max { get; set; }
        public event Action Died;
        public event Action HealthChanged;

        public void Construct()
        {
            HealthChanged?.Invoke();
        }

        public void TakeDamage(int damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Max = progress.MaxHP;
            Current = progress.MaxHP;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }
    }
}