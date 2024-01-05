using CodeBase.StaticData.Hits;
using UnityEngine;

namespace CodeBase.Services.Pool
{
    public interface IBlastsPoolService : IPoolService
    {
        GameObject GetFromPool(BlastTypeId typeId);
    }
}