using CodeBase.Services.Ads;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface IAdListener
    {
        void Construct(GameObject hero, IAdsService adsService);
    }
}