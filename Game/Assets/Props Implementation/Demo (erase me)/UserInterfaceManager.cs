using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
  public enum FadeInOutState
  {
    Off = 0,
    FadeIn = 1,
    FadeOut = 2
  }

  public PropsManager propsManager;
  public Button testHuman;
  public Button testSages;
  public Button testDemons;
  public Button exitApp;
  public Image fadeInOutImage;

  [SerializeField]
  private float _fadeInOutDuration = 2f;
  private float _lerpTimer;
  private Color _currentColor;
  private FadeInOutState _faderState = FadeInOutState.Off;

  private void Awake()
  {
    _currentColor = fadeInOutImage.color;
    _currentColor.a = 255;
    fadeInOutImage.color = _currentColor;
    fadeInOutImage.enabled = true;
  }

  private void Start()
  {
    testHuman.onClick.AddListener(delegate { propsManager.InstantiateHumans(Vector3.zero); });
    testSages.onClick.AddListener(delegate { propsManager.InstantiateSages(Vector3.zero); });
    testDemons.onClick.AddListener(delegate { propsManager.InstantiateDemons(Vector3.zero); });
    exitApp.onClick.AddListener(delegate { KillApp(); });
    Invoke("HoldBeforeStarting", 0.5f);
  }

  private void HoldBeforeStarting()
  {
    StartFadeInOut(FadeInOutState.FadeOut);
  }

  private void StartFadeInOut(FadeInOutState state)
  {
    _lerpTimer = 0;
    _faderState = state;
  }

  private void Update()
  {
    if (_faderState != FadeInOutState.Off)
    {
      _lerpTimer += Time.deltaTime;

      if (_faderState == FadeInOutState.FadeIn)
      {
        _currentColor.a = Mathf.Lerp(0, 1, _lerpTimer / _fadeInOutDuration);
      }
      else if (_faderState == FadeInOutState.FadeOut)
      {
        _currentColor.a = Mathf.Lerp(1, 0, _lerpTimer / _fadeInOutDuration);
      }

      fadeInOutImage.color = _currentColor;

      if (_lerpTimer >= _fadeInOutDuration)
      {
        StartFadeInOut(FadeInOutState.Off);
      }
    }
  }

  private void KillApp()
  {
    StartFadeInOut(FadeInOutState.FadeIn);
    Invoke("QuitApp", _fadeInOutDuration);
  }

  private void QuitApp()
  {
    Debug.Log("Application.Quit() executed.");
    Application.Quit();
  }
}
