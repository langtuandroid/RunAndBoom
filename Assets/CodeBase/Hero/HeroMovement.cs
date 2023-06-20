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
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _cameraTransform;

        private const float BaseRatio = 1f;

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
            // MoveCharacterController();
            // MoveCharacterControllerWithCorrection();
            MoveRigidbody();
        }

        private void MoveRigidbody()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                Vector3 forward = new Vector3(-_cameraTransform.right.z, 0.0f, _cameraTransform.right.x);
                movementVector =
                    (forward * _inputService.Axis.y + _cameraTransform.right * _inputService.Axis.x +
                     Vector3.up * _rigidbody.velocity.y).normalized * _movementSpeed;
            }

            _rigidbody.velocity = movementVector;
        }

        private void MoveCharacterController()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector =
                    _cameraTransform.TransformDirection(new Vector3(_inputService.Axis.x, 0,
                        _inputService.Axis.y));
                movementVector.Normalize();
            }

            movementVector += Physics.gravity;
            _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
        }

        private void MoveCharacterControllerWithCorrection()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                Vector3 cameraDirection = _cameraTransform.TransformDirection(new Vector3(_inputService.Axis.x, 0,
                    _inputService.Axis.y));
                float x = 0f;
                float y = 0f;
                float z = 0f;

                if (cameraDirection.x > 0)
                    x = 1f;
                if (cameraDirection.x < 0)
                    x = -1f;
                if (cameraDirection.y > 0)
                    y = 1f;
                if (cameraDirection.y < 0)
                    y = -1f;
                if (cameraDirection.z > 0)
                    z = 1f;
                if (cameraDirection.z < 0)
                    z = -1f;

                movementVector = new Vector3(x, y, z);
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