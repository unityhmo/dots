using UnityEngine;
using UnityEngine.UI;

public class PickUpDemo : MonoBehaviour
{
  public GameObject chest;
  public Button chestInteraction;
  public GameObject sphere;
  public GameObject cube;
  public Button lootSphere;
  public Button lootCube;
  public Button lootEmpty;
  public Button exitApp;

  private bool _isOpen = false;
  private Text _buttonText;

  private void Start()
  {
    exitApp.onClick.AddListener(delegate { Application.Quit(); });
    chestInteraction.onClick.AddListener(delegate { ChestInteration(!_isOpen); });
    lootSphere.onClick.AddListener(delegate { SetLoot(sphere); });
    lootCube.onClick.AddListener(delegate { SetLoot(cube); });
    lootEmpty.onClick.AddListener(delegate { EmptyLoot(); });
    _buttonText = chestInteraction.GetComponentInChildren<Text>();
    _buttonText.text = "Open";
  }

  private void ChestInteration(bool isOpened)
  {
    _isOpen = isOpened;

    if (_isOpen)
    {
      _buttonText.text = "Close";
      chest.SendMessage("Open");
    }
    else
    {
      _buttonText.text = "Open";
      chest.SendMessage("Close");
    }
  }

  private void SetLoot(GameObject loot)
  {
    chest.SendMessage("SetLoot", loot);
  }

  private void EmptyLoot()
  {
    chest.SendMessage("RemoveLoot");
  }
}
