using CodeBase.Data;
using CodeBase.Services.Audio;
using CodeBase.Services.PersistentProgress;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.Logic.Level
{
    [RequireComponent(typeof(AudioSource))]
    public class DoorMovement : MonoBehaviour, IProgressReader
    {
        [SerializeField] private GameObject _door;
        [SerializeField] private LevelSectorTrigger _trigger;

        private const float Speed = 10f;

        private AudioSource _audioSource;
        private float _minY;
        private float _maxY;
        private float _positionY;
        private float _targetY;
        private Transform _doorTransform;
        private bool _close;
        private float _currentVolume = 1f;

        private Coroutine _movementCoroutine;
        private PlayerProgress _progress;
        private float _volume;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _doorTransform = _door.GetComponent<Transform>();
            _positionY = _door.transform.position.y;
            _targetY = _positionY;
            _minY = _positionY - _door.GetComponent<MeshRenderer>().bounds.size.y;
            _maxY = _positionY;

            _trigger.Passed += Close;
        }

        private void Close()
        {
            _targetY = _maxY;
            _close = true;
            // AudioUtils.PlaySound(stop: _audioOpenDoor, play: _audioCloseDoor);
        }

        private void Update()
        {
            MoveDoor();
        }

        private void MoveDoor()
        {
            _doorTransform.position = Vector3.MoveTowards(_doorTransform.position,
                new Vector3(_doorTransform.position.x, _targetY, _doorTransform.position.z), Speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareByTag(Constants.HeroTag) && _close == false)
            {
                _targetY = _minY;
                SoundInstance.GetClipFromLibrary(AudioClipAddresses.DoorClosing);
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.DoorClosing), transform: transform,
                    _currentVolume, _audioSource);
                // AudioUtils.PlaySound(stop: _audioCloseDoor, play: _audioOpenDoor);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareByTag(Constants.HeroTag) && _close == false)
            {
                _targetY = _maxY;
                SoundInstance.GetClipFromLibrary(AudioClipAddresses.DoorOpening);
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.DoorOpening), transform: transform,
                    _currentVolume, _audioSource);
                // AudioUtils.PlaySound(stop: _audioOpenDoor, play: _audioCloseDoor);
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;
            _progress.SettingsData.SoundSwitchChanged += SwitchChanged;
            _progress.SettingsData.SoundVolumeChanged += VolumeChanged;
            VolumeChanged();
            SwitchChanged();
        }

        private void VolumeChanged() =>
            _volume = _progress.SettingsData.SoundVolume;

        private void SwitchChanged() =>
            _volume = _progress.SettingsData.SoundOn ? _progress.SettingsData.SoundVolume : Constants.Zero;
    }
}