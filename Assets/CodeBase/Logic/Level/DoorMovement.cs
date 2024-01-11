using CodeBase.Data.Settings;
using CodeBase.Hero;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.Logic.Level
{
    [RequireComponent(typeof(AudioSource))]
    public class DoorMovement : MonoBehaviour
    {
        [SerializeField] private GameObject _door;
        [SerializeField] private LevelSectorTrigger _trigger;
        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private AreaClearChecker _areaClearChecker;

        private AudioSource _audioSource;
        private float _positionY;
        private float _targetY;
        private Transform _doorTransform;
        private bool _close;
        private Coroutine _movementCoroutine;
        private SettingsData _settingsData;
        private float _volume;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _doorTransform = _door.GetComponent<Transform>();
            _positionY = _door.transform.position.y;
            _targetY = _positionY;
            _minY = _positionY - _door.GetComponent<MeshRenderer>().bounds.size.y;
            _maxY = _positionY;

            _settingsData = AllServices.Container.Single<IPlayerProgressService>().SettingsData;
        }

        private void OnEnable()
        {
            _trigger.Passed += Close;

            if (_settingsData == null)
                return;

            _settingsData.SoundSwitchChanged += SwitchChanged;
            _settingsData.SoundVolumeChanged += VolumeChanged;
            VolumeChanged();
            SwitchChanged();
        }

        private void OnDisable()
        {
            _trigger.Passed -= Close;

            if (_settingsData == null)
                return;

            _settingsData.SoundSwitchChanged -= SwitchChanged;
            _settingsData.SoundVolumeChanged -= VolumeChanged;
        }

        private void Close()
        {
            _targetY = _maxY;
            _close = true;
        }

        private void Update() =>
            MoveDoor();

        private void MoveDoor()
        {
            if (_doorTransform.position.y != _targetY)
                _doorTransform.position = Vector3.MoveTowards(_doorTransform.position,
                    new Vector3(_doorTransform.position.x, _targetY, _doorTransform.position.z),
                    _speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HeroHealth heroHealth) && _close == false
                // && _areaClearChecker.IsAreaClear()
               )
            {
                _targetY = _minY;
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.DoorClosing), transform: transform,
                    _volume, _audioSource);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out HeroHealth heroHealth) && _close == false)
            {
                _targetY = _maxY;
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.DoorOpening), transform: transform,
                    _volume, _audioSource);
            }
        }

        private void VolumeChanged() =>
            _volume = _settingsData.SoundVolume;

        private void SwitchChanged() =>
            _volume = _settingsData.SoundOn ? _settingsData.SoundVolume : Constants.Zero;
    }
}