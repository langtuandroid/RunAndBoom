using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    [CreateAssetMenu(fileName = "UpgradableWeaponData", menuName = "StaticData/Items/Shop/UpgradableWeapon")]
    public class UpgradableWeaponStaticData : ScriptableObject
    {
        public HeroWeaponTypeId WeaponTypeId;
        public Sprite UpgradableWeapon;
    }
}