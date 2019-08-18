using UnityEngine;

public class GameUIController : MonoBehaviour {
    private GameManager gameManager;

    void Start() {
        gameManager = GameManager.Instance;
        gameManager.SceneLoaded();
    }

    public void GoBackToMenu() {
        gameManager.TransitionTo(GameManager.State.MainMenu);
    }
}