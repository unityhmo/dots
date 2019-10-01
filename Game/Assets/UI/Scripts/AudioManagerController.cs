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
    private float musicLevel;
    private float sfxLevel;

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

        LoadAudioLevelFromProps();
    }

    public void PlaySfx(AudioClip clip, float volume) {
        effectSource.volume = volume * sfxLevel;
        effectSource.PlayOneShot(clip, volume);
    }

    public void PlayMusic(AudioClip clip, float volume, bool repeat) {
        musicSource.clip = clip;
        musicSource.volume = volume * musicLevel;
        musicSource.loop = repeat;
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

    public void LoadAudioLevelFromProps() {
        musicLevel = PlayerSettingsController.GetFloat(PlayerSettingsController.Setting.Music,
            PlayerSettingsController.defaultValues[PlayerSettingsController.Setting.Music]);
        sfxLevel = PlayerSettingsController.GetFloat(PlayerSettingsController.Setting.Music,
            PlayerSettingsController.defaultValues[PlayerSettingsController.Setting.Music]);
        effectSource.volume = sfxLevel;
        musicSource.volume = musicLevel;
    }
}
