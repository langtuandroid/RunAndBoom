using System.Collections;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud
{
    public class LookAtCamera : MonoBehaviour
    {
        private const float MainCameraCreationDelay = 0.5f;
        private const float RefreshDelay = 0.2f;
        private Camera _mainCamera;

        private void Start() =>
            StartCoroutine(CoroutineLookAt());

        private IEnumerator CoroutineLookAt()
        {
            yield return new WaitForSeconds(MainCameraCreationDelay);
            _mainCamera = Camera.main;
            Quaternion rotation = _mainCamera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
            yield return new WaitForSeconds(RefreshDelay);
        }
    }
}