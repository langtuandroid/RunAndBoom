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

       protected IPlayerProgressService ProgressService;
        protected IStaticDataService StaticData;
         protected IUIFactory UIFactory;
        // protected static List<GameObject> WeaponItemGameObjects = new List<GameObject>();

        protected void Construct(IPlayerProgressService progressService, IStaticDataService staticData, IUIFactory uiFactory)
        {
            ProgressService = progressService;
            StaticData = staticData;
            UIFactory = uiFactory;
            // Parent = parent;
        }

        protected void Initialize()
        {
        }

        public abstract void OnItemClick(WeaponTypeId typeId);
    }
}