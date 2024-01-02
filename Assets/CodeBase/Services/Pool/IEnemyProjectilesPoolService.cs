using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Services.Pool
{
    public interface IEnemyProjectilesPoolService : IPoolService
    {
        GameObject GetFromPool(EnemyWeaponTypeId typeId);
    }
}