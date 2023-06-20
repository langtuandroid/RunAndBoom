using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Data.Perks;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroMovement : MonoBehaviour, IProgressReader
    {
        private const float BaseRatio = 1f;
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Camera _camera;

        private IStaticDataService _staticDataService;
        private IInputService _inputService;
        private float _baseMovementSpeed = 5f;
        private float _movementRatio = 1f;
        private float _movementSpeed;
        private bool _canMove;
        private PerkItemData _runningItemData;
        private PlayerProgress _progress;
        private List<PerkItemData> _perks;
        private Rigidbody _rigidbody;
        private Vector3 _playerMovementInput;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update() => 
            Move();

        private void Move()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector =
                    _camera.transform.TransformDirection(new Vector3(_inputService.Axis.x, 0, _inputService.Axis.y));
                movementVector.Normalize();
            }

            movementVector += Physics.gravity;

            _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
        }

        private void OnEnable()
        {
            if (_runningItemData != null)
                _runningItemData.LevelChanged += ChangeSpeed;
        }

        private void OnDisable()
        {
            if (_runningItemData != null)
                _runningItemData.LevelChanged -= ChangeSpeed;
        }

        public void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        private void SetSpeed()
        {
            _runningItemData = _perks.Find(x => x.PerkTypeId == PerkTypeId.Running);
            _runningItemData.LevelChanged += ChangeSpeed;
            ChangeSpeed();
        }

        private void ChangeSpeed()
        {
            if (_runningItemData.LevelTypeId == LevelTypeId.None)
                _movementRatio = BaseRatio;
            else
                _movementRatio = _staticDataService.ForPerk(PerkTypeId.Running, _runningItemData.LevelTypeId).Value;

            _movementSpeed = _baseMovementSpeed * _movementRatio;
        }

        public void TurnOn() =>
            _canMove = true;

        public void TurnOff() =>
            _canMove = false;

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;
            _perks = _progress.PerksData.Perks;
            SetSpeed();
        }
    }
}