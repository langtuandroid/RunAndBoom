using CodeBase.Services.Ads;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface IAdListener
    {
        void Construct(GameObject hero, IAdsService adsService, IPlayerProgressService progressService);
    }
}