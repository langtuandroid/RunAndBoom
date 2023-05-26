using CodeBase.Data;
using CodeBase.Data.Upgrades;
using CodeBase.StaticData.Items;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class UpgradeGiftItem : UpgradeItemBase
    {
        private GiftsGenerator _generator;
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, UpgradeItemData upgradeItemData, PlayerProgress progress,
            GiftsGenerator generator)
        {
            _heroTransform = heroTransform;
            _generator = generator;
            base.Construct(upgradeItemData, progress);
        }

        protected override void Clicked()
        {
            Progress.WeaponsData.UpgradesData.LevelUp(_upgradableWeaponStaticData.WeaponTypeId,
                _upgradeStaticData.UpgradeTypeId);
            ClearData();
            _generator.Clicked();

            switch (_upgradeLevelInfoStaticData.LevelTypeId)
            {
                case LevelTypeId.None:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.UpgradeLevel_1),
                        transform: _heroTransform, Volume, AudioSource);
                    break;
                case LevelTypeId.Level_1:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.UpgradeLevel_2),
                        transform: _heroTransform, Volume, AudioSource);
                    break;
                case LevelTypeId.Level_2:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.UpgradeLevel_3),
                        transform: _heroTransform, Volume, AudioSource);
                    break;
            }
        }
    }
}