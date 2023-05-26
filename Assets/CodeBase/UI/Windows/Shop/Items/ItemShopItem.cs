using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class ItemShopItem : ItemItemBase
    {
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, ItemTypeId typeId, HeroHealth health, PlayerProgress progress)
        {
            _heroTransform = heroTransform;
            base.Construct(typeId, health, progress);
        }

        protected override void Clicked()
        {
            if (ShopItemBalance.IsMoneyEnough(_itemStaticData.Cost))
            {
                ShopItemBalance.ReduceMoney(_itemStaticData.Cost);

                if (_itemStaticData.TypeId == ItemTypeId.HealthRecover)
                {
                    Health.Recover();
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.FullRecovery),
                        transform: _heroTransform, Volume, AudioSource);
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