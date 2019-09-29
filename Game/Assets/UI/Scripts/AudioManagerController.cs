using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private AudioListener audioListener;

    public static AudioManagerController Instance => instance;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (audioListener == null) {
            AudioListener existingAudioListener = FindObjectOfType<AudioListener>();

            if (existingAudioListener == null) {
                audioListener = gameObject.AddComponent<AudioListener>();
            } else {
                audioListener = existingAudioListener;
            }
        }
    }

    private void Awake() {
        Debug.Log("on awake");

        if (Instance == null) {
            instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

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