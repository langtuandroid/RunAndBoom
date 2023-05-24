using CodeBase.Data;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class WeaponGiftItem : WeaponItemBase
    {
        private GiftsGenerator _generator;
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, HeroWeaponTypeId weaponTypeId, PlayerProgress progress,
            GiftsGenerator generator)
        {
            _heroTransform = heroTransform;
            _generator = generator;
            base.Construct(weaponTypeId, progress);
        }

        protected override void Clicked()
        {
            Progress.WeaponsData.SetAvailableWeapon(_weaponTypeId);
            ClearData();
            _generator.Clicked();
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.WeaponGotten),
                transform: _heroTransform, Volume, AudioSource);
        }
    }
}