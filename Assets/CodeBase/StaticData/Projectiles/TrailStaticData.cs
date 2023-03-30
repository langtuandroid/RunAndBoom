using CodeBase.Services.Pool;
using UnityEngine;

namespace CodeBase.StaticData.Projectiles
{
    [CreateAssetMenu(fileName = "ProjectileTraceData", menuName = "StaticData/Projectiles/Trace")]
    public class TrailStaticData : ScriptableObject
    {
        public TrailTypeId TrailTypeId;

        public float StartDelay;
        public float EndDelay;
        public GameObject Prefab;
    }
}