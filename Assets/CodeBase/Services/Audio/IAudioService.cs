using UnityEngine;

namespace CodeBase.Services.Audio
{
    public interface IAudioService : IService
    {
        void Initialize();
        AudioClip GetSoundEffect(string address);
        void PlaySoundEffect(AudioSource audioSource);
        void PlayMusic(AudioSource musicSource);
        void ChangeSoundFxVolume(AudioSource _soundSource);
        void ChangeMusicVolume(AudioSource _musicSource);
    }
}