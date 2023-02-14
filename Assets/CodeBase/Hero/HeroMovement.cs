using CodeBase.Services.Input.Platforms;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroMovement : MonoBehaviour
    {
        private IPlatformInputService _platformInputService;

        private float _moveSpeed = 5f;
        private Vector3 _movement = Vector3.zero;

        private void Update() =>
            transform.Translate(_movement * _moveSpeed * Time.deltaTime);

        [Inject]
        public void Construct(IPlatformInputService platformInputService)
        {
            _platformInputService = platformInputService;
            SubscribeServicesEvents();
        }

        private void SubscribeServicesEvents() =>
            _platformInputService.Moved += MoveTo;

        private void MoveTo(Vector2 direction) =>
            _movement = new Vector3(direction.x, 0, direction.y);
    }
}