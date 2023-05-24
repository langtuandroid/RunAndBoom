using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Services.Audio
{
    public class AudioService : IAudioService
    {
        private const string File = "file://";

        private readonly float _soundFxVolume;
        private readonly float _musicVolume;
        private float _lowPitchRange = .95f;
        private float _highPitchRange = 1.05f;
        private string _audioPath;

        private Dictionary<string, AudioClip> _audioClips;
        private AudioClip _audioClip;

        public AudioService(float soundFxVolume, float musicVolume)
        {
            _soundFxVolume = soundFxVolume;
            _musicVolume = musicVolume;
        }

        public void Initialize()
        {
            // StringBuilder stringBuilder = new StringBuilder();
            // stringBuilder.Append(File);
            // stringBuilder.Append(Application.streamingAssetsPath);
            // stringBuilder.Append(Application.streamingAssetsPath);
            // _audioPath = stringBuilder.ToString();

            // _audioClips[AudioClipAddresses.DoorOpening] = (AudioClip)Resources.Load(AudioClipAddresses.DoorOpening);
            // _audioClips[AudioClipAddresses.DoorClosing] = (AudioClip)Resources.Load(AudioClipAddresses.DoorClosing);
        }

        private IEnumerator LoadAudio(string path, string name)
        {
            WWW request = GetAudioFromFile(path, name);
            yield return request;

            AudioClip audioClip = request.GetAudioClip();
            audioClip.name = name;

            PlayAudioFile(audioClip);
        }

        private void PlayAudioFile(AudioClip audioClip)
        {
            throw new NotImplementedException();
        }

        private WWW GetAudioFromFile(string path, string name)
        {
            string audioToLoad = string.Format(path + "{0}", name);
            WWW request = new WWW(audioToLoad);
            return request;
        }

        public AudioClip GetSoundEffect(string address) =>
            _audioClips[address];

        public void PlaySoundEffect(AudioSource audioSource)
        {
            if (audioSource == null)
                return;

            audioSource.loop = false;
            audioSource.volume = _soundFxVolume;
            audioSource.playOnAwake = false;
            audioSource.Play();
        }

        public void PlayMusic(AudioSource musicSource)
        {
            if (musicSource == null)
                return;

            musicSource.loop = true;
            musicSource.volume = _musicVolume;
            musicSource.playOnAwake = false;
            musicSource.Play();
        }

        public void ChangeSoundFxVolume(AudioSource _soundSource) =>
            _soundSource.volume = _soundFxVolume;

        public void ChangeMusicVolume(AudioSource _musicSource) =>
            _musicSource.volume = _musicVolume;
    }
}