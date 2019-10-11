using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PropsManager : MonoBehaviour
{
  public GameObject gameManager;
  public GameObject humansProp;
  public GameObject sagesProp;
  public GameObject demonsProp;
  public GameObject capturedQuad;

  public GameObject[] humansStructures;
  public GameObject[] sagesStructures;
  public GameObject[] demonsStructures;

  public void InstantiateHumans(Vector3 coords)
  {
    InstantiateProp(coords, humansProp);
    InstantiateStructure(coords, SelectRandomStructure(humansStructures));
  }

  public void InstantiateSages(Vector3 coords)
  {
    InstantiateProp(coords, sagesProp);
    InstantiateStructure(coords, SelectRandomStructure(sagesStructures));
  }

  public void InstantiateDemons(Vector3 coords)
  {
    InstantiateProp(coords, demonsProp);
    InstantiateStructure(coords, SelectRandomStructure(demonsStructures));
  }

  public GameObject SelectRandomStructure(GameObject[] structureArray){
    int rand = UnityEngine.Random.Range(0, structureArray.Length);
    return structureArray[rand];
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

  private void InstantiateStructure(Vector3 coords, GameObject structure)
  {
    StartCoroutine(EsperarUnSegundo(coords,structure));
   
  }

  IEnumerator EsperarUnSegundo(Vector3 coords, GameObject structure)
  {
      yield return new WaitForSeconds(0.8f);
      Instantiate(structure, coords, Quaternion.Euler(0, 0, 0));     
  }
  
}
