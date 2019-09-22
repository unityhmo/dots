using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

static class SceneLoader
{
  public const int TRANSITIONER_SORTING_ORDER = 1;
  public const float FADE_IN_DURATION = 0.4f;
  public const float FADE_OUT_DURATION = 0.4f;
  private static GameObject _loadingScreen;
  private static GameObject _transitioner;

  public static void LoadScene(int sceneIndex, Dictionary<string, object> optional = null)
  {
    if (_transitioner != null)
    {
      return;
    }

    SceneTransition newSceneTransition = new GameObject().AddComponent<SceneTransition>();

    float fadeInDuration = FADE_IN_DURATION;
    float fadeOutDuration = FADE_OUT_DURATION;

    if (optional != null)
    {
      if (optional.ContainsKey("useLoadingScreen"))
      {
        newSceneTransition.UseResourceImage = (bool)optional["useLoadingScreen"];
      }

      if (optional.ContainsKey("color"))
      {
        newSceneTransition.SetColor((Color)optional["color"]);
      }

      if (optional.ContainsKey("fadeInDuration"))
      {
        fadeInDuration = (float)optional["fadeInDuration"];
      }

      if (optional.ContainsKey("fadeOutDuration"))
      {
        fadeOutDuration = (float)optional["fadeOutDuration"];
      }
    }

    newSceneTransition.Begin(sceneIndex, fadeInDuration, fadeOutDuration);

    _transitioner = newSceneTransition.gameObject;
  }

  public static void LoadSceneCompleted()
  {
    _transitioner = null;
  }

  public static GameObject GetLoadingScreen()
  {
    if (_loadingScreen == null)
    {
      _loadingScreen = Resources.Load<GameObject>("ui/loading_screen");
    }

    return _loadingScreen;
  }
}

public class SceneTransition : MonoBehaviour
{
  private enum TransitionState
  {
    In,
    Hold,
    Out
  }

  public bool UseResourceImage = false;
  private bool _initialized = false;
  private int _sceneIndex;
  private float _durationFadeIn;
  private float _durationFadeOut;
  private float _timer;
  private Vector2 _screenSize;
  private TransitionState _state;
  private Color _fadeColor = Color.black;
  private Image _fader;
  private CanvasGroup _canvas;

  public void Begin(int sceneIndex, float durationFadeIn, float durationFadeOut)
  {
    DontDestroyOnLoad(gameObject);

    SceneManager.sceneLoaded += OnSceneLoaded;

    gameObject.name = "Transitioner";

    _screenSize = new Vector2(Screen.width, Screen.height);

    _sceneIndex = sceneIndex;
    _durationFadeIn = durationFadeIn;
    _durationFadeOut = durationFadeOut;
    _fadeColor.a = 0;

    CreateCanvas();

    ChangeState(TransitionState.In);
    _initialized = true;
  }

  // In case we want to swap add personalized colors to fader
  public SceneTransition SetColor(Color color)
  {
    _fadeColor = color;

    return this;
  }

  private void Update()
  {
    if (!_initialized || _state == TransitionState.Hold)
    {
      return;
    }

    _timer += Time.deltaTime;

    if (_state == TransitionState.In)
    {
      _fadeColor.a = Mathf.Lerp(0, 1, _timer / _durationFadeIn);
      if (_timer >= _durationFadeIn)
      {
        _fadeColor.a = 1;
        ChangeState(TransitionState.Hold);
        SceneManager.LoadScene(_sceneIndex);
      }
    }
    else if (_state == TransitionState.Out)
    {
      _fadeColor.a = Mathf.Lerp(1, 0, _timer / _durationFadeOut);
      if (_timer >= _durationFadeOut)
      {
        _fadeColor.a = 0;
        SceneLoader.LoadSceneCompleted();
        Destroy(gameObject);
      }
    }

    if (UseResourceImage)
    {
      _canvas.alpha = _fadeColor.a;
    }
    else
    {
      _fader.color = _fadeColor;
    }
  }

  // Triggers once new scene is loalded
  private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    ChangeState(TransitionState.Out);
  }

  // Used for swapping states
  private void ChangeState(TransitionState goTo)
  {
    _timer = 0f;
    _state = goTo;
  }

  // Add canvas component and create new Image object.
  private void CreateCanvas()
  {
    // Canvas constructor & setup
    Canvas canvas = gameObject.AddComponent<Canvas>();
    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    canvas.sortingOrder = SceneLoader.TRANSITIONER_SORTING_ORDER;

    CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
    scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
    scaler.referenceResolution = _screenSize;
    gameObject.AddComponent<GraphicRaycaster>();

    // Image child object constructor & setup

    GameObject fader;
    if (UseResourceImage)
    {
      fader = Instantiate(SceneLoader.GetLoadingScreen());
      _canvas = fader.GetComponent<CanvasGroup>();
      _canvas.alpha = 0f;
    }
    else
    {
      fader = new GameObject();
      _fader = fader.AddComponent<Image>();
      _fader.color = _fadeColor;
    }

    fader.name = "Screen Blocker";
    fader.transform.SetParent(transform);

    RectTransform faderRect = fader.GetComponent<RectTransform>();
    faderRect.localPosition = Vector3.zero;
    faderRect.sizeDelta = _screenSize;
  }
}
