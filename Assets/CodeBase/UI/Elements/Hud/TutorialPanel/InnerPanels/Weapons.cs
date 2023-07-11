using CodeBase.Data;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels
{
    public class Weapons : IconsPanel, IProgressReader
    {
        [SerializeField] private TutorialPanel _panel;
        [SerializeField] private Action _glButton;
        [SerializeField] private Action _rpgButton;
        [SerializeField] private Action _rlButton;
        [SerializeField] private Action _mortarButton;
        [SerializeField] private Action _weaponsClick;

        private bool _showGL;
        private bool _showRPG;
        private bool _showRL;
        private bool _showMortar;

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

        public void LoadProgress(PlayerProgress progress) =>
            progress.WeaponsData.CurrentWeaponChanged += HideWeaponClick;

        private void HideWeaponClick() =>
            _panel.HidePanel();

        protected override void RuChosen() =>
            _weaponsClick.Text.text = LocalizationConstants.TutorialClickWeaponsIconRu;

        protected override void TrChosen() =>
            _weaponsClick.Text.text = LocalizationConstants.TutorialClickWeaponsIconTr;

        protected override void EnChosen() =>
            _weaponsClick.Text.text = LocalizationConstants.TutorialClickWeaponsIconEn;
    }
}