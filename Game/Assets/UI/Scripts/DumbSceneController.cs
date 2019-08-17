using UnityEngine;

public class DumbSceneController : MonoBehaviour {
    [SerializeField] private GameManager.State nextStep = GameManager.State.MainMenu;
    [SerializeField] private float delayBeforeNextStep = 3f;
    [SerializeField] private bool isSkippable = true;

    private GameManager gameManager;

    void Start() {
        gameManager = GameManager.Instance;
        gameManager.SceneLoaded();
        Invoke(nameof(TransitionToNextStep), delayBeforeNextStep);
    }

    void TransitionToNextStep() {
        gameManager.TransitionTo(nextStep);
    }

    void Update() {
        if (isSkippable && (Input.touchCount > 0 || Input.anyKeyDown)) {
            CancelInvoke(nameof(TransitionToNextStep));
            TransitionToNextStep();
        }
    }
}