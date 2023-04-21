using CodeBase.Data.Perks;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroMovement : MonoBehaviour
    {
        private IPlayerProgressService _progressService;
        private IStaticDataService _staticDataService;
        private float _baseMovementSpeed = 5f;
        private float _speedRatio = 1;
        private Vector3 _movement = Vector3.zero;
        private bool _canMove;
        private PerkItemData _running;

        private void Update()
        {
            if (_canMove)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");

                transform.Translate(
                    new Vector3(horizontalInput, 0, verticalInput) * _baseMovementSpeed * _speedRatio * Time.deltaTime);
            }
        }

        public void Construct(IPlayerProgressService progressService, IStaticDataService staticDataService)
        {
            _progressService = progressService;
            _staticDataService = staticDataService;
            _progressService.Progress.PerksData.IsAvailable(PerkTypeId.Running);
            _running = _progressService.Progress.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.Running);
            _running.LevelChanged += IncreaseSpeed;
            IncreaseSpeed();
        }

        private void IncreaseSpeed()
        {
            if (_running.LevelTypeId != LevelTypeId.None)
                _speedRatio = _staticDataService.ForPerk(PerkTypeId.Running, _running.LevelTypeId).Value;
        }

        private void MoveTo(Vector2 direction) =>
            _movement = new Vector3(direction.x, 0, direction.y);

        public void TurnOn() =>
            _canMove = true;

        public void TurnOff() =>
            _canMove = false;
    }
}