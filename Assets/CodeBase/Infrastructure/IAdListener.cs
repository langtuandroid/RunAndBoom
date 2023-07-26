using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface IAdListener
    {
        void Construct(GameObject hero, ILoadingCurtain loadingCurtain);
        void SubscribeAdsService();
    }
}