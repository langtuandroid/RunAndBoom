using UnityEngine;

namespace CodeBase.StaticData.Hits
{
    [CreateAssetMenu(fileName = "HitData", menuName = "StaticData/Projectiles/Hits")]
    public class HitStaticData : BaseHitStaticData
    {
        public HitTipeId TipeId;
    }
}