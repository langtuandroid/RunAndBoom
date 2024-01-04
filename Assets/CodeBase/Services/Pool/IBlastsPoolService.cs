using CodeBase.StaticData.Hits;
using CodeBase.StaticData.ShotVfxs;
using UnityEngine;

namespace CodeBase.Services.Pool
{
    public interface IBlastsPoolService : IPoolService
    {
        GameObject GetFromPool(BlastTypeId typeId);
    }
}