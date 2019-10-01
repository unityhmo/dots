using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
  public Slider slider;
  private SceneTransition _transitioner;
  public GameObject holderClanSprites;
  public Sprite[] clanSprites;

  public void StartUp(SceneTransition transitioner)
  {
    _transitioner = transitioner;
    SetClanSprite();
  }

  private void Update()
  {
    if (_transitioner == null)
    {
      return;
    }

    slider.value = _transitioner.Progress;
  }

  void SetClanSprite(){
     int rand = UnityEngine.Random.Range(0, clanSprites.Length);
     holderClanSprites.GetComponent<Image>().sprite = clanSprites[rand];
     holderClanSprites.GetComponent<Image>().SetNativeSize();
  }
}
