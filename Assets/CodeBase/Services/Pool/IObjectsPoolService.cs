using System.Threading.Tasks;
using CodeBase.StaticData.ShotVfxs;
using UnityEngine;

namespace CodeBase.Services.Pool
{
    public interface IObjectsPoolService : IService
    {
        void GenerateObjects();
        Task<GameObject> GetEnemyProjectile(string name);
        Task<GameObject> GetHeroProjectile(string name);
        Task<GameObject> GetShotVfx(ShotVfxTypeId typeId);
        void ReturnEnemyProjectile(string name, GameObject gameObject);
        void ReturnHeroProjectile(string name, GameObject gameObject);
        void ReturnShotVfx(string name, GameObject gameObject);
    }
}