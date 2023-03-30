using UnityEngine;

namespace CodeBase.StaticData.ShotVfxs
{
    [CreateAssetMenu(fileName = "ShotVfxData", menuName = "StaticData/Projectiles/ShotVfx")]
    public class ShotVfxStaticData : ScriptableObject
    {
        public ShotVfxTypeId TypeId;
        public GameObject Prefab;
    }
}