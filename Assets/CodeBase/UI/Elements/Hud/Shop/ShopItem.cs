using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud.Shop
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private Image _main;
        [SerializeField] private Image _level;
        [SerializeField] private Image _additional;
        [SerializeField] private TextMeshProUGUI _cost;
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private TextMeshProUGUI _title;

        protected IStaticDataService StaticDataService;

        private void Awake() =>
            StaticDataService = AllServices.Container.Single<IStaticDataService>();

        protected void Construct(LevelingItemData itemData)
        {
            
        }
    }
}