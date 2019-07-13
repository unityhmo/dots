using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cuadro{
    public GameObject primeraLinea{get;set;}
    public GameObject segundaLinea{get;set;}
    public GameObject terceraLinea{get;set;}
    public GameObject cuartaLinea{get;set;}
}

public class GameController : MonoBehaviour
{   
    List<Cuadro> listaCuadros = new List<Cuadro>();
    public GameObject txtPuntajeJugador1;
    public GameObject txtPuntajeJugador2;
    public GameObject txtTurnoJugador;
    public GameObject txtContadorLineas;
    public GameObject txtMensajeFinal;

    int puntosJugador1;
    int puntosJugador2;
    int JugadorActual=1;

    bool acabaDeHacerUnPunto=false;

    int TotalMaximoLineas;
    int LineasActuales;
    string TextoFinal = "Empate";

    void Start(){
        CalcularTotalMaximoDeLineas();
        txtMensajeFinal.SetActive(false);
    }
    void Update()
    {
        txtPuntajeJugador1.GetComponent<Text>().text="Puntos: "+puntosJugador1;
        txtPuntajeJugador2.GetComponent<Text>().text="Puntos: "+puntosJugador2;
        txtTurnoJugador.GetComponent<Text>().text="Es el Turno del Jugador : "+JugadorActual;
        txtContadorLineas.GetComponent<Text>().text="El numero maximo de lineas es: "+TotalMaximoLineas+ " Lineas actuales: "+LineasActuales;
        txtMensajeFinal.GetComponent<Text>().text=TextoFinal;
    }

    void CalcularTotalMaximoDeLineas(){
        int columnas = this.GetComponent<GridGenerator>().GetTotalColumnas();
        int filas = this.GetComponent<GridGenerator>().GetTotalFilas();

        int colXFilas = columnas*filas;
        colXFilas=colXFilas+((columnas-1)*(filas-1))-1;

        TotalMaximoLineas=colXFilas;
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetJugadorActual(){
        return JugadorActual;
    }

    public void EvaluarTablero(){
        //Traer todas las lineas
        //Traer primera coordenada de la linea
        //Revisar si existen las lineas con las coordenadas que corresponden a cerrar un cuadro, en el sentido del reloj y contra el sentido.
        //Si existen 4 lineas que correspondan con el set de coordenadas, marcar un cuadro.

        GameObject[] lineArray;
        lineArray = GameObject.FindGameObjectsWithTag("Line");
        LineasActuales=lineArray.Length;



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
                EvaluarCuadro(col,fil);
            }
            }
                if(!acabaDeHacerUnPunto){
                    if(JugadorActual==1){
                        JugadorActual=2;
                }else{
                    JugadorActual=1;
                }
                }
            acabaDeHacerUnPunto=false;

