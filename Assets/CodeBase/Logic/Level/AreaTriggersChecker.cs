using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic.Level
{
    public class AreaTriggersChecker : MonoBehaviour
    {
        [SerializeField] private List<EnemyChecker> _enemyCheckers;

        private const float OffEnemyCheckersTime = 0.2f;

        private bool _checking;
        private bool _offChechers;
        private bool _open;
        private bool _tempOpen;
        private WaitForSeconds _offEnemyCheckersDelay;
        private Coroutine _offEnemyCheckersCoroutine;
        private float _currentTime;

        public Action Cleared;

        private void Awake()
        {
            _offEnemyCheckersDelay = new WaitForSeconds(OffEnemyCheckersTime);

            foreach (EnemyChecker enemyChecker in _enemyCheckers)
            {
                enemyChecker.Construct(this);
                enemyChecker.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (_checking && _offChechers)
            {
                if (_open)
                {
                    OffEnemyCheckers();
                    Cleared?.Invoke();
                }
                else
                {
                    OffEnemyCheckers();
                }
            }
        }

        public void CheckTriggersForEnemies()
        {
            _offChechers = false;
            _checking = true;

            foreach (EnemyChecker enemyChecker in _enemyCheckers)
                enemyChecker.gameObject.SetActive(true);

            if (_offEnemyCheckersCoroutine != null)
                StopCoroutine(_offEnemyCheckersCoroutine);

            _offEnemyCheckersCoroutine = StartCoroutine(CoroutineOffEnemyCheckers());
        }

        private IEnumerator CoroutineOffEnemyCheckers()
        {
            yield return _offEnemyCheckersDelay;
            _offChechers = true;
        }

        public void NotOpen() =>
            _open = false;

        public void Open() =>
            _open = true;

        private void OffEnemyCheckers()
        {
            _offChechers = false;
            _checking = false;

            foreach (EnemyChecker enemyChecker in _enemyCheckers)
                enemyChecker.gameObject.SetActive(false);
        }
    }
}