using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Projectiles.Hit
{
    public class DestroyWithBlast : MonoBehaviour
    {
        [SerializeField] private LayerMask _objectLayerMask;

        private const string EnemyTag = "Enemy";
        private const string DestructableTag = "Destructable";

        private int _objectsHitsCount = 16;
        private float _sphereDistance = 0f;
        private List<EnemyHealth> _enemies = new List<EnemyHealth>();

        public void HitAllAround(float sphereRadius, float damage)
        {
            _enemies.Clear();
            RaycastHit[] objectsHits = new RaycastHit[_objectsHitsCount];
            int objectsHitsCount = GetObjectsHits(objectsHits, sphereRadius);
            IDeath death = null;
            IHealth health = null;

            for (int i = 0; i < objectsHitsCount; i++)
            {
                string objectTag = objectsHits[i].transform.gameObject.tag;

                if (objectTag == EnemyTag)
                {
                    health = objectsHits[i].transform.gameObject.GetComponent<IHealth>();

                    health.TakeDamage(damage);

                    death = objectsHits[i].transform.gameObject.GetComponent<IDeath>();

                    if (death == null)
                        death = objectsHits[i].transform.parent.gameObject.GetComponent<IDeath>();

                    death?.Die();
                }
                else if (objectTag == DestructableTag)
                {
                    death = objectsHits[i].transform.gameObject.GetComponent<IDeath>();

                    if (death == null)
                        death = objectsHits[i].transform.parent.gameObject.GetComponent<IDeath>();

                    death?.Die();
                }
            }
        }

        private int GetObjectsHits(RaycastHit[] hits, float sphereRadius) =>
            Physics.SphereCastNonAlloc(transform.position, sphereRadius, transform.forward, hits, _sphereDistance,
                _objectLayerMask,
                QueryTriggerInteraction.UseGlobal);
    }
}