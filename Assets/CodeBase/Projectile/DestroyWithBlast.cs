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

            for (int i = 0; i < objectsHitsCount; i++)
            {
                IDeath death = objectsHits[i].transform.gameObject.GetComponent<IDeath>();
                death.Die();
            }
        }

        private int GetObjectsHits(RaycastHit[] enemiesHits) =>
            Physics.SphereCastNonAlloc(transform.position, _sphereRadius, transform.forward, enemiesHits, _sphereDistance, _objectLayerMask,
                QueryTriggerInteraction.UseGlobal);
    }
}