       if(LineasActuales>=TotalMaximoLineas){
            //Todas las lineas posibles han sido llenadas determinar quien es el ganador/a
            if(puntosJugador1>puntosJugador2){
                TextoFinal="Gana el Jugador 1";
            }
            if(puntosJugador2>puntosJugador1){
                TextoFinal="Gana el Jugador 2";
            }
            txtMensajeFinal.SetActive(true);            
        }
      
    }

    bool EvaluarCuadro(int col, int fil){
        bool hayUnCuadro=false;
        string primeraCoordenada="["+col+","+fil+"]";
        string segundaCoordenada ="["+(col+1)+","+fil+"]";
        string terceraCoordenada ="["+(col+1)+","+(fil+1)+"]";
        string cuartaCoordenada ="["+col+","+(fil+1)+"]";

        //Existe esta linea?
        GameObject primeraLinea1 = GameObject.Find("Line_"+primeraCoordenada+"-"+segundaCoordenada+"(Clone)");
        GameObject primeraLinea2 = GameObject.Find("Line_"+segundaCoordenada+"-"+primeraCoordenada+"(Clone)");
        if(primeraLinea1!=null||primeraLinea2!=null){
            //buscar segunda linea
            GameObject segundaLinea1 = GameObject.Find("Line_"+segundaCoordenada+"-"+terceraCoordenada+"(Clone)");
            GameObject segundaLinea2 = GameObject.Find("Line_"+terceraCoordenada+"-"+segundaCoordenada+"(Clone)");
            if(segundaLinea1!=null||segundaLinea2!=null){
                //buscar tercera linea
                GameObject terceraLinea1 = GameObject.Find("Line_"+terceraCoordenada+"-"+cuartaCoordenada+"(Clone)");
                GameObject terceraLinea2 = GameObject.Find("Line_"+cuartaCoordenada+"-"+terceraCoordenada+"(Clone)");
                if(terceraLinea1!=null||terceraLinea2!=null){
                    //buscar cuarta linea que cierra el cuadro
                    GameObject cuartaLinea1 = GameObject.Find("Line_"+cuartaCoordenada+"-"+primeraCoordenada+"(Clone)");
                    GameObject cuartaLinea2 = GameObject.Find("Line_"+primeraCoordenada+"-"+cuartaCoordenada+"(Clone)");
                    if(cuartaLinea1!=null||cuartaLinea2!=null){
                        hayUnCuadro=true;
                        
                        Cuadro cuadro = new Cuadro();
                        var algo = primeraLinea1!=null ? cuadro.primeraLinea=primeraLinea1 : cuadro.primeraLinea=primeraLinea2;
                        var a2= segundaLinea1!=null ? cuadro.segundaLinea=segundaLinea1 : cuadro.segundaLinea=segundaLinea2;
                        var a3= terceraLinea1!=null ? cuadro.terceraLinea=terceraLinea1 : cuadro.terceraLinea=terceraLinea2;
                        var a4= cuartaLinea1!=null ? cuadro.cuartaLinea=cuartaLinea1 : cuadro.cuartaLinea=cuartaLinea2;
                        if(listaCuadros.Find(n=>   n.primeraLinea.name == cuadro.primeraLinea.name
                                                && n.segundaLinea.name==cuadro.segundaLinea.name
                                                && n.terceraLinea.name==cuadro.terceraLinea.name
                                                && n.cuartaLinea.name==cuadro.cuartaLinea.name)==null)
                        {
                            //Debug.Log("Hay un cuadro en "+col+","+fil);
                            listaCuadros.Add(cuadro);
                            //Llenar area del cuadro.
                            LlenarAreaDelCuadro(cuadro.primeraLinea,cuadro.terceraLinea);
                            acabaDeHacerUnPunto=true;
                            if(JugadorActual==1){
                                puntosJugador1++;
                            }else{
                                puntosJugador2++;
                            }
                        }else{
                            //Debug.Log("Cuadro ya existe es: "+cuadro.primeraLinea.name+ "/"+cuadro.segundaLinea.name+"/"+cuadro.terceraLinea.name+"/"+cuadro.cuartaLinea.name);
                            int contador=0;
                            foreach( Cuadro cuad in listaCuadros){
                                //Debug.Log("Cuadro "+contador+" : "+cuad.primeraLinea.name+ "/"+cuad.segundaLinea.name+"/"+cuad.terceraLinea.name+"/"+cuad.cuartaLinea.name);
                                contador++;
                            }
                        }
                        


                    }
                }
            }

        }

        return hayUnCuadro;
    }

    void LlenarAreaDelCuadro(GameObject primeraLinea, GameObject segundaLinea){
        Vector3 posicionArea = Vector3.Lerp(primeraLinea.transform.position , segundaLinea.transform.position, 0.5f);
        GameObject areaObject = (GameObject)Resources.Load ("ConqueredArea");
        if(JugadorActual==1){
           areaObject.GetComponent<SpriteRenderer>().color=Color.blue;
        }else{
           areaObject.GetComponent<SpriteRenderer>().color=Color.red;
        }
        Instantiate(areaObject, posicionArea, Quaternion.identity);
    }
}
