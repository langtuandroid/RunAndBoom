using CodeBase.Services.PersistentProgress;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.Services.Audio
{
    public class AudioService : IAudioService
    {
        private readonly IPlayerProgressService _progressService;

        public AudioService(IPlayerProgressService progressService) =>
            _progressService = progressService;

        public void LaunchBlastSound(BlastSoundId blastSoundId, Transform transform, AudioSource audioSource = null)
        {
            if (_progressService.SettingsData.SoundOn == false)
                return;

            switch (blastSoundId)
            {
                case BlastSoundId.BlastGrenadeLauncher:
                    InstantiateOnPos(AudioClipAddresses.BlastGrenadeLauncher, transform.position,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case BlastSoundId.BlastRocketLauncherAndRpg:
                    InstantiateOnPos(AudioClipAddresses.BlastRocketLauncherAndRpg, transform.position,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case BlastSoundId.BlastMortar:
                    InstantiateOnPos(AudioClipAddresses.BlastMortar, transform.position,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
            }
        }

        public void LaunchDestructionSound(DestructionSoundId destructionSoundId, Transform transform,
            AudioSource audioSource = null)
        {
            if (_progressService.SettingsData.SoundOn == false)
                return;

            switch (destructionSoundId)
            {
                case DestructionSoundId.DestructionWoodenBox:
                    InstantiateOnTransform(AudioClipAddresses.DestructionWoodenBox, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case DestructionSoundId.DestructionConcrete:
                    InstantiateOnTransform(AudioClipAddresses.DestructionConcrete, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
            }
        }

        public void LaunchDoorSound(DoorSoundId doorSoundId, Transform transform, AudioSource audioSource = null)
        {
            if (_progressService.SettingsData.SoundOn == false)
                return;

            switch (doorSoundId)
            {
                case DoorSoundId.DoorOpening:
                    InstantiateOnTransform(AudioClipAddresses.DoorOpening, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case DoorSoundId.DoorClosing:
                    InstantiateOnTransform(AudioClipAddresses.DoorClosing, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
            }
        }

        public void LaunchGameEventSound(GameEventSoundId gameEventSoundId, Transform transform,
            AudioSource audioSource = null)
        {
            if (_progressService.SettingsData.SoundOn == false)
                return;

            switch (gameEventSoundId)
            {
                case GameEventSoundId.Death:
                    InstantiateOnTransform(AudioClipAddresses.Death, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case GameEventSoundId.GameWon:
                    InstantiateOnTransform(AudioClipAddresses.GameWon, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case GameEventSoundId.VictoryMusic:
                    InstantiateOnTransform(AudioClipAddresses.VictoryMusic, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
            }
        }

        public void LaunchShopSound(ShopSoundId shopSoundId, Transform transform, AudioSource audioSource = null)
        {
            if (_progressService.SettingsData.SoundOn == false)
                return;

            switch (shopSoundId)
            {
                case ShopSoundId.AmmoGotten:
                    InstantiateOnTransform(AudioClipAddresses.AmmoGotten, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShopSoundId.ItemGotten:
                    InstantiateOnTransform(AudioClipAddresses.ItemGotten, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShopSoundId.OneMoneyGotten:
                    InstantiateOnTransform(AudioClipAddresses.OneMoneyGotten, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShopSoundId.FewMoneyGotten:
                    InstantiateOnTransform(AudioClipAddresses.FewMoneyGotten, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShopSoundId.PouringMoneyGotten:
                    InstantiateOnTransform(AudioClipAddresses.PouringMoneyGotten, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShopSoundId.WeaponGotten:
                    InstantiateOnTransform(AudioClipAddresses.WeaponGotten, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShopSoundId.UpgradeLevel_1:
                    InstantiateOnTransform(AudioClipAddresses.UpgradeLevel_1, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShopSoundId.UpgradeLevel_2:
                    InstantiateOnTransform(AudioClipAddresses.UpgradeLevel_2, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShopSoundId.UpgradeLevel_3:
                    InstantiateOnTransform(AudioClipAddresses.UpgradeLevel_3, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShopSoundId.NotEnoughMoney:
                    InstantiateOnTransform(AudioClipAddresses.NotEnoughMoney, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShopSoundId.FullRecovery:
                    InstantiateOnTransform(AudioClipAddresses.FullRecovery, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
            }
        }

        public void LaunchShotSound(ShotSoundId shotSoundId, Transform transform, AudioSource audioSource = null)
        {
            if (_progressService.SettingsData.SoundOn == false)
                return;

            switch (shotSoundId)
            {
                case ShotSoundId.ShotPistol:
                    InstantiateOnTransform(AudioClipAddresses.ShotPistol, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShotSoundId.ShotShotgun:
                    InstantiateOnPos(AudioClipAddresses.ShotShotgun, transform.position,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShotSoundId.ShotSr:
                    InstantiateOnTransform(AudioClipAddresses.ShotSr, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShotSoundId.ShotSmg:
                    InstantiateOnTransform(AudioClipAddresses.ShotSmg, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShotSoundId.ShotMg:
                    InstantiateOnTransform(AudioClipAddresses.ShotMg, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShotSoundId.ShotGl:
                    InstantiateOnTransform(AudioClipAddresses.ShotGl, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShotSoundId.ShotRpg:
                    InstantiateOnTransform(AudioClipAddresses.ShotRpg, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShotSoundId.ShotRl:
                    InstantiateOnTransform(AudioClipAddresses.ShotRl, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case ShotSoundId.ShotMortar:
                    InstantiateOnTransform(AudioClipAddresses.ShotMortar, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
            }
        }

        public void LaunchUIActionSound(UIActionSoundId uiActionSoundId, Transform transform,
            AudioSource audioSource = null)
        {
            if (_progressService.SettingsData.SoundOn == false)
                return;

            switch (uiActionSoundId)
            {
                case UIActionSoundId.ButtonDeny:
                    InstantiateOnTransform(AudioClipAddresses.ButtonDeny, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case UIActionSoundId.ButtonSuccess:
                    InstantiateOnTransform(AudioClipAddresses.ButtonSuccess, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case UIActionSoundId.CheckboxClick:
                    InstantiateOnTransform(AudioClipAddresses.CheckboxClick, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case UIActionSoundId.MenuClose:
                    InstantiateOnTransform(AudioClipAddresses.MenuClose, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case UIActionSoundId.MenuOpen:
                    InstantiateOnTransform(AudioClipAddresses.MenuOpen, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
                case UIActionSoundId.Generate:
                    InstantiateOnTransform(AudioClipAddresses.Generate, transform,
                        _progressService.SettingsData.SoundVolume, audioSource);
                    break;
            }
        }

        private void InstantiateOnTransform(string name, Transform transform, float volume,
            AudioSource audioSource = null) =>
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(name),
                transform: transform, volume, audioSource);

        private void InstantiateOnPos(string name, Vector3 position, float volume, AudioSource audioSource = null) =>
            SoundInstance.InstantiateOnPos(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.BlastGrenadeLauncher),
                position: position, volume, audioSource);
    }
}