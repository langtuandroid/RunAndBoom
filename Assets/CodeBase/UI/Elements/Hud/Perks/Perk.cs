using CodeBase.Data.Perks;
using CodeBase.Services;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud.Perks
{
    public class Perk : MonoBehaviour
    {
        [SerializeField] private Image _perkType;
        [SerializeField] private Image _levelType;

        private PerkData _perkData;
        private IStaticDataService _staticDataService;
        private PerkStaticData _perkStaticData;

        private void Awake() =>
            _staticDataService = AllServices.Container.Single<IStaticDataService>();

        public void Construct(PerkData perkData)
        {
            _perkData = perkData;
            _perkData.LevelChanged += ChangeLevel;

            ChangeLevel();
        }

        private void ChangeLevel()
        {
            _perkStaticData = _staticDataService.ForPerk(_perkData.PerkTypeId, _perkData.LevelTypeId);

            if (_perkStaticData == null || _perkStaticData.LevelTypeId == LevelTypeId.None)
            {
                _perkType.ChangeImageAlpha(Constants.AlphaInvisibleItem);
                _levelType.ChangeImageAlpha(Constants.AlphaInvisibleItem);
                return;
            }

            if (_perkStaticData.LevelTypeId == LevelTypeId.Level_1)
                _levelType.ChangeImageAlpha(Constants.AlphaInvisibleItem);
            else
                _levelType.ChangeImageAlpha(Constants.AlphaSelectedItem);

            _perkType.ChangeImageAlpha(Constants.AlphaSelectedItem);

            _perkType.sprite = _perkStaticData.MainImage;
            _levelType.sprite = _perkStaticData.LevelImage;
        }
    }
}