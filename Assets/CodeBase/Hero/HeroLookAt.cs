using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroLookAt : MonoBehaviour
    {
        [SerializeField] private HeroRotating _heroRotating;
        [SerializeField] private EnemiesChecker _enemiesChecker;

        private GameObject _target;
        private bool _lookAtForward;
        private Coroutine _lookAtCoroutine;

        private void Awake()
        {
            _heroRotating.EndedRotatingToEnemy += LookAt;
            _heroRotating.EndedRotatingToForward += LookToForward;
            _heroRotating.StartedRotating += NotLookAtTarget;
            _enemiesChecker.EnemyNotFound += NotLookAtTarget;
        }

        private void Update()
        {
            if (_target)
                transform.LookAt(_target.transform);

            if (_lookAtForward)
                transform.LookAt(Vector3.forward);
        }

        private void LookAt(GameObject target)
        {
            Debug.Log("LookAt");
            _lookAtForward = false;
            _target = target;
        }

        private void LookToForward()
        {
            Debug.Log("LookToForward");
            _lookAtForward = true;
        }

        private void NotLookAtTarget() =>
            _target = null;
    }
}