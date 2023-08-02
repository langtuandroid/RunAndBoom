using CodeBase.Data;
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
        private const float YDelta = 0.01f;

        private AudioSource _audioSource;
        private float _minY;
        private float _maxY;
        private float _positionY;
        private float _targetY;
        private bool _close;
        private Coroutine _movementCoroutine;
        private PlayerProgress _progress;
        private float _volume;
        private bool _move;
        private Vector3 _doorPosition;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _doorPosition = _door.GetComponent<Transform>().position;
            _positionY = _doorPosition.y;
            _targetY = _positionY;
            _minY = _positionY - _door.GetComponent<MeshRenderer>().bounds.size.y;
            _maxY = _positionY;

            _trigger.Passed += Close;
        }

        private void Close()
        {
            _targetY = _maxY;
            _close = true;
        }

        private void Update() =>
            TryMoveDoor();

        private void TryMoveDoor()
        {
            if (!IsArchivedTarget() && _move)
                _doorPosition = Vector3.MoveTowards(_doorPosition,
                    new Vector3(_doorPosition.x, _targetY, _doorPosition.z), Speed * Time.deltaTime);
            else
                _move = false;
        }

        private bool IsArchivedTarget() =>
            Mathf.Abs(_positionY) - Mathf.Abs(_targetY) < YDelta;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareByTag(Constants.HeroTag) && _close == false)
            {
                _targetY = _minY;
                SoundInstance.GetClipFromLibrary(AudioClipAddresses.DoorClosing);
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.DoorClosing), transform: transform,
                    _volume, _audioSource);
                _move = true;
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
                    _volume, _audioSource);
                _move = true;
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