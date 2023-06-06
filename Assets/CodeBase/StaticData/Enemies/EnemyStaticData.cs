using CodeBase.StaticData.Weapons;
using UnityEngine;
using UnityEngine.AddressableAssets;

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

        public bool IsMovable;

        [Range(0.5f, 50f)] public float FollowDistance;

        [Range(0.5f, 50f)] public float AttackDistance;

        [Range(0.1f, 5f)] public float AttackCooldown;

        [Range(1, 3)] public int Reward;

        public AssetReferenceGameObject PrefabReference;
    }
}