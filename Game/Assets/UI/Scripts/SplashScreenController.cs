using System.Collections.Generic;
using UnityEngine;

public class SplashScreenController : MonoBehaviour
{
  [SerializeField]
  private bool _isSkippable = true;
  [SerializeField]
  private float _delayBeforeNextStep = 1f;

  private void Start()
  {
    Invoke(nameof(TransitionToNextStep), _delayBeforeNextStep);
  }

  private void Update()
  {
    if (_isSkippable && (Input.touchCount > 0 || Input.anyKeyDown))
    {
      CancelInvoke(nameof(TransitionToNextStep));
      TransitionToNextStep();
    }
  }

  private void TransitionToNextStep()
  {
    Dictionary<string, object> optionalParameters = new Dictionary<string, object>();
    optionalParameters["useLoadingScreen"] = true;
    SceneLoader.LoadScene(2, optionalParameters);
  }
}