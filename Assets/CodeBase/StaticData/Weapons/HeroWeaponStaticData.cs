using CodeBase.Services.Pool;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.ShotVfxs;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.StaticData.Weapons
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "StaticData/Weapons/HeroWeapon")]
    public class HeroWeaponStaticData : ScriptableObject
    {
        public HeroWeaponTypeId WeaponTypeId;
        public ProjectileTypeId ProjectileTypeId;

        [FormerlySerializedAs("trailTypeId")] [FormerlySerializedAs("ProjectileTraceTypeId")]
        public TrailTypeId TrailTypeId;

        [FormerlySerializedAs("shotVfxTypeId")] [FormerlySerializedAs("MuzzleVfxTypeId")]
        public ShotVfxTypeId ShotVfxTypeId;

        [Range(1, 10)] public int RotationSpeed;
        [Range(1, 5)] public int Cooldown;
        [Range(1, 50)] public int AimRange;

        [FormerlySerializedAs("MuzzleVfxLifeTime")] [Range(1f, 5f)]
        public float ShotVfxLifeTime;
    }
}