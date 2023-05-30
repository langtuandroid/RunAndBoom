using CodeBase.Services.Pool;
using UnityEngine;

namespace CodeBase.StaticData.Projectiles
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "StaticData/Projectiles/Projectile")]
    public class ProjectileStaticData : ScriptableObject
    {
        public ProjectileTypeId ProjectileTypeId;
        public TrailTypeId TrailTypeId;

        [Range(1f, 10f)] public float MovementLifeTime;
        [Range(1, 30)] public int Speed;
    }
}