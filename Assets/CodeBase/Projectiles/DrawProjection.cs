using System;
using System.Collections.Generic;
using CodeBase.Projectiles.Movement;
using CodeBase.Weapons;
using UnityEngine;

namespace CodeBase.Projectiles
{
    public class DrawProjection : MonoBehaviour
    {
        [SerializeField] private MortarBehavior _mortarBehavior;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private LayerMask _collidableLayers;
        [SerializeField] [Range(3, 50)] private int _lineSegmentCount;
        [SerializeField] private float _timeBetweenPoints = 0.1f;

        private List<Vector3> _linePoints = new List<Vector3>();

        // private void Update()
        // {
        //     if ((BombMovement)_mortarBehavior.GetMovement() != null)
        //     {
        //         _lineRenderer.positionCount = _lineSegmentCount;
        //
        //         if (_mortarBehavior.ProjectilesRespawns.Length == 1)
        //         {
        //             Vector3 startingPosition = _mortarBehavior.ProjectilesRespawns[0].position;
        //             BombMovement bombMovement = ((BombMovement)_mortarBehavior.GetMovement());
        //             Vector3 startingVelocity = _mortarBehavior.ProjectilesRespawns[0].forward * bombMovement.Power;
        //
        //             for (float i = 0; i < _lineSegmentCount; i += _timeBetweenPoints)
        //             {
        //                 Vector3 newPoint = startingPosition + i * startingVelocity;
        //                 newPoint.y = startingPosition.y + startingVelocity.y * i + Physics.gravity.y / 2f * i * i;
        //                 _linePoints.Add(newPoint);
        //
        //                 if (Physics.OverlapSphere(newPoint, 2, _collidableLayers).Length > 0)
        //                 {
        //                     _lineRenderer.positionCount = _linePoints.Count;
        //                     bombMovement.SetTargetPosition(newPoint);
        //                     // _mortarBehavior.SetTarget(newPoint);
        //                     break;
        //                 }
        //             }
        //
        //             _lineRenderer.SetPositions(_linePoints.ToArray());
        //         }
        //     }
        // }

        // public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidbody, Vector3 startingPoint)
        // {
        //     Vector3 velocity = (forceVector / rigidbody.mass) * Time.fixedTime;
        //     float flightDuration = (2 * velocity.y) / Physics.gravity.y;
        //     float stepTime = flightDuration / _lineSegmentCount;
        //     _linePoints.Clear();
        //
        //     for (int i = 0; i < _lineSegmentCount; i++)
        //     {
        //         float stepTimePassed = stepTime * i;
        //
        //         Vector3 movementVector = new Vector3(velocity.x * stepTimePassed,
        //             velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
        //             velocity.z * stepTimePassed);
        //
        //         if (Physics.Raycast(startingPoint, -movementVector, out _, movementVector.magnitude))
        //             break;
        //
        //         _linePoints.Add(-movementVector + startingPoint);
        //     }
        //
        //     _lineRenderer.positionCount = _linePoints.Count;
        //     _lineRenderer.SetPositions(_linePoints.ToArray());
        // }

        public void ShowTrajectory(Vector3 origin, Vector3 speed)
        {
            Vector3[] points = new Vector3[100];
            _lineRenderer.positionCount = points.Length;

            for (int i = 0; i < points.Length; i++)
            {
                float time = i * _timeBetweenPoints;

                points[i] = origin + speed * time + Physics.gravity * time * time / 2f;
            }

            _lineRenderer.SetPositions(points);
        }
    }
}