using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Enemy
{
    public class TargetMovement : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private const float Speed = 1f;
        private const float Height = 0.5f;

        private float _topY;
        private float _bottomY;
        private float _targetY;
        private bool _up = true;
        private bool _show = false;
        private Coroutine _moveCoroutine;

        private void Start()
        {
            _bottomY = transform.position.y;
            _topY = _bottomY + Height;
        }

        private void Update()
        {
            if (_show)
            {
                _bottomY = transform.position.y;
                _topY = _bottomY + Height;

                _targetY = _up ? _topY : _bottomY;

                if (_moveCoroutine == null)
                    _moveCoroutine = StartCoroutine(MoveCoroutine());
            }
            else
            {
                if (_moveCoroutine != null)
                    StopCoroutine(_moveCoroutine);
            }
        }

        private IEnumerator MoveCoroutine()
        {
            while (_image.transform.position.y != _targetY)
            {
                var position = MoveTarget();
                CheckAchievingTarget(position);

                yield return null;
            }
        }

        private Vector3 MoveTarget()
        {
            var position = _image.transform.position;
            position = Vector3.MoveTowards(position,
                new Vector3(position.x, _targetY, position.z), Time.deltaTime * Speed);
            _image.transform.position = position;
            return position;
        }

        private void CheckAchievingTarget(Vector3 position)
        {
            if (_targetY == position.y)
            {
                if (_targetY == _topY)
                    _up = false;
                else if (_targetY == _bottomY)
                    _up = true;
            }
        }

        public void Show()
        {
            _show = true;
            _image.transform.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _show = false;
            _image.transform.gameObject.SetActive(false);
        }
    }
}