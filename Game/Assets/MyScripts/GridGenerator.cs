using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject FichaGameObject;
    public int TotalColumnas;
    public int TotalFilas;
    public float PosicionXInicial;
    public float PosicionYInicial;

    void Start()
    {
        CrearGrid();
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
}
