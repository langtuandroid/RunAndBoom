using CodeBase.Data.Progress;
using CodeBase.Data.Progress.Upgrades;
using CodeBase.StaticData.Items;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class UpgradeShopItem : UpgradeItemBase
    {
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, UpgradeItemData upgradeItemData, ProgressData progressData)
        {
            _heroTransform = heroTransform;
            base.Construct(upgradeItemData, progressData);
        }

        protected override void Clicked()
        {
            if (ShopItemBalance.IsMoneyEnough(_upgradeLevelInfoStaticData.Cost))
            {
                ShopItemBalance.ReduceMoney(_upgradeLevelInfoStaticData.Cost);
                ProgressData.WeaponsData.UpgradesData.LevelUp(_upgradableWeaponStaticData.WeaponTypeId,
                    _upgradeStaticData.UpgradeTypeId);

                switch (_upgradeLevelInfoStaticData.LevelTypeId)
                {
                    case LevelTypeId.Level_1:
                        SoundInstance.InstantiateOnTransform(
                            audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.UpgradeLevel_1),
                            transform: _heroTransform, Volume, AudioSource);
                        break;
                    case LevelTypeId.Level_2:
                        SoundInstance.InstantiateOnTransform(
                            audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.UpgradeLevel_2),
                            transform: _heroTransform, Volume, AudioSource);
                        break;
                    case LevelTypeId.Level_3:
                        SoundInstance.InstantiateOnTransform(
                            audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.UpgradeLevel_3),
                            transform: _heroTransform, Volume, AudioSource);
                        break;
                }

                ClearData();
            }
            else
            {
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.NotEnoughMoney),
                    transform: _heroTransform, Volume, AudioSource);
            }
        }
    }
}