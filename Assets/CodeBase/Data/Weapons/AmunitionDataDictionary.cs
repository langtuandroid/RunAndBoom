using System;
using CodeBase.StaticData.Weapons;

namespace CodeBase.Data.Weapons
{
    [Serializable]
    public class AmunitionDataDictionary : SerializableDictionary<HeroWeaponTypeId, int>
    {
    }
}