using CodeBase.Services;
using CodeBase.Services.SaveLoad;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic
{
    public class FinishTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private TextMeshPro _scoreText;

        private ISaveLoadService _saveLoadService;

        private void Awake() =>
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Level finished.");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (!_collider)
                return;

            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }
    }
}