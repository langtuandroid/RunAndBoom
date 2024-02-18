using CodeBase.Hero;
using CodeBase.Services;
using CodeBase.Services.Audio;
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

        private AreaTriggersChecker _areaTriggersChecker;
        private AudioSource _audioSource;
        private float _positionY;
        private float _targetY;
        private Transform _doorTransform;
        private bool _close;
        private Coroutine _movementCoroutine;
        private IAudioService _audioService;
        private float _volume;

        private void Awake()
        {
            _areaTriggersChecker = GetComponent<AreaTriggersChecker>();
            _audioSource = GetComponent<AudioSource>();
            _doorTransform = _door.GetComponent<Transform>();
            _positionY = _door.transform.position.y;
            _targetY = _positionY;
            _minY = _positionY - _door.GetComponent<MeshRenderer>().bounds.size.y;
            _maxY = _positionY;

            _audioService = AllServices.Container.Single<IAudioService>();
        }

        private void OnEnable()
        {
            _trigger.Passed += Close;
            _areaTriggersChecker.Cleared += Open;
        }

        private void OnDisable()
        {
            _trigger.Passed -= Close;
            _areaTriggersChecker.Cleared -= Open;
        }

        private void Update() =>
            MoveDoor();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HeroHealth heroHealth) && _close == false)
                _areaTriggersChecker.CheckTriggersForEnemies();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out HeroHealth heroHealth) && _close == false)
            {
                _targetY = _maxY;
                _audioService.LaunchDoorSound(DoorSoundId.DoorClosing, transform, _audioSource);
            }
        }

        private void Close()
        {
            _targetY = _maxY;
            _close = true;
        }

        private void MoveDoor()
        {
            if (_doorTransform.position.y != _targetY)
                _doorTransform.position = Vector3.MoveTowards(_doorTransform.position,
                    new Vector3(_doorTransform.position.x, _targetY, _doorTransform.position.z),
                    _speed * Time.deltaTime);
        }

        private void Open()
        {
            _targetY = _minY;
            _audioService.LaunchDoorSound(DoorSoundId.DoorOpening, transform, _audioSource);
        }
    }
}