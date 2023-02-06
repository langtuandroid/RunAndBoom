using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Projectile
{
    public class DestroyWithBlast : MonoBehaviour
    {
        [SerializeField] private LayerMask _objectLayerMask;

        private int _objectsHitsCount = 16;
        private float _sphereDistance = 0f;
        private float _sphereRadius = 20f;

        private List<EnemyHealth> _enemies = new List<EnemyHealth>();

        public void DestroyAllAround()
        {
            _enemies.Clear();
            RaycastHit[] objectsHits = new RaycastHit[_objectsHitsCount];
            int objectsHitsCount = GetObjectsHits(objectsHits);
            IDeath death = null;

            for (int i = 0; i < objectsHitsCount; i++)
            {
                death = objectsHits[i].transform.gameObject.GetComponent<IDeath>();

                if (death == null)
                    death = objectsHits[i].transform.parent.gameObject.GetComponent<IDeath>();

                death?.Die();
            }
        }

        private int GetObjectsHits(RaycastHit[] hits) =>
            Physics.SphereCastNonAlloc(transform.position, _sphereRadius, transform.forward, hits, _sphereDistance, _objectLayerMask,
                QueryTriggerInteraction.UseGlobal);
    }
}