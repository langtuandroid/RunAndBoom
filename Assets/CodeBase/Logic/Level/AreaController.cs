using CodeBase.StaticData.Enemies;
using UnityEngine;

namespace CodeBase.Logic.Level
{
    public class AreaController : MonoBehaviour
    {
        [SerializeField] private AreaTypeId _areaTypeId;
        [SerializeField] private AreaEnemiesContainer _areaEnemiesContainer;
        [SerializeField] private AreaClearChecker _areaClearChecker;

        public AreaEnemiesContainer AreaEnemiesContainer => _areaEnemiesContainer;
        public AreaClearChecker AreaClearChecker => _areaClearChecker;

        public void Initialize()
        {
            _areaEnemiesContainer = GetComponent<AreaEnemiesContainer>();
            _areaClearChecker = GetComponent<AreaClearChecker>();
            _areaEnemiesContainer.Initialize(_areaTypeId, this);
            _areaClearChecker.Initialize(_areaTypeId);
        }
    }
}