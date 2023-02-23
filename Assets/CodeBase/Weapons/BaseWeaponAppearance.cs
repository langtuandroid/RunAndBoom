using System.Collections.Generic;
using CodeBase.Projectiles;
using CodeBase.StaticData.ProjectileTrace;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class BaseWeaponAppearance : MonoBehaviour
    {
        [SerializeField] protected GameObject _projectilePrefab;
        [SerializeField] protected Transform[] _projectilesRespawns;
        [SerializeField] protected Transform[] _muzzlesRespawns;
        [SerializeField] protected bool _showProjectiles;

        protected List<GameObject> _projectileObjects;
        protected List<ProjectileMovement> _projectileMovements;
        protected List<ProjectileTrace> _traces;
        protected WaitForSeconds _launchProjectileCooldown;
        protected ProjectileTraceStaticData _currentProjectileTraceStaticData;
        protected GameObject _vfxShot;
        protected float _muzzleVfxLifetime;

        protected void Construct()
        {
            _projectileObjects = new List<GameObject>(_projectilesRespawns.Length);
            _projectileMovements = new List<ProjectileMovement>(_projectilesRespawns.Length);
            _traces = new List<ProjectileTrace>(_projectilesRespawns.Length);
        }
    }
}