using CodeBase.StaticData.ShotVfxs;
using UnityEngine;

namespace CodeBase.Services.Pool
{
    public interface IObjectsPoolService : IService
    {
        void GenerateObjects();
        GameObject GetEnemyProjectile(string name);
        GameObject GetHeroProjectile(string name);
        GameObject GetShotVfx(ShotVfxTypeId typeId);
        void ReturnEnemyProjectile(GameObject gameObject);
        void ReturnHeroProjectile(GameObject gameObject);
        void ReturnShotVfx(GameObject gameObject);
        void StopAllObjects();
        void LaunchAllObjects();
    }
}