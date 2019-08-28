/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleConsecutivoController : MonoBehaviour
{
    int columnas;
    int filas;
    CuadCapturadoController [,] matriz;
    //Obtener todos los cuadros de la matriz
    void Start(){
        GameObject[] cuadrosCapturados = GameObject.FindGameObjectsWithTag("CuadCapturado");
        //transformarlos a la matriz
        columnas = this.GetComponent<GridGenerator>().GetTotalColumnas();
        filas = this.GetComponent<GridGenerator>().GetTotalFilas();
        matriz[,] = new CuadCapturadoController[filas,columnas];
    }

    void PintarConsecutivos(){
//--------- AQUI ES DONDE SE EMPIEZAN A PINTAR YA LOS CAUDRITOS CUANDO SON CONSECUTIVOS-----------------


//SE INICIA RECORRIENDO LAS X PARA SACAR LOS CONSECUTIVOS HORIZONTALES (SIEMPRE DE DERECHA A IZQ) 

for(int y=1;x<= mat[y].lenght();y++) // recorer las Y
{
	for(int x=1;x<= mat[x].lenght();x++)//recorrer las X
	{
		//MAGOS
		if (player.raza == "Magos")
		{
			if(cuadro[x][y].GetEsContinuo() == false && cuadro[x+1][y].GetEsContinuo() == false && x+1 <= mat[x].lenght())
			 // Si no es ya consecutivo y hay espacios (2) hacia el final... entra a la condición
			{
				if(mat[x][y] == mat[x+1][y])
				//Si los cuadros son del mismo jugador
				
				consecutivo[x][y].SetEsContinuo(true);
				consecutivo[x+1][y].SetEsContinuo(true);
            }
		}			
		
		//HUMANOS
		if (player.raza == "Humanos")
		{
			if(cuadro[x][y].GetEsContinuo() == false && cuadro[x+1][y].GetEsContinuo() == false && cuadro[x+2][y].GetEsContinuo() == false && x+2 <= mat[x].lenght()) 
			// Si no es ya consecutivo y hay espacios (3) hacia el final... entra a la condición
			{
				if(mat[x][y] == mat[x+1][y] == mat[x+2][y])
				//Si los cuadros son del mismo jugador
				
				consecutivo[x][y].SetEsContinuo(true);
				consecutivo[x+1][y].SetEsContinuo(true);
				consecutivo[x+2][y].SetEsContinuo(true);
			}
		}		
		//PIEDRA
		if (player.raza == "Piedra")
		{
			if(cuadro[x][y].GetEsContinuo() == false && cuadro[x+1][y].GetEsContinuo() == false && cuadro[x+2][y].GetEsContinuo() == false && cuadro[x+3][y].GetEsContinuo() == false && x+3 <= mat[x].lenght())
			 // Si no es ya consecutivo y hay espacios (4) hacia el final... entra a la condición
			{
				if(mat[x][y] == mat[x+1][y] == mat[x+2][y] == mat[x+3][y])
				//Si los cuadros son del mismo jugador
				
				consecutivo[x][y].SetEsContinuo(true);
				consecutivo[x+1][y].SetEsContinuo(true);
				consecutivo[x+2][y].SetEsContinuo(true);
				consecutivo[x+3][y].SetEsContinuo(true);
			}
		}
		
	}
}

//SE INICIA RECORRIENDO LAS Y PARA SACAR LOS CONSECUTIVOS VERTICALES (SIEMPRE DE ARRIBA HACIA ABAJO)

for(int x=1;x<= mat[x].lenght();x++)//recorrer las X
{
	for(int y=1;x<= mat[y].lenght();y++) // recorer las Y
	{
		//MAGOS
		if (player.raza == "Magos")
		{
			if(cuadro[x][y].GetEsContinuo() == false && cuadro[x][y+1].GetEsContinuo() == false && y+1 <= mat[y].lenght())
			 // Si no es ya consecutivo y hay espacios (2) hacia el final... entra a la condición
			{
				if(mat[x][y] == mat[x][y+1])
				//Si los cuadros son del mismo jugador
				
				consecutivo[x][y].SetEsContinuo(true);
				consecutivo[x][y+1].SetEsContinuo(true);
				
			}
		}
		//HUMANOS
		if (player.raza == "Humanos")
		{
			if(cuadro[x][y].GetEsContinuo() == false && cuadro[x][y+1].GetEsContinuo() == false && cuadro[x][y+2].GetEsContinuo() == false && y+2 <= mat[y].lenght()) 
			// Si no es ya consecutivo y hay espacios (3) hacia el final... entra a la condición
			{
				if(mat[x][y] == mat[x][y+1] == mat[x][y+2])
				//Si los cuadros son del mismo jugador
				
				consecutivo[x][y].SetEsContinuo(true);
				consecutivo[x][y+1].SetEsContinuo(true);
				consecutivo[x][y+2].SetEsContinuo(true);
				
			}
		}
		//PIEDRA
		if (player.raza == "Piedra")
		{
			if(cuadro[x][y].GetEsContinuo() == false && cuadro[x][y+1].GetEsContinuo() == false && cuadro[x][y+2].GetEsContinuo() == false && cuadro[x][y+3].GetEsContinuo() == false && y+3 <= mat[y].lenght())
			 // Si no es ya consecutivo y hay espacios (4) hacia el final... entra a la condición
			{
				if(mat[x][y] == mat[x][y+1] == mat[x][y+2] == mat[x][y+3])
				//Si los cuadros son del mismo jugador
				{
				consecutivo[x][y].SetEsContinuo(true);
				consecutivo[x][y+1].SetEsContinuo(true);
				consecutivo[x][y+2].SetEsContinuo(true);
				consecutivo[x][y+3].SetEsContinuo(true);
				}
			}
		}
	}
}
    }
}

*/
