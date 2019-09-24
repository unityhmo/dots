using UnityEngine;

public class PlayMusic : PlaySfx {
    [SerializeField] private bool repeat;

    public override void Play() {
        audioManagerController.PlaySfx(audioClip, volume);
    }

    void OnDestroy() {
        if (playWhen == AudioManagerController.EventType.BeforeDestroy) {
            Play();
        }
    }
}