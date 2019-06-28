using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LookForPosibleTargets : MonoBehaviour
{
    void OnMouseDown()
    {
        //Limpiar todas las fichas
        LimpiarFichas();
       //Lo que pasa cuando le dan click a un punto
       BuscarVecinos();
    }

    void LimpiarFichas(){
        //Regresa todas las fichas a su color original.
        GameObject[] arrayFichas = GameObject.FindGameObjectsWithTag("Ficha");
			if (arrayFichas != null) {
                foreach(GameObject ficha in arrayFichas){
                    ficha.GetComponent<SpriteRenderer>().color=Color.white;
                }
            }
    }

    void BuscarVecinos(){
        //Obtener las filas y columnas.
        string nombreFicha=transform.gameObject.name;
        nombreFicha=nombreFicha.Replace("(Clone)","");
        string Fila = nombreFicha.Substring(nombreFicha.LastIndexOf(',') + 1);
        string Columna = nombreFicha.Substring(nombreFicha.LastIndexOf('_') + 1);
        Columna=Columna.Replace(","+Fila,"");

        int columna = Convert.ToInt32(Columna);
        int fila = Convert.ToInt32(Fila);
        RevisarVecinos(columna,fila);
    }

    void RevisarVecinos(int col, int fil){
        //Determina cuales son las posiciones de las fichas vecinas que pueden ser objetivos
        //arriba
        string arriba= col+","+(fil-1);
        //abajo
        string abajo = col+","+(fil+1);
        //izquierda
        string izquierda = (col-1)+","+fil;
        //derecha
        string derecha = (col+1)+","+fil;
        IluminarVecinos(arriba,abajo,izquierda,derecha);
    }

    void IluminarVecinos(string arriba, string abajo, string izquierda,string derecha){
        //Muestra cuales son los vecinos que pueden ser objetivo final.
        GameObject fichaArriba = GameObject.Find("Ficha_"+arriba+"(Clone)");
        GameObject fichaAbajo = GameObject.Find("Ficha_"+abajo+"(Clone)");
        GameObject fichaIzquierda = GameObject.Find("Ficha_"+izquierda+"(Clone)");
        GameObject fichaDerecha = GameObject.Find("Ficha_"+derecha+"(Clone)");
        if(fichaArriba!=null){
            IluminaFicha(fichaArriba);
        }
        if(fichaAbajo!=null){
            IluminaFicha(fichaAbajo);
        }
        if(fichaIzquierda!=null){
            IluminaFicha(fichaIzquierda);
        }
        if(fichaDerecha!=null){
            IluminaFicha(fichaDerecha);
        }
    }

    void IluminaFicha(GameObject obj){
        obj.GetComponent<SpriteRenderer>().color=Color.green;
    }
}
