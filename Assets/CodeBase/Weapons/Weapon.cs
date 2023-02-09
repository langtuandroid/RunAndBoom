using UnityEngine;

namespace CodeBase.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform[] _projectilesRespawns;
        [SerializeField] private Transform[] _muzzlesRespawns;

        public Transform[] ProjectilesRespawns => _projectilesRespawns;
        public Transform[] MuzzlesRespawns => _muzzlesRespawns;
    }
}