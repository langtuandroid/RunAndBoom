using CodeBase.Enemy.Attacks;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Follow _follow;

        private Attack _attack;

        private void Awake() => _attack = GetComponent<Attack>();

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            _attack.DisableAttack();
        }

        public void Construct(float radius) =>
            _triggerObserver.GetComponent<SphereCollider>().radius = radius;

        private void TriggerEnter(Collider obj)
        {
            _attack.EnableAttack();
            _follow.Stop();
            _follow.enabled = false;
        }

        private void TriggerExit(Collider obj)
        {
            _attack.DisableAttack();
            _follow.Run();
            _follow.enabled = true;
        }
    }
}