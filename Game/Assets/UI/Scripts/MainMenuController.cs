using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour {
    private static readonly int ToBase = Animator.StringToHash("ToBase");
    private static readonly int ToConfig = Animator.StringToHash("ToConfig");
    private static readonly int ToHelp = Animator.StringToHash("ToHelp");
    private static readonly int ToPreGame = Animator.StringToHash("ToPreGame");
    private static readonly int IsCredits = Animator.StringToHash("IsCredits");

    int JugadorActual;
    string RazaJugador1="";
    string RazaJugador2="";

    private Animator animator;
    private GameManager gameManager;

    void Start() {
        gameManager = GameManager.Instance;
        animator = GetComponent<Animator>();
        gameManager.SceneLoaded();
        JugadorActual=1;
    }

    public void GoToBase() {
        animator.SetTrigger(ToBase);
    }

    public void GoToConfig() {
        animator.SetTrigger(ToConfig);
    }

    public void GoToHelp() {
        animator.SetTrigger(ToHelp);
    }

    public void GoToCredits(bool isCredits) {
        animator.SetBool(IsCredits, isCredits);
    }

    public void GoToPreGame() {
        animator.SetTrigger(ToPreGame);
    }

    public void GoToGame() {
        //Invoke(nameof(ChangeToGameScene), 0.1f);
        ValoresEntreEscenas.RazaJugador1 = RazaJugador1;
        ValoresEntreEscenas.RazaJugador2 = RazaJugador2;
	ValoresEntreEscenas.NumeroEscenaACargar=3;
	SceneManager.LoadScene(4);
    }	

    public void GoToGameVSCPU(){
        ValoresEntreEscenas.RazaJugador1 = RazaJugador1;
        ValoresEntreEscenas.RazaJugador2 = RazaJugador2;
        ValoresEntreEscenas.JugarVSCPU=true;
        ValoresEntreEscenas.NumeroEscenaACargar=3;	
	SceneManager.LoadScene(4);
    }

    public void ElegirHumano(){
        AsignarRaza("Humano");
        CambiarJugador();
    }

    public void ElegirMago(){
        AsignarRaza("Mago");
        CambiarJugador();
    }

    public void ElegirPiedra(){
        AsignarRaza("Piedra");
        CambiarJugador();
    }

    void AsignarRaza(string raza){
        if(JugadorActual==1){
            RazaJugador1=raza;
        }else{
            RazaJugador2=raza;
        }
        ActivarMarcadorJugador(raza);
    }

    void ActivarMarcadorJugador(string raza){
        GameObject[] marcasJugadorArray;
        marcasJugadorArray = GameObject.FindGameObjectsWithTag("Player_Select");

        foreach(GameObject marcas in marcasJugadorArray){
            string name = marcas.gameObject.name;
            if(name.Contains("J"+JugadorActual)){
                marcas.GetComponent<Image>().enabled=false;
            }
        }
        GameObject marcaActiva = GameObject.Find("J"+JugadorActual+"_"+raza);
        marcaActiva.GetComponent<Image>().enabled=true;
    }

     

    void CambiarJugador(){
         if(JugadorActual==1){
            JugadorActual=2;
        }else{
            JugadorActual=1;
        }
    }

    private void ChangeToGameScene() {
        gameManager.TransitionTo(GameManager.State.Game);
    }
}