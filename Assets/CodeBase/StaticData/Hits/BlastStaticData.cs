using UnityEngine;

namespace CodeBase.StaticData.Hits
{
    [CreateAssetMenu(fileName = "BlastData", menuName = "StaticData/Projectiles/Blast")]
    public class BlastStaticData : BaseHitStaticData
    {
        public BlastTypeId TypeId;
        public float Radius;

        public GameObject Prefab;
    }
}