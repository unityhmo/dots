using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject FichaGameObject;
    public GameObject LineaGameObject;
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
                Instantiate(FichaGameObject, new Vector3(PosicionXInicial+x, PosicionYInicial-y, 0), Quaternion.identity);
            }
        }
    }

    void CrearLineasVacias(){
        for(int x=0;x<TotalColumnas;x++){
            for(int y=0;y<TotalFilas;y++){
                if(x<TotalColumnas-1){
                    LineaGameObject.name="Line_["+x+","+y+"]-["+(x+1)+","+y+"]";
                    Instantiate(LineaGameObject, new Vector3(PosicionXInicial+x+0.5f, PosicionYInicial-y, 0), Quaternion.identity);
                }
                if(y<TotalFilas-1){
                    LineaGameObject.name="Line_["+x+","+y+"]-["+x+","+(y+1)+"]";
                    Instantiate(LineaGameObject, new Vector3(PosicionXInicial+x, PosicionYInicial-y-0.5f, 0), Quaternion.Euler(0,0,90));
                }
            }
        }

    }
}
