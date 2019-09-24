using UnityEngine;

public class PlayMusic : PlaySfx {
    [SerializeField] private bool repeat;

    public override void Play() {
        soundManagerController.PlaySfx(audioClip, volume);
    }

    void OnDestroy() {
        if (playWhen == SoundManagerController.EventType.BeforeDestroy) {
            Play();
        }
    }
}