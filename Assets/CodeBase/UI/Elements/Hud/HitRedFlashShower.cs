using System.Collections;
using CodeBase.Hero;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud
{
    public class HitRedFlashShower : MonoBehaviour
    {
        private Image _image;
        private Color _activeStatus;
        private Color _inactiveStatus;
        private WaitForSeconds _waitForSeconds;
        private Coroutine _coroutine;
        private HeroHealth _heroHealth;

        public void Construct(HeroHealth heroHealth)
        {
            _image = GetComponent<Image>();
            _activeStatus = _image.color;
            _inactiveStatus = new Color(_activeStatus.r, _activeStatus.g, _activeStatus.b, Constants.Zero);
            _image.color = _inactiveStatus;
            _waitForSeconds = new WaitForSeconds(0.5f);
            _heroHealth = heroHealth;
            _heroHealth.HealthDamaged += Show;
        }

        private void Show()
        {
            _image.color = _activeStatus;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(CoroutineHide());
        }

        private IEnumerator CoroutineHide()
        {
            yield return _waitForSeconds;
            _image.color = _inactiveStatus;
        }
    }
}