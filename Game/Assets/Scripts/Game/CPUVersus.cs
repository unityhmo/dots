using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CPUVersus : MonoBehaviour
{
    /*
    1.- Busca si hay un cuadro que solo falte una linea que poner, si lo hay, pone esa linea
    2.- Si no hay la posibilidad de hacer puntos, Busca poner una linea donde no vaya a formar un cuadro
    3.- Si no hay la posibilidad de hacer la tercera linea de un cuadro, pone una linea en un lugar disponible aleatorio.
    4.- Si tiene la posibilidad de usar su habilidad, la usará.
     */
    bool vsCPU;
    string raza;
    bool GameOver;
    bool esperar;
    bool yaEstaEsperando;

    void Start(){
        vsCPU=ValoresEntreEscenas.JugarVSCPU;
        raza=ValoresEntreEscenas.RazaJugador2;
    }
     void Update(){
         if(vsCPU){
             GameOver=this.GetComponent<GameController>().GetGameOver();
             if(!GameOver){
             //Comenzar a evaluar sus jugadas
             int jugador = this.GetComponent<GameController>().GetJugadorActual();
             if(jugador==2){
                 esperar=true;
                 //Es el turno de la maquina.
                 //Esperar un momento.
                if(esperar&&!yaEstaEsperando){
                     StartCoroutine(EsperarUnSegundo());
                     yaEstaEsperando=true;
                }
             }
             }
         }
     }

    IEnumerator EsperarUnSegundo()
    {
        yield return new WaitForSeconds(0.7f);
        esperar=false;
        yaEstaEsperando=false;

        //Continuar con el turno
        bool hizoLinea=false;
         //busca cerrar un cuadro
        hizoLinea=CerrarCuadro();
                 
        //Poner una linea disponible como suya.
        if(!hizoLinea){
            MarcarLineaRandom();
        }        
    }

     bool CerrarCuadro(){
        GameObject[] lineArray;
        lineArray = GameObject.FindGameObjectsWithTag("Line");
        bool cerroCuadro=false;
            if (lineArray.Length > 0)
            {
            foreach(GameObject line in lineArray){
                string nombreLinea = line.name.Replace("(Clone)","");
                string segundaCoordenada = nombreLinea.Substring(nombreLinea.LastIndexOf('-') + 1);
                string primeraCoordenada = nombreLinea.Substring(nombreLinea.LastIndexOf('_') + 1);
                primeraCoordenada=primeraCoordenada.Replace("-"+segundaCoordenada,"");
                string fila=primeraCoordenada.Substring(primeraCoordenada.LastIndexOf(',')+1);
                string columna = primeraCoordenada.Replace(","+fila,"");
                fila=fila.Replace("]","");
                columna=columna.Replace("[","");

                int fil = Convert.ToInt32(fila);
                int col= Convert.ToInt32(columna);
                cerroCuadro=EvaluarCuadro(col,fil);
                if(cerroCuadro){
                    break;
                }
            }
            }
        return cerroCuadro;
     }

     bool EvaluarCuadro(int col, int fil){
        bool marcoLinea=false;
        string primeraCoordenada="["+col+","+fil+"]";
        string segundaCoordenada ="["+(col+1)+","+fil+"]";
        string terceraCoordenada ="["+(col+1)+","+(fil+1)+"]";
        string cuartaCoordenada ="["+col+","+(fil+1)+"]";

        string tagLinea="Line";

        //Existe esta linea?
        GameObject primeraLinea1 = GameObject.Find("Line_"+primeraCoordenada+"-"+segundaCoordenada+"(Clone)");
        GameObject primeraLinea2 = GameObject.Find("Line_"+segundaCoordenada+"-"+primeraCoordenada+"(Clone)");
        bool tieneTag=false;
        GameObject primeraLinea=null;
        if(primeraLinea1!=null){
            primeraLinea=primeraLinea1;
        }
        if(primeraLinea2!=null){
            primeraLinea=primeraLinea2;
        }
        if(primeraLinea!=null){
        if(primeraLinea.tag==tagLinea){
            tieneTag=true;
        }
        }
        if(tieneTag){
            Debug.Log("primera");
            //buscar segunda linea
            GameObject segundaLinea1 = GameObject.Find("Line_"+segundaCoordenada+"-"+terceraCoordenada+"(Clone)");
            GameObject segundaLinea2 = GameObject.Find("Line_"+terceraCoordenada+"-"+segundaCoordenada+"(Clone)");
            tieneTag=false;
            GameObject segundaLinea=null;
            if(segundaLinea1!=null){
                segundaLinea=segundaLinea1;
            }
            if(segundaLinea2!=null){
                segundaLinea=segundaLinea2;
            }
            if(segundaLinea!=null){
            if(segundaLinea.tag==tagLinea){
                tieneTag=true;
            }
            }
            if(tieneTag){
                Debug.Log("segunda");
                //buscar tercera linea
                GameObject terceraLinea1 = GameObject.Find("Line_"+terceraCoordenada+"-"+cuartaCoordenada+"(Clone)");
                GameObject terceraLinea2 = GameObject.Find("Line_"+cuartaCoordenada+"-"+terceraCoordenada+"(Clone)");
                tieneTag=false;
                GameObject terceraLinea=null;
                if(terceraLinea1!=null){
                    terceraLinea=terceraLinea1;
                }
                if(terceraLinea2!=null){
                    terceraLinea=terceraLinea2;
                }
                if(terceraLinea!=null){
                    if(terceraLinea.tag==tagLinea){
                        tieneTag=true;
                    }
                }
                if(tieneTag){
                    Debug.Log("tercera");
                    //buscar cuarta linea que cierra el cuadro
                    GameObject cuartaLinea1 = GameObject.Find("Line_"+cuartaCoordenada+"-"+primeraCoordenada+"(Clone)");
                    GameObject cuartaLinea2 = GameObject.Find("Line_"+primeraCoordenada+"-"+cuartaCoordenada+"(Clone)");
                    tieneTag=false;
                    tagLinea="LineaEnEspera";
                    GameObject cuartaLinea=null;
                    if(cuartaLinea1!=null){
                        cuartaLinea=cuartaLinea1;
                    }
                    if(cuartaLinea2!=null){
                        cuartaLinea=cuartaLinea2;
                    }
                    if(cuartaLinea!=null){
                    if(cuartaLinea.tag==tagLinea){
                        tieneTag=true;
                    }
                    }
                    if(tieneTag){          
                        Debug.Log("cuarta, marcar");              
                        //Coloca la linea para cerrar el cuadro.
                        if(cuartaLinea.GetComponent<LineController>().ChecarEventos()){
                            cuartaLinea.GetComponent<LineController>().ActivarLinea();
                            marcoLinea=true;
                        }    
                    }
                }
            }
        }
        return marcoLinea;
    }

     void MarcarLineaRandom(){        
        //Toma todas las lineas sin marcar.
        GameObject[] lineArray;
        lineArray = GameObject.FindGameObjectsWithTag("LineaEnEspera");
        if(lineArray.Length>0){
        //Selecciona una linea al azar
        int posicionRandom = UnityEngine.Random.Range(0, lineArray.Length);
        GameObject lineaRandom = lineArray[posicionRandom];
        bool marcoLinea=false;

        while(!marcoLinea){
            //Primero checa los eventos de la linea
            if(lineaRandom.GetComponent<LineController>().ChecarEventos()){
                //Si si te lo permite, marcala.
                lineaRandom.GetComponent<LineController>().ActivarLinea();
                marcoLinea=true;
            }else{
                posicionRandom = UnityEngine.Random.Range(0, lineArray.Length);
                lineaRandom = lineArray[posicionRandom];
            }
        }
        }       
     }


}
