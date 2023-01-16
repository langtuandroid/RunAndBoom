using CodeBase.Hero;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(HeroRotatingMarker))]
    public class HeroRotatingMarkerEditor : UnityEditor.Editor
    {
        private static float _radius = 0.5f;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(HeroRotatingMarker hero, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Debug.DrawLine(hero.transform.position,
                hero.transform.position + hero.transform.forward * 10, Color.red);
            Gizmos.DrawWireSphere(hero.transform.position, _radius);
        }
    }
}