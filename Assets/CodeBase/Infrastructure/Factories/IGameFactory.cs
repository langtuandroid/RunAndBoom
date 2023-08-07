using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        List<IProgressReader> ProgressReaders { get; }
        List<IProgressSaver> ProgressWriters { get; }

        void WarmUp();
        Task<GameObject> CreateHero(Vector3 at);
        void CleanUp();
        GameObject GetHero();
    }
}