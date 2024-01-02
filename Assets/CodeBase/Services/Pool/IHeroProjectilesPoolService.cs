using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Services.Pool
{
    public interface IHeroProjectilesPoolService : IPoolService
    {
        GameObject GetFromPool(HeroWeaponTypeId typeId);
    }
}