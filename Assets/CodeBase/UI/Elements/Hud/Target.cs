using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _height = 5f;
        [SerializeField] private float _upset = 0.5f;

        private bool _show = false;

        private void Start() =>
            Hide();

        private void Update()
        {
            // if (_show)
            // {
            //     float newY = Mathf.Sin(Time.time * _speed) * _height + transform.position.y;
            //     Debug.Log($"newY {newY}");
            //     transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            // }
        }

        public void Show()
        {
            _image.transform.gameObject.SetActive(true);
            // _show = true;
            Vector3[] path = { transform.position, transform.position + new Vector3(0, 0.5f, 0), transform.position };

            _image.transform.DOPath(path, 2f, PathType.Linear, PathMode.Ignore).SetLoops(-1);
        }

        public void Hide() =>
            _image.transform.gameObject.SetActive(false);
    }
}