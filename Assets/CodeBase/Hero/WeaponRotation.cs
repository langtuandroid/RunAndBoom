using System;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Hero
{
    public class WeaponRotation : MonoBehaviour
    {
        [SerializeField] private HeroWeaponSelection _weaponSelection;
        [SerializeField] private GameObject _currentWeapon;

        private Camera _mainCamera;
        private float _centralPosition = 0.5f;
        private float _rotateDuration = 0.5f;
        private float _maxDistance = 1000f;

        public Action<Vector3> GotTarget;

        private void Start() =>
            _mainCamera = Camera.main;

        private void Awake() =>
            _weaponSelection.WeaponSelected += WeaponChosen;

        private void WeaponChosen(GameObject selectedWeapon, HeroWeaponStaticData arg2, TrailStaticData arg3) =>
            _currentWeapon = selectedWeapon;

        private void FixedUpdate()
        {
            if (_currentWeapon != null)
            {
                Ray ray = _mainCamera.ViewportPointToRay(new Vector3(_centralPosition, _centralPosition, 0));
                RaycastHit hit;
                RaycastHit[] results = new RaycastHit[1];
                int count = Physics.RaycastNonAlloc(ray, results, _maxDistance);

                if (count > 0)
                {
                    Vector3 targetPosition = results[0].point;
                    _currentWeapon.transform.LookAt(targetPosition);
                    GotTarget?.Invoke(targetPosition);
                    Debug.DrawLine(transform.position, results[0].point, Color.red);
                }
            }
        }
    }
}