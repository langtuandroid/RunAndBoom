using UnityEngine;

namespace CodeBase.UI.Elements.Hud.MobileInputPanel
{
    public class MobileInput : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;

        private void Awake() =>
            _panel.SetActive(Application.isMobilePlatform);
    }
}