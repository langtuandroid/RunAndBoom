using System.Collections;
using CodeBase.Data;
using CodeBase.Services.Input.Platforms;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroMovement : MonoBehaviour
    {
        private const string BounceTag = "Bounce";
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _delay = 0f;

        [Inject] private IPlatformInputService _platformInputService;
        private float _startX;
        private float _lineOffset = 5f;

        private Coroutine _moveAsideCoroutine;
        private Coroutine _runCoroutine;

        private void Awake()
        {
            // _platformInputService = AllServices.Container.Single<IPlatformInputService>();

            _platformInputService.Moved += MoveTo;
        }

        private void MoveTo(Vector2 direction)
        {
            if (_moveAsideCoroutine != null)
                StopCoroutine(_moveAsideCoroutine);

            float resultX = direction.y * _lineOffset + _startX;
            _moveAsideCoroutine = StartCoroutine(OnMoveTo(resultX));
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.CompareByTag(BounceTag))
            {
                if (_moveAsideCoroutine != null)
                    StopCoroutine(_moveAsideCoroutine);

                _moveAsideCoroutine = StartCoroutine(OnMoveTo(_startX));
            }

            //TODO(Insert in PlayerVictory.cs)
            // if (other.gameObject.CompareTag("Finish"))
            // {
            //     StopGame();
            // }
        }


        private IEnumerator OnMoveTo(float targetX)
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(_delay);

            while (transform.position.x != targetX)
            {
                float newX = Mathf.MoveTowards(transform.position.x, targetX, _moveSpeed * Time.deltaTime);
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);

                yield return null;
            }

            _startX = targetX;
        }
    }
}