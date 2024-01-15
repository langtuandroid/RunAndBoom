using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Logic.Level
{
    public class EnemyChecker : MonoBehaviour
    {
        private const float Delay = 0.2f;

        private AreaTriggersChecker _areaTriggersChecker;
        private int _count;
        private float _currentTime;

        private void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= Delay)
            {
                _currentTime = 0f;

                if (_count > 0)
                {
                    _areaTriggersChecker.NotOpen();
                    _count = 0;
                }
                else
                {
                    _areaTriggersChecker.Open();
                }
            }
        }

        public void Construct(AreaTriggersChecker areaTriggersChecker) =>
            _areaTriggersChecker = areaTriggersChecker;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.EnemyTag))
            {
                EnemyHealth enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth>();

                if (enemyHealth.Current > 0)
                    _count++;
                else
                    _count = 0;
            }
        }
    }
}