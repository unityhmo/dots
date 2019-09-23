using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FichaController : MonoBehaviour
{
    public bool OcupadoArriba;
    public bool OcupadoAbajo;
    public bool OcupadoDerecha;
    public bool OcupadoIzquierda;

    public bool primerFicha;

    string nombrePrimeraFicha;
    string nombreSegundaFicha;
    void OnMouseDown(){
        //click en la ficha
        DibujarLinea();
    }

    void DibujarLinea(){
        if(this.GetComponent<SpriteRenderer>().color==Color.green){
            this.GetComponent<SpriteRenderer>().color=Color.red;
            BuscarPrimerFicha();
            nombreSegundaFicha=this.name;
            DeterminarLadosOcupados();
            EvaluarTablero();
        }          
    }

    void EvaluarTablero(){
        GameObject objGC = GameObject.Find("GameController");
        if(objGC!=null){
            objGC.GetComponent<GameController>().EvaluarTablero();
        }
    }

    int GetJugador(){
        GameObject objGC = GameObject.Find("GameController");
        if(objGC!=null){
            return objGC.GetComponent<GameController>().GetJugadorActual();
        }
        return -1;
    }

    void DeterminarLadosOcupados(){
        //Columna y fila de primer ficha
        if(nombrePrimeraFicha!=null){
        string primerFicha=nombrePrimeraFicha.Replace("(Clone)","");
        string primerFila = primerFicha.Substring(primerFicha.LastIndexOf(',') + 1);
        string primerColumna = primerFicha.Substring(primerFicha.LastIndexOf('_') + 1);
        primerColumna=primerColumna.Replace(","+primerFila,"");

        int c1 = Convert.ToInt32(primerColumna);
        int f1 = Convert.ToInt32(primerFila);

        //Columna y fila de segunda ficha //TODO: quitar esta copia del codigo
        string segundaFicha=nombreSegundaFicha.Replace("(Clone)","");
        string segundaFila = segundaFicha.Substring(segundaFicha.LastIndexOf(',') + 1);
        string segundaColumna = segundaFicha.Substring(segundaFicha.LastIndexOf('_') + 1);
        segundaColumna=segundaColumna.Replace(","+segundaFila,"");

        int c2 = Convert.ToInt32(segundaColumna);
        int f2 = Convert.ToInt32(segundaFila);

        GameObject objPrimeraFicha = GameObject.Find(nombrePrimeraFicha);
        GameObject objSegundaFicha = GameObject.Find(nombreSegundaFicha);
        
        //Agregar linea
        string nombreLinea = "Line_["+c1+","+f1+"]-["+c2+","+f2+"](Clone)";
        GameObject lineObject = GameObject.Find(nombreLinea);
        if(lineObject==null){
             nombreLinea = "Line_["+c2+","+f2+"]-["+c1+","+f1+"](Clone)";
             lineObject = GameObject.Find(nombreLinea);
        }
        if(lineObject!=null){
            if(GetJugador()==1){
            //raya azul
            lineObject.GetComponent<SpriteRenderer>().color=Color.blue;
            }else{
            //raya roja
            lineObject.GetComponent<SpriteRenderer>().color=Color.red;
            }

            //activar linea
            lineObject.gameObject.tag="Line";
            Color tmp = lineObject.GetComponent<SpriteRenderer>().color;
            tmp.a = 255f;
            lineObject.GetComponent<SpriteRenderer>().color = tmp;
        }
        

        if(c1==c2){
            //arriba o abajo
            if(f1>f2){
                //arriba
                objPrimeraFicha.GetComponent<FichaController>().OcupadoArriba=true;
                objSegundaFicha.GetComponent<FichaController>().OcupadoAbajo=true;
            }else{
                //abajo
                objPrimeraFicha.GetComponent<FichaController>().OcupadoAbajo=true;
                objSegundaFicha.GetComponent<FichaController>().OcupadoArriba=true;
            }            
        }
        else{
            //izquierda o derecha
            if(c1>c2){
                //izquierda
                objPrimeraFicha.GetComponent<FichaController>().OcupadoIzquierda=true;
                objSegundaFicha.GetComponent<FichaController>().OcupadoDerecha=true;
            }else{
                //derecha
                objPrimeraFicha.GetComponent<FichaController>().OcupadoDerecha=true;
                objSegundaFicha.GetComponent<FichaController>().OcupadoIzquierda=true;
            }
        }       
        }
        
    }

    public void BuscarPrimerFicha(){
        //Ficha que fue tocada primeramente
        GameObject[] arrayFichas = GameObject.FindGameObjectsWithTag("Ficha");
			if (arrayFichas != null) {
                foreach(GameObject ficha in arrayFichas){
                    if(ficha.GetComponent<FichaController>().primerFicha){                        
                        ficha.GetComponent<SpriteRenderer>().color=Color.blue;
                        ficha.GetComponent<FichaController>().primerFicha=false;
                        nombrePrimeraFicha=ficha.name;
                    };
                }
            }
    }

}
