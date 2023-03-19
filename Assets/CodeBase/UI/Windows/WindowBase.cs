using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class WindowBase : MonoBehaviour
    {
        private void Awake() =>
            Hide();

        public void Hide() =>
            gameObject.SetActive(false);

        public void Show() =>
            gameObject.SetActive(true);

        // [SerializeField] private Button _closeButton;
        //
        // protected IPlayerProgressService ProgressService;
        // protected PlayerProgress Progress => ProgressService.Progress;
        // protected string CurrentError => ProgressService.CurrentError;
        //
        // public void Construct(IPlayerProgressService progressService) =>
        //     ProgressService = progressService;
        //
        // private void Awake()
        // {
        //     OnAwake();
        // }
        //
        // private void Start()
        // {
        //     Initialize();
        //     SubscribeUpdates();
        // }
        //
        // private void OnDestroy()
        // {
        //     CleanUp();
        // }
        //
        // protected virtual void OnAwake() =>
        //     _closeButton.onClick.AddListener(() => Destroy(gameObject));
        //
        // protected virtual void Initialize()
        // {
        // }
        //
        // protected virtual void SubscribeUpdates()
        // {
        // }
        //
        // protected virtual void CleanUp()
        // {
        // }
    }
}