using System.Collections.Generic;
using UnityEngine;

public class AudioManagerController : MonoBehaviour {
    public enum EventType {
        StartScript,
        BeforeDestroy,
        Other
    }

    private static AudioManagerController instance;
    private AudioSource effectSource;
    private AudioSource musicSource;
    private float savedVolume = 0f;

    public static AudioManagerController Instance => instance;

    private void Awake() {
        if (Instance == null) {
            instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        if (musicSource == null || effectSource == null) {
            effectSource = gameObject.AddComponent<AudioSource>();
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.ignoreListenerPause = true;
        }
    }

    public void PlaySfx(AudioClip clip, float volume) {
        // TODO multiply by global volume
        effectSource.PlayOneShot(clip, volume);
    }

    public void PlayMusic(AudioClip clip, float volume) {
        // TODO multiply by global volume
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void TogglePauseSfx(bool pause) {
        if (pause) {
            AudioListener.pause = true;
            savedVolume = musicSource.volume;
            musicSource.volume /= 2;
        } else {
            AudioListener.pause = false;
            musicSource.volume = savedVolume;
        }
    }
}