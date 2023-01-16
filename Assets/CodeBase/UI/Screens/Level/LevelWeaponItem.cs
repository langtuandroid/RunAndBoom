using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Weapon;
using CodeBase.UI.Services;
using CodeBase.Weapons;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Screens.Level
{
    public class LevelWeaponItem : WeaponItem
    {
        [SerializeField] private TextMeshProUGUI _mainFireShotsCountText;
        // [SerializeField] private TextMeshProUGUI _secondaryFireCountText;

        private LevelStats _levelStats => ProgressService.Progress.CurrentLevelStats;

        public new void Construct(IPlayerProgressService progressService, GameObject weaponItemsContainerGameObject,
            Sprite sprite, WeaponTypeId weaponTypeId, WeaponArmoryDescription description)
        {
            base.Construct(progressService, weaponItemsContainerGameObject, sprite, weaponTypeId, description);
        }

        public new void Initialize()
        {
            base.Initialize();
        }

        public void Subscribe()
        {
            _levelStats.ScoreData.ScoreChanged += EvaluateShotsCount;
        }

        public void Unsubscribe()
        {
            _levelStats.ScoreData.ScoreChanged -= EvaluateShotsCount;
        }

        private void EvaluateShotsCount()
        {
            int shotsCount = _levelStats.ScoreData.Score / Description.MainFireCost;
            _mainFireShotsCountText.text = shotsCount.ToString();

            ChangeItemActivity(shotsCount);
        }

        private void ChangeItemActivity(int shotsCount)
        {
            if (shotsCount == 0)
            {
                ClickItemButton.enabled = false;
                Icon.ChangeImageAlpha(Constants.AlphaUnselectedItem);
            }
            else
            {
                ClickItemButton.enabled = true;
                Icon.ChangeImageAlpha(Constants.AlphaSelectedItem);
            }
        }

        // TODO(Finish method!)
        protected override void OnClick()
        {
        }
    }
}