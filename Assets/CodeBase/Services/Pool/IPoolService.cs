using UnityEngine;

namespace CodeBase.Services.Pool
{
    public interface IPoolService : IService
    {
        void GenerateObjects();
        void ReturnToPool(GameObject gameObject);
    }
}