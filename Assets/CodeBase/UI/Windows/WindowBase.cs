using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class WindowBase : MonoBehaviour
    {
        private GameObject _hero;

        protected void Construct(GameObject hero)
        {
            _hero = hero;
            Hide();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _hero.GetComponent<HeroShooting>().TurnOn();
            _hero.GetComponent<EnemiesChecker>().TurnOn();
            _hero.GetComponentInChildren<HeroWeaponSelection>().TurnOn();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _hero.GetComponent<HeroShooting>().TurnOff();
            _hero.GetComponent<EnemiesChecker>().TurnOff();
            _hero.GetComponent<HeroRotating>().TurnOff();
            _hero.GetComponent<HeroLookAt>().TurnOff();
            _hero.GetComponentInChildren<HeroWeaponSelection>().TurnOff();
        }

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