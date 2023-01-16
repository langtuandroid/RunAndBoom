using CodeBase.Data;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using Zenject;

namespace CodeBase.Level
{
    public class LevelFinishing : MonoBehaviour
    {
        private const string PlayerTag = "Player";

        [Inject] private IWindowService _windowService;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.CompareByTag(PlayerTag))
            {
                Time.timeScale = 0;
                _windowService.Open(WindowId.Finish);
            }
        }
    }
}