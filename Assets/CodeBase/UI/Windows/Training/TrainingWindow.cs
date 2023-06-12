using CodeBase.UI.Elements.Hud.WeaponsPanel;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using UnityEngine;

namespace CodeBase.UI.Windows.Training
{
    public class TrainingWindow : WindowBase
    {
        private WeaponsVisibility _weaponsVisibility;

        private void Update() =>
            CheckClick();

        private void CheckClick()
        {
            if (Input.anyKeyDown)
                ActivateHud();

            foreach (Touch touch in Input.touches)
                if (touch.phase == TouchPhase.Began)
                    ActivateHud();
        }

        private void ActivateHud()
        {
            Hide();
            _weaponsVisibility.ShowAvailable();
        }

        public void Construct(GameObject hero, WeaponsVisibility weaponsVisibility)
        {
            _weaponsVisibility = weaponsVisibility;
            base.Construct(hero, WindowId.Training);
        }

        public void ShowAllWeaponCells() =>
            _weaponsVisibility.ShowAll();
    }
}