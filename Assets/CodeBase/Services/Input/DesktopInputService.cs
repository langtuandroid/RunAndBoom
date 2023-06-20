using UnityEngine;

namespace CodeBase.Services.Input
{
    public class DesktopInputService : InputService
    {
        public override bool IsAttackButtonUp() =>
            UnityEngine.Input.GetMouseButton(0);

        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = SimpleInputAxis();

                if (axis == Vector2.zero)
                    axis = UnityAxis();

                return axis;
            }
        }

        private static Vector2 UnityAxis() =>
            new(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
    }
}