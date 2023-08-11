using UnityEngine;

namespace DefaultNamespace {
    public class AudioUtils {
        public static void PlaySoundInterval(AudioSource audioSource, float fromSeconds, float toSeconds) {
            audioSource.time = fromSeconds;
            audioSource.Play();
            audioSource.SetScheduledEndTime(AudioSettings.dspTime + (toSeconds - fromSeconds));
        }
    }
}