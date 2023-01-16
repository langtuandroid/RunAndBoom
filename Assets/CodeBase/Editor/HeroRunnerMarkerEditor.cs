using CodeBase.Hero;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(HeroRunnerMarker))]
    public class HeroRunnerMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(HeroRunnerMarker hero, GizmoType gizmo)
        {
            Gizmos.color = Color.green;
            Debug.DrawLine(hero.transform.position, hero.transform.position + hero.transform.forward * 10,
                Color.green);
        }
    }
}