using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    private static readonly int ToBase = Animator.StringToHash("ToBase");
    private static readonly int ToConfig = Animator.StringToHash("ToConfig");
    private static readonly int ToHelp = Animator.StringToHash("ToHelp");
    private static readonly int ToPreGame = Animator.StringToHash("ToPreGame");
    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
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

    public void GoToPreGame() {
        animator.SetTrigger(ToPreGame);
    }

    public void GoToGame() {
        Invoke(nameof(ChangeToGameScene), 0.1f);
    }

    private void ChangeToGameScene() {
        SceneManager.LoadScene(sceneBuildIndex: 1, LoadSceneMode.Single);
    }
}