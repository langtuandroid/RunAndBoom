using UnityEngine;

namespace CodeBase.StaticData.Monster
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;

        [Range(1, 100)] public int Hp;

        [Range(1, 30)] public int Damage;

        [Range(0, 10)] public float MoveSpeed;

        [Range(0.5f, 1)] public float EffectiveDistance;

        [Range(0.5f, 1)] public float Cleavage;

        [Range(0.5f, 1)] public float AttackCooldown;

        public int DeathPoints;
    }
}