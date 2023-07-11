using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel
{
    public class TutorialPanel : MonoBehaviour
    {
        [SerializeField] private Settings _settings;
        [SerializeField] private Look _look;
        [SerializeField] private Movement _movement;
        [SerializeField] private Shoot _shoot;
        [SerializeField] private InnerPanels.Weapons _weapons;

        private IInputService _inputService;

        private void Start()
        {
            _inputService = AllServices.Container.Single<IInputService>();

            if (_inputService is MobileInputService)
            {
                _look.ShowForMobile();
                _settings.ShowForMobile();
                _movement.ShowForMobile();
                _shoot.ShowForMobile();
                _weapons.ShowForMobile();
            }
            else
            {
                _look.ShowForPc();
                _settings.ShowForPc();
                _movement.ShowForPc();
                _shoot.ShowForPc();
                _weapons.ShowForPc();
            }
        }

        private void Update()
        {
            if (_inputService.IsAttackButtonUp())
                HidePanel();

            if (_inputService is MobileInputService && _inputService.LookAxis.magnitude > Constants.Epsilon)
                HidePanel();

            if (_inputService is MobileInputService && _inputService.MoveAxis.magnitude > Constants.Epsilon)
                HidePanel();

            if (Input.GetKeyDown(KeyCode.W))
                HidePanel();

            if (Input.GetKeyDown(KeyCode.S))
                HidePanel();

            if (Input.GetKeyDown(KeyCode.A))
                HidePanel();

            if (Input.GetKeyDown(KeyCode.D))
                HidePanel();

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) ||
                Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
                HidePanel();

            if (Input.GetKeyDown(KeyCode.Mouse0))
                HidePanel();

            if (Input.GetKeyDown(KeyCode.Escape))
                HidePanel();
        }

        public void HidePanel() =>
            gameObject.SetActive(false);
    }
}