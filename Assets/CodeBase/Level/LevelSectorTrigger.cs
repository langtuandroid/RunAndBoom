using CodeBase.Data;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using Zenject;

namespace CodeBase.Level
{
    public class LevelSectorTrigger : MonoBehaviour
    {
        [SerializeField] private string _name;

        private const string HeroTag = "Hero";

        [Inject] private IWindowService _windowService;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.CompareByTag(HeroTag))
            {
                Time.timeScale = 0;
                _windowService.Open(WindowId.Finish);
            }
        }
    }
}