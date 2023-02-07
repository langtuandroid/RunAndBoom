using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;

        private IHealth _health;

        private void Awake()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
        }

        public void Construct(IHealth health)
        {
            _health = health;

            _health.Died += UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            _hpBar.SetValue(_health.Current, _health.Max);

            if (_health.Current < 0)
                _health.Died -= UpdateHpBar;
        }
    }
}