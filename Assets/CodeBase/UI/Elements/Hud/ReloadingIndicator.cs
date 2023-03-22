using System.Collections;
using CodeBase.Hero;
using CodeBase.StaticData.ProjectileTraces;
using CodeBase.StaticData.Weapons;
using JetBrains.Annotations;
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
        private HeroShooting _heroShooting;
        private Vector3 _startEulerAngles;

        private void Awake() =>
            _slider = GetComponent<Slider>();

        public void Construct(HeroShooting heroShooting, HeroWeaponSelection heroWeaponSelection)
        {
            _startEulerAngles = _reloadingImage.transform.eulerAngles;
            _heroShooting = heroShooting;
            _heroWeaponSelection = heroWeaponSelection;

            _heroShooting.OnStartReloading += Reload;
            _heroShooting.OnStopReloading += Stop;
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

        private void Stop([CanBeNull] GameObject o, HeroWeaponStaticData heroWeaponStaticData, [CanBeNull] ProjectileTraceStaticData arg3)
        {
            if (_rotationCoroutine != null)
                StopCoroutine(_rotationCoroutine);

            _reloadingImage.transform.eulerAngles = _startEulerAngles;
            _reloadingImage.gameObject.SetActive(false);
            _progressImage.gameObject.SetActive(false);
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