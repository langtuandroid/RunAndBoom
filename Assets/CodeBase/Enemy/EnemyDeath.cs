using System.Collections;
using CodeBase.Logic;
using CodeBase.UI.Elements.Hud;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour, IDeath
    {
        private const float UpForce = 500f;

        private Rigidbody _rigidbody;
        private float _deathDelay = 5f;
        private IHealth _health;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _health = GetComponent<IHealth>();
            _health.Died += ForceUp;
        }

        private void ForceUp()
        {
            transform.GetComponentInChildren<Target>().Hide();
            _rigidbody.AddForce(Vector3.up * UpForce, ForceMode.Force);
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