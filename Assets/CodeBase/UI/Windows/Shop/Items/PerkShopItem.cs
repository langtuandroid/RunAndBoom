using CodeBase.Services.Audio;
using CodeBase.StaticData.Items;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class PerkShopItem : PerkItemBase
    {
        protected override void Clicked()
        {
            if (ShopItemBalance.IsMoneyEnough(_perkStaticData.Cost))
            {
                ShopItemBalance.ReduceMoney(_perkStaticData.Cost);
                Progress.PerksData.LevelUp(_perkStaticData.PerkTypeId);
                ClearData();

                switch (_perkStaticData.ILevelTypeId)
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