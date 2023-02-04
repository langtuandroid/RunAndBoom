using System.Collections;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.DestructableObject
{
    public class DestructableObjectDeath : MonoBehaviour, IDeath
    {
        [SerializeField] private GameObject _solid;
        [SerializeField] private GameObject _broken;

        private float _deathDelay = 5f;

        private void Awake()
        {
            _solid.SetActive(true);
            _broken.SetActive(false);
        }

        public void Die()
        {
            _solid.SetActive(false);
            _broken.SetActive(true);
            _broken.GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Force);
            StartCoroutine(DestroyTimer());
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(_deathDelay);
            Destroy(gameObject);
        }
    }
}