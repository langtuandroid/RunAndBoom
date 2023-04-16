using CodeBase.Projectiles.Movement;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class MortarBehavior : HeroWeaponAppearance
    {
        [SerializeField] private DrawProjection _drawProjection;

        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void FixedUpdate()
        {
            if (ProjectilesRespawns[0] != null)
                _drawProjection.UpdateTrajectory(GetMovement() as BombMovement);
        }

        private void Update()
        {
            // if ((BombMovement)GetMovement() != null)
            // {
            //     Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            //     new Plane(Vector3.forward, -transform.position).Raycast(ray, out float enter);
            //     Vector3 mouseInWorld = ray.GetPoint(enter);
            //     BombMovement bombMovement = (GetMovement() as BombMovement);
            //     Vector3 speed = (mouseInWorld - transform.position) * bombMovement.Power;
            //     _drawProjection.ShowTrajectory(ProjectilesRespawns[0].position, speed);
            //     // _drawProjection.ShowTrajectory(bombMovement.gameObject.transform.position, speed);
            //     bombMovement.SetSpeed(speed);
            // }
        }
        // private Vector3? _targetPosition = null;
        //
        // public void SetTarget(Vector3 targetPosition) =>
        //     _targetPosition = targetPosition;
        //
        // protected virtual IEnumerator CoroutineShootTo()
        // {
        //     if (_targetPosition != null && GetMovement() is BombMovement)
        //         (GetMovement() as BombMovement)?.SetTargetPosition((Vector3)_targetPosition);
        //
        //     Launch();
        //     yield return LaunchProjectileCooldown;
        // }
    }
}