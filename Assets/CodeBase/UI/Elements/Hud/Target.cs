using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private void Start() =>
            Hide();

        public void Show()
        {
            _image.transform.gameObject.SetActive(true);
            Vector3[] path = new[] { transform.position, transform.position + new Vector3(0, 0.5f, 0), transform.position };
            _image.transform.DOPath(path, 1f, PathType.Linear, PathMode.Ignore).SetLoops(-1);
        }

        public void Hide()
        {
            _image.transform.gameObject.SetActive(false);
        }
    }
}