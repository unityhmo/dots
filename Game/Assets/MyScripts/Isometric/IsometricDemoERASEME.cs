using UnityEngine;
using UnityEngine.UI;

public class IsometricDemoERASEME : MonoBehaviour
{
  public Button _closeButton;

  private void Start()
  {
    _closeButton.onClick.AddListener(delegate { Application.Quit(); });
  }
}
