using CodeBase.Enemy.Attacks;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class CheckAttackRange : MonoBehaviour, IOnOffable
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Follow _follow;

        private Attack _attack;
        private bool _run;

        private void Awake() =>
            _attack = GetComponent<Attack>();

        private void Start()
        {
            On();
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
            _attack.enabled = false;
        }

        public void Construct(float radius) =>
            _triggerObserver.GetComponent<SphereCollider>().radius = radius;

        private void TriggerEnter(Collider obj)
        {
            if (_follow != null && _run)
            {
                _attack.enabled = true;
                _follow.Stop();
                _follow.enabled = false;
            }
        }

        private void TriggerExit(Collider obj)
        {
            if (_follow != null && _run)
            {
                _attack.enabled = false;
                _follow.Move();
                _follow.enabled = true;
            }
        }

        public void On() =>
            _run = true;

        public void Off() =>
            _run = false;
    }
}