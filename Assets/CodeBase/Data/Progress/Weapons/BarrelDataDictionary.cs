using System;
using CodeBase.StaticData.Weapons;

namespace CodeBase.Data.Progress.Weapons
{
    [Serializable]
    public class BarrelDataDictionary : SerializableDictionary<HeroWeaponTypeId, int>
    {
    }
}