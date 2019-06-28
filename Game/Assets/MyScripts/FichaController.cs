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
            LimpiarFichas();
        }          
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
        Vector3 posicionLinea = Vector3.Lerp(objPrimeraFicha.transform.position , objSegundaFicha.transform.position, 0.5f);
        GameObject lineObject = (GameObject)Resources.Load ("Line");
        

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
            Instantiate(lineObject, posicionLinea, Quaternion.Euler(0,0,90));
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
             Instantiate(lineObject, posicionLinea, Quaternion.identity);
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

    void LimpiarFichas(){
        this.GetComponent<LookForPosibleTargets>().LimpiarFichas();
    }

}
