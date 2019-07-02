﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cuadro{
    public GameObject primeraLinea{get;set;}
    public GameObject segundaLinea{get;set;}
    public GameObject terceraLinea{get;set;}
    public GameObject cuartaLinea{get;set;}
}

public class GameController : MonoBehaviour
{   
    List<Cuadro> listaCuadros = new List<Cuadro>();
    void Update()
    {
        
    }

    public void EvaluarTablero(){
        //Traer todas las lineas
        //Traer primera coordenada de la linea
        //Revisar si existen las lineas con las coordenadas que corresponden a cerrar un cuadro, en el sentido del reloj y contra el sentido.
        //Si existen 4 lineas que correspondan con el set de coordenadas, marcar un cuadro.

        GameObject[] lineArray;
        lineArray = GameObject.FindGameObjectsWithTag("Line");

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
                EvaluarCuadro(fil,col);
            }
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
                        var algo = primeraLinea1==null ? cuadro.primeraLinea=primeraLinea1 : cuadro.primeraLinea=primeraLinea2;
                        var a2= segundaLinea1==null ? cuadro.segundaLinea=segundaLinea1 : cuadro.segundaLinea=segundaLinea1;
                        var a3= terceraLinea1==null ? cuadro.terceraLinea=terceraLinea1 : cuadro.terceraLinea=terceraLinea1;
                        var a4= cuartaLinea1==null ? cuadro.cuartaLinea=cuartaLinea1 : cuadro.cuartaLinea=cuartaLinea1;
                        if(listaCuadros.Find(n=> n.primeraLinea == cuadro.primeraLinea 
                                                && n.segundaLinea==cuadro.segundaLinea 
                                                && n.terceraLinea==cuadro.terceraLinea 
                                                && n.cuartaLinea==cuadro.cuartaLinea)==null)
                        {
                            Debug.Log("Hay un cuadro en "+col+","+fil);
                            listaCuadros.Add(cuadro);
                        }
                        


                    }
                }
            }

        }

        return hayUnCuadro;
    }
}
