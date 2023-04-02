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

        [Range(1f, 3f)] public float MovementLifeTime;
        [Range(1, 5)] public int Cooldown;
        [Range(0.5f, 5f)] public float MuzzleVfxLifeTime;
    }
}