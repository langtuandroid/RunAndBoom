using CodeBase.StaticData.ProjectileTraces;
using UnityEngine;

namespace CodeBase.StaticData.Weapons
{
    [CreateAssetMenu(fileName = "EnemyWeaponData", menuName = "StaticData/Weapons/EnemyWeapon")]
    public class EnemyWeaponStaticData : ScriptableObject
    {
        public EnemyWeaponTypeId WeaponTypeId;
        public ProjectileTraceTypeId ProjectileTraceTypeId;

        [Range(1f, 3f)] public float MovementLifeTime;
        [Range(1, 30)] public int Damage;
        [Range(1, 5)] public int Cooldown;
        [Range(1, 30)] public int ProjectileSpeed;
        [Range(0.5f, 5f)] public float MuzzleVfxLifeTime;

        public GameObject MuzzleVfx;
        public GameObject ProjectilePrefab;
        public GameObject ProjectileTracePrefab;
    }
}