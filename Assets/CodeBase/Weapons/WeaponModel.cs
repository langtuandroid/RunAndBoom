using System.Collections.Generic;
using CodeBase.StaticData.Weapon;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class WeaponModel : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _muzzles;
        [SerializeField] private GameObject _shootVfx;
        [SerializeField] public WeaponTypeId WeaponTypeId;

        // public WeaponArmoryDescription WeaponArmoryDescription { get; private set; }

        public List<GameObject> Muzzles => _muzzles;
        public GameObject ShootVfx => _shootVfx;
    }
}