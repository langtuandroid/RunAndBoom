using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using UnityEngine;

namespace CodeBase.UI.Windows.Training
{
    public class TrainingWindow : WindowBase
    {
        private void Update()
        {
            if (Input.anyKeyDown)
                Hide();
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.Training);
    }
}