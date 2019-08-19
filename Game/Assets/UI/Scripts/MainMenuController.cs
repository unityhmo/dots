using UnityEngine;

public class MainMenuController : MonoBehaviour {
    private static readonly int ToBase = Animator.StringToHash("ToBase");
    private static readonly int ToConfig = Animator.StringToHash("ToConfig");
    private static readonly int ToHelp = Animator.StringToHash("ToHelp");
    private static readonly int ToPreGame = Animator.StringToHash("ToPreGame");
    private static readonly int IsCredits = Animator.StringToHash("IsCredits");

    private Animator animator;
    private GameManager gameManager;

    void Start() {
        gameManager = GameManager.Instance;
        animator = GetComponent<Animator>();

        gameManager.SceneLoaded();
    }

    public void GoToBase() {
        animator.SetTrigger(ToBase);
    }

    public void GoToConfig() {
        animator.SetTrigger(ToConfig);
    }

    public void GoToHelp() {
        animator.SetTrigger(ToHelp);
    }

    public void GoToCredits(bool isCredits) {
        animator.SetBool(IsCredits, isCredits);
    }

    public void GoToPreGame() {
        animator.SetTrigger(ToPreGame);
    }

    public void GoToGame() {
        Invoke(nameof(ChangeToGameScene), 0.1f);
    }

    private void ChangeToGameScene() {
        gameManager.TransitionTo(GameManager.State.Game);
    }
}