using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroDeath : MonoBehaviour, IDeath
    {
        private IHealth _health;

        private void Awake()
        {
            _health = GetComponent<IHealth>();
            _health.HealthChanged += HealthChanged;
        }

        private void HealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        public void Die()
        {
            //TODO fill it
        }
    }
}