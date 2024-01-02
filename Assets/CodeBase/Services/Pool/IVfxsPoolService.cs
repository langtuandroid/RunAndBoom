using CodeBase.StaticData.ShotVfxs;
using UnityEngine;

namespace CodeBase.Services.Pool
{
    public interface IVfxsPoolService : IPoolService
    {
        GameObject GetFromPool(ShotVfxTypeId typeId);
    }
}