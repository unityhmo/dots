using UnityEngine;

public class PlayMusic : PlaySfx {
    [SerializeField] private bool repeat;

    public override void Play() {
        audioManagerController.PlayMusic(audioClip, volume, repeat);
    }

    void OnDestroy() {
        if (playWhen == AudioManagerController.EventType.BeforeDestroy) {
            Play();
        }
    }
}