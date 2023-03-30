using CodeBase.StaticData.ShotVfxs;
using UnityEngine;

namespace CodeBase.Services.Pool
{
    public interface IPoolService : IService
    {
        GameObject GetEnemyProjectile(string name);
        GameObject GetHeroProjectile(string name);
        GameObject GetShotVfx(ShotVfxTypeId typeId);
        void ReturnEnemyProjectile(GameObject gameObject);
        void ReturnHeroProjectile(GameObject gameObject);
        void ReturnShotVfx(GameObject gameObject);
        void GenerateObjects();
    }
}