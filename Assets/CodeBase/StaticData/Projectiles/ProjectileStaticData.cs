using CodeBase.Services.Pool;
using CodeBase.StaticData.Hits;
using UnityEngine;

namespace CodeBase.StaticData.Projectiles
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "StaticData/Projectiles/Projectile")]
    public class ProjectileStaticData : ScriptableObject
    {
        public ProjectileTypeId ProjectileTypeId;
        public TrailTypeId TrailTypeId;
        public BlastTypeId BlastTypeId;
        public HitTipeId HitTipeId;

        [Range(1f, 6f)] public float MovementLifeTime;
        [Range(1, 30)] public int Speed;

        private GameObject Prefab;
    }
}