using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    bool estaBloqueada;
    int jugadorBloqueo;
    int jugadorCapturo;

    void OnMouseUpAsButton(){
        if(ChecarVsCPU()){
            if(ChecarEventos()){            
                ActivarLinea();
            }
        }
    }

    bool ChecarVsCPU(){
        bool puedeContinuar=false;
        bool vsCPU=true;//ValoresEntreEscenas.JugarVSCPU;
        if(vsCPU){
             int jugador=GetJugador();
             if(jugador==1){
                 //No es turno de la maquina, puede continuar.
                 puedeContinuar=true;
             }
        }else{
            //No es contra la maquina, puede continuar.
            puedeContinuar=true;
        }
        return puedeContinuar;
    }

    void AnimarLinea(){
        this.GetComponent<Animator>().Play("Line_Animation");
    }

    public bool  ChecarEventos(){
        bool puedeActivarLinea=false;
        int jugador=GetJugador();
        
        if(estaBloqueada){
            puedeActivarLinea=false;
            if(jugadorBloqueo==jugador){
            puedeActivarLinea=true;
            }    
        }else{            
            GameObject objGC = GameObject.Find("GameController");
            if(objGC!=null){
                bool PuedeBloquear=objGC.GetComponent<GameController>().PuedeBloquear(jugador);
                if(PuedeBloquear&&jugadorCapturo==0){
                    puedeActivarLinea=false;
                    estaBloqueada=true;
                    jugadorBloqueo=jugador;
                    this.gameObject.GetComponent<SpriteRenderer>().color=Color.yellow;
                    if(jugador==1){
                        objGC.GetComponent<GameController>().FinBloquearJugador1();
                        
                    }else{
                        objGC.GetComponent<GameController>().FinBloquearJugador2();
                    }
                }else{
                    puedeActivarLinea=true;
                } 
            }
        }

      
        return puedeActivarLinea;
    }

    public void ActivarLinea(){
        if(this.gameObject.tag!="Line"){
            this.gameObject.tag="Line";
            GameObject objGC = GameObject.Find("GameController");
            if(objGC!=null){
                AnimarLinea();
                if(GetJugador()==1){
                //raya azul
                    this.gameObject.GetComponent<SpriteRenderer>().color=Color.blue;
                    jugadorCapturo=1;
                }else{
                //raya roja
                    this.gameObject.GetComponent<SpriteRenderer>().color=Color.red;
                    jugadorCapturo=2;
                }
                objGC.GetComponent<GameController>().EvaluarTablero();
            }
        }
    }

    int GetJugador(){
        GameObject objGC = GameObject.Find("GameController");
        if(objGC!=null){
            return objGC.GetComponent<GameController>().GetJugadorActual();
        }
        return -1;
    }

    public bool GetEstaBloqueada(){
        return estaBloqueada;
    }
}
