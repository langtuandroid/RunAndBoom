using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData.Weapon
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "StaticData/Weapon")]
    public class WeaponStaticData : ScriptableObject
    {
        public WeaponTypeId WeaponTypeId;
        public string Name;

        [Range(1f, 100f)] public int MainFireCost;

        // [Range(1f, 100f)] public int SecondaryFireCost;
        [Range(1f, 30f)] public int MainFireDamage;

        // [Range(1f, 30f)] public int SecondaryFireDamage;
        [Range(0f, 10f)] public float MainFireRotationSpeed;

        // [Range(0f, 10f)] public float SecondaryFireRotationSpeed;
        [Range(0f, 5f)] public float MainFireCooldown;

        // [Range(0f, 5f)] public float SecondaryFireCooldown;
        [Range(1f, 6)] public int MainFireBarrels;

        // [Range(1f, 6)] public int SecondaryFireBarrels;
        [Range(0f, 50f)] public float MainFireRange;

        // [Range(0f, 50f)] public float SecondaryFireRange;
        [Range(0f, 30f)] public float MainFireBulletSpeed;
        // [Range(0f, 30f)] public float SecondaryFireBulletSpeed;

        public Sprite Icon;
        public AssetReferenceGameObject PrefabReference;
    }
}