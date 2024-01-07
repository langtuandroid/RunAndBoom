using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHitShower : MonoBehaviour
    {
        [SerializeField] private GameObject _bloodVfx;

        private void Awake() => 
            _bloodVfx.SetActive(false);

        public void Show() => 
            _bloodVfx.SetActive(true);
    }
}