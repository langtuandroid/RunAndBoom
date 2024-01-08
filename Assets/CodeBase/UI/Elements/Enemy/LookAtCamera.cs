using System.Collections;
using UnityEngine;

namespace CodeBase.UI.Elements.Enemy
{
    public class LookAtCamera : MonoBehaviour
    {
        private const float MainCameraCreationDelay = 0.5f;
        private Camera _mainCamera;
        private WaitForSeconds _coroutineLookAt;

        private void Start()
        {
            _coroutineLookAt = new WaitForSeconds(MainCameraCreationDelay);
            StartCoroutine(CoroutineLookAt());
        }

        private IEnumerator CoroutineLookAt()
        {
            yield return _coroutineLookAt;
            _mainCamera = Camera.main;

            while (gameObject.activeSelf)
            {
                Quaternion rotation = _mainCamera.transform.rotation;
                transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
                yield return null;
            }
        }
    }
}