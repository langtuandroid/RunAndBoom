using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        protected IPlayerProgressService ProgressService;
        protected PlayerProgress Progress => ProgressService.Progress;
        protected string CurrentError => ProgressService.CurrentError;

        public void Construct(IPlayerProgressService progressService) =>
            ProgressService = progressService;

        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy()
        {
            CleanUp();
        }

        protected virtual void OnAwake() =>
            _closeButton.onClick.AddListener(() => Destroy(gameObject));

        protected virtual void Initialize()
        {
        }

        protected virtual void SubscribeUpdates()
        {
        }

        protected virtual void CleanUp()
        {
        }
    }
}