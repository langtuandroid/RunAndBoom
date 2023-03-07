using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.DestructableObject
{
    public class DestructableObjectDeath : MonoBehaviour, IDeath
    {
        [SerializeField] private GameObject _solid;
        [SerializeField] private GameObject _broken;

        private float _deathDelay = 50f;
        private bool _isBroken = false;
        private List<Rigidbody> _parts;

        public event Action Died;

        private void Awake()
        {
            _solid.SetActive(true);
            _broken.SetActive(false);
            _parts = new List<Rigidbody>(_broken.transform.childCount);

            for (int i = 0; i < _broken.transform.childCount; i++)
                _parts.Add(_broken.transform.GetChild(i).GetComponent<Rigidbody>());
        }

        public void Die()
        {
            _solid.SetActive(false);
            _broken.SetActive(true);

            if (_isBroken == false)
                foreach (Rigidbody part in _parts)
                    part.AddForce(Vector3.up * 5f, ForceMode.Impulse);

            _isBroken = true;

            StartCoroutine(DestroyTimer());
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(_deathDelay);
            Destroy(gameObject);
        }
    }
}