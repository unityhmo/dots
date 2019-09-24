﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject FichaGameObject;
    public GameObject LineaGameObject;
    public GameObject ItemAgregarEnergia;
    public GameObject ItemQuitarEnergia;
    public GameObject Cofre;
    public GameObject ItemMultiplicador;

    public int TotalColumnas;
    public int TotalFilas;
    public float PosicionXInicial;
    public float PosicionYInicial;

    void Start()
    {
        CrearGrid();
        CrearLineasVacias();
    }

    public int GetTotalColumnas(){
        return TotalColumnas;
    }
    public int GetTotalFilas(){
        return TotalFilas;
    }

    void CrearGrid(){
        for(int x=0;x<TotalColumnas;x++){
            for(int y=0;y<TotalFilas;y++){
                //Instanciar una ficha, ponerle el nombre de su coordenada.
                FichaGameObject.name="Ficha_"+x+","+y;
                // "Y" es negativa para ordenar mejor las filas y columnas.
                Instantiate(FichaGameObject, new Vector3(PosicionXInicial+x, PosicionYInicial, -y),Quaternion.Euler(-90,0,0));
            }
        }
    }

    void CrearLineasVacias(){
        for(int x=0;x<TotalColumnas;x++){
            for(int y=0;y<TotalFilas;y++){
                if(x<TotalColumnas-1){
                    LineaGameObject.name="Line_["+x+","+y+"]-["+(x+1)+","+y+"]";
                    Instantiate(LineaGameObject, new Vector3(PosicionXInicial+x+0.5f, PosicionYInicial, -y), Quaternion.Euler(-90,0,0));
                    Vector3 posicionItem = Vector3.Lerp(new Vector3(PosicionXInicial+x+0.5f, PosicionYInicial, -y-0.5f),LineaGameObject.transform.position , 0f);
                    int random = (int)Random.Range(1f, 10.0f);
                    GameObject itemRandom = ItemAgregarEnergia;
                    if(random>=5){
                        itemRandom=ItemQuitarEnergia;
                    }
                    if(random>=8){
                        itemRandom=ItemMultiplicador;
                    }
                    if(random<=4){
                        itemRandom=Cofre;
                    }
                    if(x%random==0&&y<TotalFilas-1){
                         
                         if(x==0){
                             int otroRandom = (int)Random.Range(1f, 10.0f);
                             if(random==otroRandom)
                             if(itemRandom!=Cofre){
                             Instantiate(itemRandom, posicionItem, Quaternion.identity);
                             }else{
                                 Instantiate(itemRandom, posicionItem, Quaternion.Euler(0,180,0));
                             }
                         }else{
                             if(itemRandom!=Cofre){
                             Instantiate(itemRandom, posicionItem, Quaternion.identity);
                             }else{
                                 Instantiate(itemRandom, posicionItem, Quaternion.Euler(0,180,0));
                             }
                         }
                         
                    }
                        
                }
                if(y<TotalFilas-1){
                    LineaGameObject.name="Line_["+x+","+y+"]-["+x+","+(y+1)+"]";
                    Instantiate(LineaGameObject, new Vector3(PosicionXInicial+x, PosicionYInicial, -y-0.5f), Quaternion.Euler(-90,0,90));
                }
            }
        }

    }
}