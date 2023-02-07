using System.Collections;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour, IDeath
    {
        private float _deathDelay = 5f;
        private IHealth _health;

        private void Awake()
        {
            _health = GetComponent<IHealth>();
            _health.Died += ForceUp;
        }

        private void ForceUp()
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 50f, ForceMode.Force);
            StartCoroutine(DestroyTimer());
        }

        public void Die() => 
            _health.TakeDamage(100);

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(_deathDelay);
            Destroy(gameObject);
        }
    }
}