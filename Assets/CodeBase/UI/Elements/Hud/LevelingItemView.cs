using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud
{
    public abstract class LevelingItemView : MonoBehaviour
    {
        public Image MainTypeImage;
        public Image LevelTypeImage;

        protected IStaticDataService StaticDataService;
        protected LevelingItemData ItemData;
        protected ILeveling LevelingStaticData;

        private void Awake() =>
            StaticDataService = AllServices.Container.Single<IStaticDataService>();

        protected void Construct(LevelingItemData itemData)
        {
            ItemData = itemData;
            ItemData.LevelChanged += ChangeLevel;

            ChangeLevel();
        }

        protected void ChangeLevel()
        {
            if (LevelingStaticData == null || LevelingStaticData.ILevelTypeId == LevelTypeId.None)
            {
                MainTypeImage.ChangeImageAlpha(Constants.AlphaInactiveItem);
                LevelTypeImage.ChangeImageAlpha(Constants.AlphaInactiveItem);
                return;
            }

            if (LevelingStaticData.ILevelTypeId == LevelTypeId.Level_1)
                LevelTypeImage.ChangeImageAlpha(Constants.AlphaInactiveItem);
            else
                LevelTypeImage.ChangeImageAlpha(Constants.AlphaActiveItem);

            MainTypeImage.ChangeImageAlpha(Constants.AlphaActiveItem);
        }
    }
}