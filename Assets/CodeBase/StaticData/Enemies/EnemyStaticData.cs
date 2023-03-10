using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.StaticData.Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyTypeId EnemyTypeId;
        public EnemyWeaponTypeId EnemyWeaponTypeId;

        [Range(1, 100)] public int Hp;

        [Range(1, 30)] public int Damage;

        [Range(0, 10)] public float MoveSpeed;

        [Range(0.5f, 4f)] public float EffectiveDistance;

        [Range(0.5f, 5f)] public float Cleavage;

        [Range(0.5f, 5f)] public float AttackCooldown;

        [Range(1, 3)] public int Reward;
    }
}