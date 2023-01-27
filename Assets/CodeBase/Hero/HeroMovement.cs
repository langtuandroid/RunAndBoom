using CodeBase.Services.Input.Platforms;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;

        [Inject] private IPlatformInputService _platformInputService;

        private Vector3 _movement = Vector3.zero;

        private void Awake() =>
            _platformInputService.Moved += MoveTo;

        private void Update() =>
            transform.Translate(_movement * _moveSpeed * Time.deltaTime);

        private void MoveTo(Vector2 direction) =>
            _movement = new Vector3(direction.x, transform.position.y, direction.y);
    }
}