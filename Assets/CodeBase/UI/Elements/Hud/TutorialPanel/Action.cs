using CodeBase.UI.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud.TutorialPanel
{
    public class Action : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI Text;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _image.ChangeImageAlpha(Constants.AlphaTutorialItem);
        }

        public void Show() =>
            gameObject.SetActive(true);

        public void Hide() =>
            gameObject.SetActive(false);
    }
}