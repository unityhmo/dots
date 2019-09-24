using System;
using UnityEngine;

public class PlaySfx : MonoBehaviour {
    [SerializeField] protected AudioManagerController.EventType playWhen = AudioManagerController.EventType.Other;
    [SerializeField] [Range(0, 1)] protected float volume = 1f;
    [SerializeField] protected AudioClip audioClip;

    protected AudioManagerController audioManagerController;

    void Start() {
        GameObject soundManager = GameObject.FindWithTag("AudioManager");

        if (audioClip == null) {
            throw new ArgumentException("No `AudioClip` added to script");
        }

        if (soundManager == null) {
            throw new ArgumentException("No `AudioManager` object found");
        }

        audioManagerController = soundManager.GetComponent<AudioManagerController>();

        if (audioManagerController == null) {
            throw new ArgumentException("No `AudioManager` object found");
        }

        if (playWhen == AudioManagerController.EventType.StartScript) {
            Play();
        }
    }

    void OnDestroy() {
        if (playWhen == AudioManagerController.EventType.BeforeDestroy) {
            Play();
        }
    }

    public virtual void Play() {
        audioManagerController.PlayMusic(audioClip, volume);
    }
}