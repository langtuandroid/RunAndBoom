using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud.MobileInputPanel
{
    public class MobileInput : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Image _lookJoystick;
        [SerializeField] private Image _lookJoystickButton;

        private void Awake()
        {
            if (Application.isMobilePlatform)
            {
                _panel.SetActive(true);
                _lookJoystick.ChangeImageAlpha(Constants.Invisible);
                _lookJoystickButton.ChangeImageAlpha(Constants.Invisible);
            }
            else
            {
                _panel.SetActive(false);
            }
        }
    }
}