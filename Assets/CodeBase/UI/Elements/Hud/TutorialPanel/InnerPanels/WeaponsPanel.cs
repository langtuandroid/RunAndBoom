using CodeBase.Data;
using CodeBase.Data.Weapons;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels
{
    public class WeaponsPanel : IconsPanel, IProgressReader
    {
        [SerializeField] private Action _glButton;
        [SerializeField] private Action _rpgButton;
        [SerializeField] private Action _rlButton;
        [SerializeField] private Action _mortarButton;
        [SerializeField] private Action _weaponsClick;
        [SerializeField] private Action _glClick;
        [SerializeField] private Action _rpgClick;
        [SerializeField] private Action _rlClick;
        [SerializeField] private Action _mortarClick;

        private WeaponsData _progressWeaponsData;

        public override void ShowForPc()
        {
            _glButton.gameObject.SetActive(true);
            _rpgButton.gameObject.SetActive(true);
            _rlButton.gameObject.SetActive(true);
            _mortarButton.gameObject.SetActive(true);
            _weaponsClick.gameObject.SetActive(false);
            _glClick.gameObject.SetActive(false);
            _rpgClick.gameObject.SetActive(false);
            _rlClick.gameObject.SetActive(false);
            _mortarClick.gameObject.SetActive(false);
        }

        public override void ShowForMobile()
        {
            _glButton.gameObject.SetActive(false);
            _rpgButton.gameObject.SetActive(false);
            _rlButton.gameObject.SetActive(false);
            _mortarButton.gameObject.SetActive(false);
            _weaponsClick.gameObject.SetActive(true);

            if (_progressWeaponsData.WeaponData.Find(x =>
                    x.WeaponTypeId == HeroWeaponTypeId.GrenadeLauncher && x.IsAvailable) !=
                null)
                _glClick.gameObject.SetActive(true);

            if (_progressWeaponsData.WeaponData.Find(x => x.WeaponTypeId == HeroWeaponTypeId.RPG && x.IsAvailable) !=
                null)
                _rpgClick.gameObject.SetActive(true);

            if (_progressWeaponsData.WeaponData.Find(x =>
                    x.WeaponTypeId == HeroWeaponTypeId.RocketLauncher && x.IsAvailable) !=
                null)
                _rlClick.gameObject.SetActive(true);

            if (_progressWeaponsData.WeaponData.Find(x => x.WeaponTypeId == HeroWeaponTypeId.Mortar && x.IsAvailable) !=
                null)
                _mortarClick.gameObject.SetActive(true);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _progressWeaponsData = progress.WeaponsData;
            _progressWeaponsData.CurrentWeaponChanged += HideWeaponClick;
        }

        private void HideWeaponClick()
        {
            switch (_progressWeaponsData.CurrentHeroWeaponTypeId)
            {
                case HeroWeaponTypeId.GrenadeLauncher:
                    _glClick.gameObject.SetActive(false);
                    break;
                case HeroWeaponTypeId.RPG:
                    _rpgClick.gameObject.SetActive(false);
                    break;
                case HeroWeaponTypeId.RocketLauncher:
                    _rlClick.gameObject.SetActive(false);
                    break;
                case HeroWeaponTypeId.Mortar:
                    _mortarClick.gameObject.SetActive(false);
                    break;
            }
        }
    }
}