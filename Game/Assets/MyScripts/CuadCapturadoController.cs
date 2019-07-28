using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuadCapturadoController : MonoBehaviour
{   
    public bool esContinuo;
    public int numeroJugador;
    public bool esInrobable;
    public int ContadorBlindaje;

    void OnMouseDown(){
       ChecarEventos();
    }

    void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.tag == "Item")
        {
            string nameCol = col.gameObject.name;
            GameObject objGC = GameObject.Find("GameController");
            if(objGC!=null){
            int jugador=GetJugadorActual();
            if(nameCol.Contains("AgregarEnergia")){
                    if(jugador==1){
                        objGC.GetComponent<GameController>().SubirEnergiaJugador1();
                    }else{
                        objGC.GetComponent<GameController>().SubirEnergiaJugador2();
                    }                
                Destroy(col.gameObject);
            }

             if(nameCol.Contains("QuitarEnergia")){
                    if(jugador==1){
                        objGC.GetComponent<GameController>().BajarEnergiaJugador1();
                    }else{
                        objGC.GetComponent<GameController>().BajarEnergiaJugador2();
                    }
                
                Destroy(col.gameObject);
             }

             if(nameCol.Contains("Multiplicador")){
                int random = (int)Random.Range(0f, 3f);
                int multiplicador=1;
                if(random>1){
                    multiplicador=2;
                }
                if(jugador==1){
                    objGC.GetComponent<GameController>().SubirPuntajeExtraJugador1(multiplicador);
                }else{
                    objGC.GetComponent<GameController>().SubirPuntajeExtraJugador2(multiplicador);
                }
                Destroy(col.gameObject);

             }
            }

        }
    }

    void ChecarEventos(){
        int jugador=GetJugadorActual();
        GameObject objGC = GameObject.Find("GameController");
        if(objGC!=null){
            bool puedeRobar=objGC.GetComponent<GameController>().PuedeRobar(jugador); 
            if(puedeRobar&&!esInrobable&&jugador!=numeroJugador){
                //Checar el blindaje
                if(ContadorBlindaje<1){
                    this.gameObject.name="Conquered_J"+jugador+"(Clone)";
                    if(jugador==1){
                        this.gameObject.GetComponent<SpriteRenderer>().color=Color.blue;
                    }else{
                        this.gameObject.GetComponent<SpriteRenderer>().color=Color.red;
                    }

                    if(jugador==1){
                        objGC.GetComponent<GameController>().SubirPuntajeJugador1(); 
                        objGC.GetComponent<GameController>().BajarPuntajeJugador2(); 
                        objGC.GetComponent<GameController>().FinRoboJugador1();
                    }else{
                        objGC.GetComponent<GameController>().BajarPuntajeJugador1(); 
                        objGC.GetComponent<GameController>().SubirPuntajeJugador2(); 
                        objGC.GetComponent<GameController>().FinRoboJugador2();
                    }
                   
                    esInrobable=true;
                    //esContinuo=false;
                    numeroJugador=jugador;
                    objGC.GetComponent<GameController>().TransformarCuadrosConsecutivos(this.gameObject.transform.position);
                    objGC.GetComponent<GameController>().LimpiarCapturados();
                }else{
                    ContadorBlindaje--;
                }
                
            }
        }
    }

    int GetJugadorActual(){
        int jugador=0;
        GameObject objGC = GameObject.Find("GameController");
        if(objGC!=null){
            jugador= objGC.GetComponent<GameController>().GetJugadorActual();
        }
        return jugador;
    }

    void CambiarColorContinuo(){
        int jugador=0;
        jugador=GetJugadorActual();
        if(esContinuo){
        if(jugador==1){
            this.GetComponent<SpriteRenderer>().color=Color.yellow;
        }else{
            this.GetComponent<SpriteRenderer>().color=Color.green;
        }
        }else{
            if(numeroJugador==1){
                this.gameObject.GetComponent<SpriteRenderer>().color=Color.blue;
            }else{
                this.gameObject.GetComponent<SpriteRenderer>().color=Color.red;
            }
        }
    }

    public void SetEsContinuo(bool continuo){
        esContinuo=continuo;
        CambiarColorContinuo();
    }

    public void SetNumeroJugador(int jugador){
        numeroJugador=jugador;
    }

    public bool GetEsContinuo(){
        return esContinuo;
    }

    public bool GetEsInrobable(){
        return esInrobable;
    }

    public int GetContadorBlindaje(){
        return ContadorBlindaje;
    }

    public void SetContadorBlindaje(int num){
        ContadorBlindaje=num;
    }


}
