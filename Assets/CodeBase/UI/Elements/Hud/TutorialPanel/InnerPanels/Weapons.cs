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

        private bool _showGL;
        private bool _showRPG;
        private bool _showRL;
        private bool _showMortar;
        private WeaponsData _progressWeaponsData;

        private void Update()
        {
            if (!_glButton.IsVisible() && !_rpgButton.IsVisible() && !_rlButton.IsVisible() &&
                !_mortarButton.IsVisible())
                Hide();
        }

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
            if (_weaponsClick.IsVisible())
                _weaponsClick.Hide();

            switch (_progressWeaponsData.CurrentHeroWeaponTypeId)
            {
                case HeroWeaponTypeId.GrenadeLauncher:
                    if (_glButton.IsVisible())
                        _glButton.Hide();
                    break;
                case HeroWeaponTypeId.RPG:
                    if (_rpgButton.IsVisible())
                        _rpgButton.Hide();
                    break;
                case HeroWeaponTypeId.RocketLauncher:
                    if (_rlButton.IsVisible())
                        _rlButton.Hide();
                    break;
                case HeroWeaponTypeId.Mortar:
                    if (_mortarButton.IsVisible())
                        _mortarButton.Hide();
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