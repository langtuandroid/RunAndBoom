using UnityEngine;

namespace CodeBase.Services.Pool
{
    public interface IPoolService : IService
    {
        void GenerateObjects();
        void Return(GameObject gameObject);
    }
}