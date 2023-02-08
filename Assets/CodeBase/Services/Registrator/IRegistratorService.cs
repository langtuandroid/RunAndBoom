using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Services.Registrator
{
    public interface IRegistratorService : IService
    {
        List<IProgressReader> ProgressReaders { get; }
        List<IProgressSaver> ProgressWriters { get; }
        GameObject InstantiateRegistered(GameObject prefab, Vector3 at);
        GameObject InstantiateRegistered(GameObject prefab);
        Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 at);
        Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Transform parent);
        Task<GameObject> InstantiateRegisteredAsync(string prefabPath);
        Task<GameObject> LoadRegisteredAsync(string prefabPath);
    }
}