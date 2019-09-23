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
    SceneLoader.LoadScene(2);
  }
}