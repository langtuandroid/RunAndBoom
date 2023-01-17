using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Monster;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<IProgressReader> ProgressReaders { get; }
        List<IProgressSaver> ProgressWriters { get; }

        Task WarmUp();
        Task<GameObject> CreateHero(Vector3 at);

        Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent);
        Task CreateSpawner(string spawnerId, Vector3 at, MonsterTypeId spawnerDataMonsterTypeId);
        void CleanUp();
    }
}