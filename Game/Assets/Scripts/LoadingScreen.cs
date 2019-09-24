using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
  public Slider slider;
  private SceneTransition _transitioner;

  public void StartUp(SceneTransition transitioner)
  {
    _transitioner = transitioner;
  }

  private void Update()
  {
    if (_transitioner == null)
    {
      return;
    }

    slider.value = _transitioner.Progress;
  }
}
