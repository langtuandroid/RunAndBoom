using UnityEngine;

namespace CodeBase.Services.Audio
{
    public interface IAudioService : IService
    {
        void LaunchBlastSound(BlastSoundId blastSoundId, Transform transform, AudioSource audioSource = null);

        void LaunchDestructionSound(DestructionSoundId destructionSoundId, Transform transform,
            AudioSource audioSource = null);

        void LaunchDoorSound(DoorSoundId doorSoundId, Transform transform, AudioSource audioSource = null);

        void LaunchGameEventSound(GameEventSoundId gameEventSoundId, Transform transform,
            AudioSource audioSource = null);

        void LaunchShopSound(ShopSoundId shopSoundId, Transform transform, AudioSource audioSource = null);
        void LaunchShotSound(ShotSoundId shotSoundId, Transform transform, AudioSource audioSource = null);
        void LaunchUIActionSound(UIActionSoundId uiActionSoundId, Transform transform, AudioSource audioSource = null);
    }
}