﻿using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;

        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHpBar;
        }

        private void UpdateHpBar() =>
            _hpBar.SetValue(_health.Current, _health.Max);
    }
}