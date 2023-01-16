using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _following;

        private readonly float _distance = 20f;

        private void LateUpdate()
        {
            if (_following == null)
                return;

            transform.position = new Vector3(0, _distance, 0) + _following.position;
        }

        public void Follow(GameObject following) =>
            _following = following.transform;
    }
}