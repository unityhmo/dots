using UnityEngine;

public class ChestAnimation : MonoBehaviour
{
  public GameObject[] loot;
  public Transform lootHolder;

  private GameObject _loot;
  private Animator _anim;

  private void Start()
  {
    _anim = GetComponent<Animator>();

     int random = (int)Random.Range(0,loot.Length);
     GameObject thisLoot = loot[random];
    if (thisLoot)
    {
      SetLoot(thisLoot);
    }
  }

  public void SetLoot(GameObject loot)
  {
    RemoveLoot();

    if (loot != null)
    {
      _loot = Instantiate(loot);
      _loot.transform.parent = lootHolder;
      _loot.transform.SetPositionAndRotation(lootHolder.transform.position, lootHolder.transform.rotation);
      _loot.transform.localScale = new Vector3(1, 1, 1);
    }
  }

  public void RemoveLoot()
  {
    if (_loot)
    {
      Destroy(_loot);
    }
  }

  public void Open()
  {
    _anim.SetBool("isOpen", true);
  }

  public void Close()
  {
    _anim.SetBool("isOpen", false);
  }
}
