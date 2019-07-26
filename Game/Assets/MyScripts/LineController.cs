using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    bool estaBloqueada;
    int jugadorBloqueo;

    void OnMouseDown(){
        if(ChecarEventos()){
            ActivarLinea();
        }
    }

    bool  ChecarEventos(){
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
                if(PuedeBloquear){
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

    void ActivarLinea(){
        if(this.gameObject.tag!="Line"){
            this.gameObject.tag="Line";
            GameObject objGC = GameObject.Find("GameController");
            if(objGC!=null){
                if(GetJugador()==1){
                //raya azul
                    this.gameObject.GetComponent<SpriteRenderer>().color=Color.blue;
                }else{
                //raya roja
                    this.gameObject.GetComponent<SpriteRenderer>().color=Color.red;
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
