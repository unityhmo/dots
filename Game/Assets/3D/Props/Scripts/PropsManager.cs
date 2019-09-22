using UnityEngine;

public class PropsManager : MonoBehaviour
{
  public GameObject gameManager;
  public GameObject humansProp;
  public GameObject sagesProp;
  public GameObject demonsProp;
  public GameObject capturedQuad;

  public void InstantiateHumans(Vector3 coords)
  {
    InstantiateProp(coords, humansProp);
  }

  public void InstantiateSages(Vector3 coords)
  {
    InstantiateProp(coords, sagesProp);
  }

  public void InstantiateDemons(Vector3 coords)
  {
    InstantiateProp(coords, demonsProp);
  }

  public void InstantiateCapturedQuad(Vector3 coords)
  {
    InstantiateProp(coords, capturedQuad);
  }

  public void QuadCaptureAnimationCompleted(Vector3 coords)
  {
    if (gameManager)
    {
      gameManager.SendMessage("CaptureCompleted", coords, SendMessageOptions.DontRequireReceiver);
    }
  }

  private void InstantiateProp(Vector3 coords, GameObject prop)
  {
    Instantiate(prop).GetComponent<Prop>().StartUp(this).position = coords;
  }
}
