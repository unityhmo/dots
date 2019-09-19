// Scene loader implementation example. 
// This script is for demo purposes only.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceDemo : MonoBehaviour
{
  public Button resourceBtn;

  // Scene build index to be loaded by transitioner
  [SerializeField]
  private int _loadSceneIndex = 3;

  // Optional parameters for transitioner
  [SerializeField]
  private float _fadeInDuration = 1f;
  [SerializeField]
  private float _fadeOutDuration = 1f;
  [SerializeField]
  private Color _color = Color.white;

  private void Start()
  {
    resourceBtn.onClick.AddListener(ButtonHandlerResource);
  }

  private void ButtonHandlerResource()
  {
    // ADVANCED usage example.
    // Fade In&Out duration and color customization.
    Dictionary<string, object> optionalParameters = new Dictionary<string, object>();
    optionalParameters["useLoadingScreen"] = true;

    SceneLoader.LoadScene(_loadSceneIndex, optionalParameters);
  }
}
