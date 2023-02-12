using UnityEngine;

namespace _Darkland.Sources.Scripts.Audio {

    public class AudioRootManager : MonoBehaviour {

        public AudioSource ambient;
        public AudioSource music;

        public void Toggle(AudioSource audioSource) {
            if (audioSource.isPlaying) {
                audioSource.Stop();
            }
            else {
                audioSource.Play();
            }
        }

    }

}