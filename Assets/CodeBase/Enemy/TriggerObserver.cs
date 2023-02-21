using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Hero"))
                TriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Hero"))
                TriggerExit?.Invoke(other);
        }
    }
}