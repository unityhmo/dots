using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
  private static readonly int ToBase = Animator.StringToHash("ToBase");
  private static readonly int ToConfig = Animator.StringToHash("ToConfig");
  private static readonly int ToHelp = Animator.StringToHash("ToHelp");
  private static readonly int ToPreGame = Animator.StringToHash("ToPreGame");
  private static readonly int IsCredits = Animator.StringToHash("IsCredits");

  int JugadorActual;
  string RazaJugador1 = "";
  string RazaJugador2 = "";

  private Animator _animator;

  void Start()
  {
    _animator = GetComponent<Animator>();
    JugadorActual = 1;
  }

  public void GoToBase()
  {
    _animator.SetTrigger(ToBase);
  }

  public void GoToConfig()
  {
    _animator.SetTrigger(ToConfig);
  }

  public void GoToHelp()
  {
    _animator.SetTrigger(ToHelp);
  }

  public void GoToCredits(bool isCredits)
  {
    _animator.SetBool(IsCredits, isCredits);
  }

  public void GoToPreGame()
  {
    _animator.SetTrigger(ToPreGame);
  }

  public void GoToGame()
  {
    ValoresEntreEscenas.JugarVSCPU = false;
    LoadGameScene();
  }

  public void GoToGameVSCPU()
  {
    ValoresEntreEscenas.JugarVSCPU = true;
    LoadGameScene();
  }

  private void LoadGameScene()
  {
    ValoresEntreEscenas.RazaJugador1 = RazaJugador1;
    ValoresEntreEscenas.RazaJugador2 = RazaJugador2;
    ValoresEntreEscenas.NumeroEscenaACargar = 3;

    Dictionary<string, object> optionalParameters = new Dictionary<string, object>();
    optionalParameters["useLoadingScreen"] = true;

    SceneLoader.LoadScene(3, optionalParameters);
  }

  public void ElegirHumano()
  {
    AsignarRaza("Humano");
    CambiarJugador();
  }

  public void ElegirMago()
  {
    AsignarRaza("Mago");
    CambiarJugador();
  }

  public void ElegirPiedra()
  {
    AsignarRaza("Piedra");
    CambiarJugador();
  }

  void AsignarRaza(string raza)
  {
    if (JugadorActual == 1)
    {
      RazaJugador1 = raza;
    }
    else
    {
      RazaJugador2 = raza;
    }
    ActivarMarcadorJugador(raza);
  }

  void ActivarMarcadorJugador(string raza)
  {
    GameObject[] marcasJugadorArray;
    marcasJugadorArray = GameObject.FindGameObjectsWithTag("Player_Select");

    foreach (GameObject marcas in marcasJugadorArray)
    {
      string name = marcas.gameObject.name;
      if (name.Contains("J" + JugadorActual))
      {
        marcas.GetComponent<Image>().enabled = false;
      }
    }
    GameObject marcaActiva = GameObject.Find("J" + JugadorActual + "_" + raza);
    marcaActiva.GetComponent<Image>().enabled = true;
  }

  void CambiarJugador()
  {
    if (JugadorActual == 1)
    {
      JugadorActual = 2;
    }
    else
    {
      JugadorActual = 1;
    }
  }
}