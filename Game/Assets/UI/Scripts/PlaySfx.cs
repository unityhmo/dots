using System;
using UnityEngine;

public class PlaySfx : MonoBehaviour {
    [SerializeField] protected SoundManagerController.EventType playWhen = SoundManagerController.EventType.Other;
    [SerializeField] [Range(0, 1)] protected float volume = 1f;
    [SerializeField] protected AudioClip audioClip;

    protected SoundManagerController soundManagerController;

    void Start() {
        GameObject soundManager = GameObject.FindWithTag("SoundManager");

        if (audioClip == null) {
            throw new ArgumentException("No `AudioClip` added to script");
        }

        if (soundManager == null) {
            throw new ArgumentException("No `SoundManager` object found");
        }

        soundManagerController = soundManager.GetComponent<SoundManagerController>();

        if (soundManagerController == null) {
            throw new ArgumentException("No `SoundManager` object found");
        }

        if (playWhen == SoundManagerController.EventType.StartScript) {
            Play();
        }
    }

    void OnDestroy() {
        if (playWhen == SoundManagerController.EventType.BeforeDestroy) {
            Play();
        }
    }

    public virtual void Play() {
        soundManagerController.PlayMusic(audioClip, volume);
    }
}