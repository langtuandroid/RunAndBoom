using UnityEngine;

namespace CodeBase.Services.Input
{
    public class DesktopInputService : InputService
    {
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";

        public override bool IsAttackButtonUp() =>
            UnityEngine.Input.GetMouseButton(0);

        public override Vector2 MoveAxis
        {
            get
            {
                Vector2 axis = MoveSimpleInputAxis();

                if (axis == Vector2.zero)
                    axis = UnityAxis();

                return axis;
            }
        }

        public override Vector2 LookAxis =>
            new(UnityEngine.Input.GetAxisRaw(MouseX), UnityEngine.Input.GetAxisRaw(MouseY));

        private static Vector2 UnityAxis() =>
            new(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
    }
}