using UnityEngine;

namespace CodeBase.StaticData.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyTypeId enemyTypeId;

        [Range(1, 100)] public int Hp;

        [Range(1, 30)] public int Damage;

        [Range(0, 10)] public float MoveSpeed;

        [Range(0.5f, 1)] public float EffectiveDistance;

        [Range(0.5f, 1)] public float Cleavage;

        [Range(0.5f, 1)] public float AttackCooldown;
    }
}