using UnityEngine;

namespace CodeBase.StaticData.ProjectileTraces
{
    [CreateAssetMenu(fileName = "ProjectileTraceData", menuName = "StaticData/ProjectileTrace")]
    public class ProjectileTraceStaticData : ScriptableObject
    {
        public ProjectileTraceTypeId ProjectileTraceTypeId;
        public float StartDelay;
        public float EndDelay;
        public GameObject PrefabReference;
    }
}