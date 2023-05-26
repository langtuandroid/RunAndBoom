using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.ShotVfxs;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.StaticData.Weapons
{
    [CreateAssetMenu(fileName = "EnemyWeaponData", menuName = "StaticData/Weapons/EnemyWeapon")]
    public class EnemyWeaponStaticData : ScriptableObject
    {
        public EnemyWeaponTypeId WeaponTypeId;
        public ProjectileTypeId ProjectileTypeId;

        [FormerlySerializedAs("shotVfxTypeId")] [FormerlySerializedAs("MuzzleVfxTypeId")]
        public ShotVfxTypeId ShotVfxTypeId;

        [Range(1f, 10f)] public float MovementLifeTime;
        [Range(0f, 5f)] public float Cooldown;
        [Range(0.0f, 5f)] public float MuzzleVfxLifeTime;
    }
}