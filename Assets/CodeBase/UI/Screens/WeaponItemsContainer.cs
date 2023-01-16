using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Weapon;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Screens
{
    public abstract class WeaponItemsContainer : MonoBehaviour
    {
        public Transform Parent;

        [Inject] protected IPlayerProgressService ProgressService
            // =
            // AllServices.Container.Single<IPersistentProgressService>()
            ;

        [Inject] protected IStaticDataService StaticData
            // = AllServices.Container.Single<IStaticDataService>()
            ;

        [Inject] protected IUIFactory UIFactory
            // = AllServices.Container.Single<IUIFactory>()
            ;
        // protected static List<GameObject> WeaponItemGameObjects = new List<GameObject>();

        // protected void Construct(IPersistentProgressService progressService, IStaticDataService staticData,
        //     IUIFactory uiFactory)
        // {
        //     ProgressService = progressService;
        //     StaticData = staticData;
        //     UIFactory = uiFactory;
        //     // Parent = parent;
        // }

        protected void Initialize()
        {
        }

        public abstract void OnItemClick(WeaponTypeId typeId);
    }
}