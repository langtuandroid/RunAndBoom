using CodeBase.Services.Audio;
using CodeBase.StaticData.Items;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class UpgradeShopItem : UpgradeItemBase
    {
        protected override void Clicked()
        {
            if (ShopItemBalance.IsMoneyEnough(_upgradeLevelInfoStaticData.Cost))
            {
                ShopItemBalance.ReduceMoney(_upgradeLevelInfoStaticData.Cost);
                Progress.WeaponsData.UpgradesData.LevelUp(_upgradableWeaponStaticData.WeaponTypeId,
                    _upgradeStaticData.UpgradeTypeId);

                switch (_upgradeLevelInfoStaticData.LevelTypeId)
                {
                    case LevelTypeId.Level_1:
                        SoundInstance.InstantiateOnTransform(
                            audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.UpgradeLevel_1),
                            transform: transform, Volume, AudioSource);
                        break;
                    case LevelTypeId.Level_2:
                        SoundInstance.InstantiateOnTransform(
                            audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.UpgradeLevel_2),
                            transform: transform, Volume, AudioSource);
                        break;
                    case LevelTypeId.Level_3:
                        SoundInstance.InstantiateOnTransform(
                            audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.UpgradeLevel_3),
                            transform: transform, Volume, AudioSource);
                        break;
                }

                ClearData();
            }
            else
            {
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.NotEnoughMoney),
                    transform: transform, Volume, AudioSource);
            }
        }
    }
}