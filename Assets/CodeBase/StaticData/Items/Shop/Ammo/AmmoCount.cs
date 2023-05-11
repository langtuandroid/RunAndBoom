using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.Ammo
{
    [CreateAssetMenu(fileName = "AmmoCount", menuName = "StaticData/Items/Shop/AmmoCount")]
    public class AmmoCount : ScriptableObject
    {
        public HeroWeaponTypeId WeaponTypeId;
        public AmmoCountType AmmoCountType;
        public int Count;
    }
}