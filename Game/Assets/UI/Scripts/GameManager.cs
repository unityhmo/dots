using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    // Ideally, we will have a different scene for every state. Otherwise, consider changing this for a switch instead
    public enum State {
        Splash,
        LanguageSelection,
        MainMenu,
        Game
    }

    private readonly Dictionary<State, int> scene = new Dictionary<State, int>() {
        {State.Splash, 0},
        {State.LanguageSelection, 1},
        {State.MainMenu, 2},
        {State.Game, 3}
    };

    [SerializeField] private float fadeDelay = 0.3f;
    private static GameManager instance;
    private Animator curtainAnimator;
    private State currentState;
    private static readonly int IsBlack = Animator.StringToHash("IsBlack");

    public State CurrentState => currentState;
    public static GameManager Instance => instance;

    private void Awake() {
        if (Instance == null) {
            instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        GameObject curtain = GameObject.FindWithTag("Curtain");

        if (curtain == null) {
            throw new ArgumentException("No `Curtain` object found");
        }

        curtainAnimator = curtain.GetComponent<Animator>();

        if (curtainAnimator == null) {
            throw new ArgumentException("No `Curtain` object found");
        }
    }

    public void SceneLoaded() {
        curtainAnimator.SetBool(IsBlack, false);
    }

    public void TransitionTo(State newState) {
        currentState = newState;
        curtainAnimator.SetBool(IsBlack, true);
        Invoke(nameof(TransitionToCurrentScene), fadeDelay);
    }

    private void TransitionToCurrentScene() {
        SceneManager.LoadScene(scene[currentState], LoadSceneMode.Single);
    }
}