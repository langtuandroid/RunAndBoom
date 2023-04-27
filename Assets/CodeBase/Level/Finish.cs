using UnityEngine;

namespace CodeBase.Level
{
    public class Finish : MonoBehaviour
    {
        public GameObject pickupEffect;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hero"))
                Pickup();
        }

        private void Pickup()
        {
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}