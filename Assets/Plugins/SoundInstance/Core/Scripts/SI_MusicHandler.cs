using System.Collections;
using Plugins.SoundInstance.Core.Resources;
using UnityEngine;

namespace Plugins.SoundInstance.Core.Scripts
{
    public class SI_MusicHandler : MonoBehaviour
    {
        bool isAllowed = false;
        private float _fade = 1f;

        MusicsStore Storage;

        public AudioSource audioSource
        {
            get { return GetComponent<AudioSource>(); }
        }

        private void Awake()
        {
            foreach (GameObject go in FindObjectsOfType<GameObject>())
            {
                if (go.name == "[SoundInstanceMusicLive]")
                {
                    Destroy(gameObject);
                    return;
                }
            }

            Setup();
        }

        void Start()
        {
        }

        void Update()
        {
            if (isAllowed && !audioSource.isPlaying)
            {
                StartMusic(GetRandomMusic().name, _fade);
            }
        }

        private void Setup()
        {
            Storage = (MusicsStore)UnityEngine.Resources.Load("SIMusicStore");
            gameObject.name = "[SoundInstanceMusicLive]";
            Static.SoundInstance.SetMusicHandler(this);
            DontDestroyOnLoad(this);
            isAllowed = true;
        }

        public void SetStartFade(float fade) =>
            _fade = fade;

        public void StartMusic(string name, float fadeSpeed)
        {
            if (audioSource.isPlaying)
            {
                StopAllCoroutines();
                StartCoroutine(Switch(name, fadeSpeed));
                return;
            }

            AudioClip audioClip = Storage.GetMusic(name).Song;
            audioSource.clip = audioClip;
            StartCoroutine(Play(1 / fadeSpeed));

            foreach (GameObject go in FindObjectsOfType<GameObject>())
            {
                Static.SoundInstance.CurrentMusic = Storage.GetMusic(name);
                go.SendMessage("OnMusicStarted", Storage.GetMusic(name), SendMessageOptions.DontRequireReceiver);
            }
        }

        public void StopMusic(float fadeSpeed)
        {
            StopAllCoroutines();
            StartCoroutine(Stop(fadeSpeed));
        }

        public void PauseMusic(float fadeSpeed)
        {
            StopAllCoroutines();
            StartCoroutine(Pause(fadeSpeed));
        }

        public void ResumeMusic(float fadeSpeed)
        {
            StopAllCoroutines();
            StartCoroutine(Resume(fadeSpeed));
        }

        private IEnumerator Play(float duration)
        {
            float currentTime = 0;
            float start = 0;

            audioSource.volume = 0;
            audioSource.Play();

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start,
                    Static.SoundInstance.musicVolume, currentTime / duration);
                yield return null;
            }

            yield break;
        }

        private IEnumerator Switch(string name, float fadeSpeed)
        {
            float duration = 1 / fadeSpeed;
            float currentTime = 0;
            float start = audioSource.volume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, 0, currentTime / duration);
                yield return null;
            }

            audioSource.Stop();

            foreach (GameObject go in FindObjectsOfType<GameObject>())
            {
                go.SendMessage("OnMusicStopped", SendMessageOptions.DontRequireReceiver);
            }

            AudioClip audioClip = Storage.GetMusic(name).Song;
            audioSource.clip = audioClip;

            Static.SoundInstance.CurrentMusic = Storage.GetMusic(name);
            foreach (GameObject go in FindObjectsOfType<GameObject>())
            {
                go.SendMessage("OnMusicStarted", Storage.GetMusic(name), SendMessageOptions.DontRequireReceiver);
            }

            StartCoroutine(Play(1 / fadeSpeed));
            yield break;
        }

        private IEnumerator Stop(float fadeSpeed)
        {
            float duration = 1 / fadeSpeed;
            float currentTime = 0;
            float start = audioSource.volume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, 0, currentTime / duration);
                yield return null;
            }

            audioSource.Stop();

            foreach (GameObject go in FindObjectsOfType<GameObject>())
            {
                go.SendMessage("OnMusicStopped", SendMessageOptions.DontRequireReceiver);
            }

            yield break;
        }

        private IEnumerator Pause(float fadeSpeed)
        {
            float duration = 1 / fadeSpeed;
            float currentTime = 0;
            float start = audioSource.volume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, 0, currentTime / duration);
                yield return null;
            }

            audioSource.Pause();

            foreach (GameObject go in FindObjectsOfType<GameObject>())
            {
                go.SendMessage("OnMusicPaused", SendMessageOptions.DontRequireReceiver);
            }

            yield break;
        }

        private IEnumerator Resume(float fadeSpeed)
        {
            float duration = 1 / fadeSpeed;
            float currentTime = 0;
            float start = audioSource.volume;

            foreach (GameObject go in FindObjectsOfType<GameObject>())
            {
                go.SendMessage("OnMusicResume", SendMessageOptions.DontRequireReceiver);
            }

            audioSource.UnPause();
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start,
                    Static.SoundInstance.musicVolume, currentTime / duration);
                yield return null;
            }

            yield break;
        }

        public Music GetNextMusic()
        {
            if (Static.SoundInstance.CurrentMusic == null)
            {
                return Storage.GetMusicByIndex(0);
            }

            if (!(Storage.GetMusicByIndex(
                      Storage.GetMusicIndex(Static.SoundInstance.CurrentMusic) +
                      1) ==
                  null))
            {
                return Storage.GetMusicByIndex(
                    Storage.GetMusicIndex(Static.SoundInstance.CurrentMusic) + 1);
            }
            else
            {
                return Storage.GetMusicByIndex(0);
            }
        }

        public Music GetRandomMusic() =>
            Storage.GetRandomMusic();
    }
}