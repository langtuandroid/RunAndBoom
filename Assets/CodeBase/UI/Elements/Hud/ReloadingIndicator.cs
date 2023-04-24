using System.Collections;
using CodeBase.Hero;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud
{
    public class ReloadingIndicator : MonoBehaviour
    {
        [SerializeField] private Image _reloadingImage;
        [SerializeField] private Image _progressImage;

        private const float RotationSpeed = 150f;

        private Slider _slider;
        private HeroWeaponSelection _heroWeaponSelection;
        private Coroutine _rotationCoroutine;
        private HeroReloading _heroReloading;
        private Vector3 _startEulerAngles;

        private void Awake() =>
            _slider = GetComponent<Slider>();

        public void Construct(HeroReloading heroReloading, HeroWeaponSelection heroWeaponSelection)
        {
            _startEulerAngles = _reloadingImage.transform.eulerAngles;
            _heroReloading = heroReloading;
            _heroWeaponSelection = heroWeaponSelection;

            _heroReloading.OnStartReloading += Reload;
            _heroReloading.OnStopReloading += Stop;
            _heroWeaponSelection.WeaponSelected += Stop;
        }

        private void Reload(float value)
        {
            if (_reloadingImage.gameObject.activeSelf == false)
                _reloadingImage.gameObject.SetActive(true);

            if (_progressImage.gameObject.activeSelf == false)
                _progressImage.gameObject.SetActive(true);

            if (_rotationCoroutine == null)
                _rotationCoroutine = StartCoroutine(CoroutineRotateImage());

            LaunchProgressImage(value);
        }

        private void Stop()
        {
            _reloadingImage.transform.eulerAngles = _startEulerAngles;
            _reloadingImage.gameObject.SetActive(false);
            _progressImage.gameObject.SetActive(false);
        }

        private void Stop(GameObject o, HeroWeaponStaticData h, TrailStaticData t)
        {
            if (_rotationCoroutine != null)
                StopCoroutine(_rotationCoroutine);

            Stop();
        }

        private IEnumerator CoroutineRotateImage()
        {
            while (true)
            {
                Vector3 angles = _reloadingImage.transform.eulerAngles;
                angles.z = angles.z - RotationSpeed * Time.deltaTime;
                _reloadingImage.transform.eulerAngles = angles;
                yield return null;
            }
        }

        private void LaunchProgressImage(float value) =>
            _slider.value = value;
    }
}