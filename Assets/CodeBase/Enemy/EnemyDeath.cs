using System.Collections;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour, IDeath
    {
        private float _deathDelay = 5f;

        public void Die()
        {
            GetComponent<IHealth>().TakeDamage(100);
            GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Force);
            StartCoroutine(DestroyTimer());
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(_deathDelay);
            Destroy(gameObject);
        }
    }
}