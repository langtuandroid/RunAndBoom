using CodeBase.Logic;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(ProjectileDirection))]
    public class ProjectileDirectionEditor : UnityEditor.Editor
    {
        private static float _length = 5f;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(ProjectileDirection direction, GizmoType gizmo)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(direction.transform.position,
                direction.transform.position + direction.transform.forward * _length);
        }
    }
}