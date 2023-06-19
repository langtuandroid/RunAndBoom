using CodeBase.Data;
using CodeBase.Data.Weapons;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels
{
    public class Weapons : IconsPanel, IProgressReader
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
            _glButton.Show();
            _rpgButton.Show();
            _rlButton.Show();
            _mortarButton.Show();
            _weaponsClick.Hide();
        }

        public override void ShowForMobile()
        {
            _glButton.Hide();
            _rpgButton.Hide();
            _rlButton.Hide();
            _mortarButton.Hide();
            _weaponsClick.Show();
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

        protected override void RuChosen() =>
            _weaponsClick.Text.text = LocalizationConstants.TutorialClickWeaponsIconRu;

        protected override void TrChosen() =>
            _weaponsClick.Text.text = LocalizationConstants.TutorialClickWeaponsIconTr;

        protected override void EnChosen() =>
            _weaponsClick.Text.text = LocalizationConstants.TutorialClickWeaponsIconEn;
    }
}