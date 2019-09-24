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
        if(col.gameObject.tag == "Particles_Potion")
        {
          ParticleSystem ps = col.gameObject.GetComponent<ParticleSystem>();
          var emission = ps.emission;
          emission.enabled = true;
          Debug.Log("particlespotion");
        }

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
                                    
                Destroy(col.gameObject,1);
            }

             if(nameCol.Contains("QuitarEnergia")){
                    if(jugador==1){
                        objGC.GetComponent<GameController>().BajarEnergiaJugador1();
                    }else{
                        objGC.GetComponent<GameController>().BajarEnergiaJugador2();
                    }
                
                Destroy(col.gameObject,1);
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
                Destroy(col.gameObject,1);

             }

            if(nameCol.Contains("Chest")){
                col.gameObject.GetComponent<ChestAnimation>().Open();
                Destroy(col.gameObject,1);
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
                    this.gameObject.name="Conquered_J"+jugador+"$x="+this.gameObject.transform.position.x.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"#z="+this.gameObject.transform.position.z.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"&(Clone)";                    
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
                    esContinuo=false;
                    numeroJugador=jugador;
                    objGC.GetComponent<SimpleConsecutivoController>().ActualizarConsecutivos();
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
        if(esContinuo){
        if(numeroJugador==1){
            this.GetComponent<SpriteRenderer>().color=Color.yellow;
        }else{
            this.GetComponent<SpriteRenderer>().color=Color.green;
        }
        }else{
            if(numeroJugador==1){
                this.GetComponent<SpriteRenderer>().color=Color.blue;
            }else{
                this.GetComponent<SpriteRenderer>().color=Color.red;
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
