using UnityEngine;

namespace CodeBase.Services.Audio
{
    public static class AudioUtils
    {
        public static void PlaySound(AudioSource stop, AudioSource play)
        {
            stop.Stop();
            play.Play();
        }
    }
}