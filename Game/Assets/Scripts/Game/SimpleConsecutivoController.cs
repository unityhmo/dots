 /*
 Matriz
 x/z
 [-2,-0.5],[-1,-0.5][0,-],[1,-],[2,-]
 [-2,-1.5],[-1,-1.5]
 [-2,-2.5]
 [-2,-3.5]
 [-2,-4.5]
  */
 
 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class SimpleConsecutivoController : MonoBehaviour
{
    int columnas;
    int filas;
    CuadCapturadoController [,] cuadro;
    //Obtener todos los cuadros de la matriz

	string GetRazaActual(){
		int jugadorActual= GetJugadorActual();
		string raza="";
		if(jugadorActual==1){
			raza=this.GetComponent<GameController>().GetRazaJugador1();
		}else{
			raza=this.GetComponent<GameController>().GetRazaJugador2();
		}
		return raza;
	}

	int GetJugadorActual(){
		return this.GetComponent<GameController>().GetJugadorActual();
	}

	public void ActualizarConsecutivos(){
		GameObject[] cuadrosCapturados = GameObject.FindGameObjectsWithTag("CuadCapturado");
		//transformarlos a la matriz
        columnas = this.GetComponent<GridGenerator>().GetTotalColumnas();
        filas = this.GetComponent<GridGenerator>().GetTotalFilas();
		columnas-=1;
		filas-=1;
        cuadro = new CuadCapturadoController[filas,columnas];
		BorrarConsecutivos(cuadrosCapturados);
		foreach(GameObject objCuad in cuadrosCapturados){
			MeterCuadroAMatriz(objCuad);
		}
		PintarConsecutivos();
	}

	void BorrarConsecutivos(GameObject[] cuadrosCapturados){
		foreach(GameObject objCuad in cuadrosCapturados){
			objCuad.GetComponent<CuadCapturadoController>().SetEsContinuo(false);
		}
	}

	void MeterCuadroAMatriz(GameObject _cuadro){
		string nombreFicha=_cuadro.name;
		nombreFicha=nombreFicha.Replace("(Clone)","");
        string Fila = nombreFicha.Substring(nombreFicha.LastIndexOf('$') + 1);
        int pFrom = Fila.IndexOf("x= ") + "x= ".Length;
        int pTo = Fila.LastIndexOf("#");

        string equis = Fila.Substring(pFrom, pTo - pFrom);
        Fila = nombreFicha.Substring(nombreFicha.LastIndexOf('#') + 1);
        pFrom = Fila.IndexOf("#z=") + "#z=".Length;
        pTo = Fila.LastIndexOf("&");
        string zeta =  Fila.Substring(pFrom, pTo - pFrom);
            

        float x=float.Parse(equis, CultureInfo.InvariantCulture);
        float z = float.Parse(zeta, CultureInfo.InvariantCulture);
		
		int pFila=0;
		int pColumna=0;
		if(x==-2){
			pColumna=0;
		}
		if(x==-1){
			pColumna=1;
		}
		if(x==0){
			pColumna=2;
		}
		if(x==1){
			pColumna=3;
		}
		if(x==2){
			pColumna=4;
		}
		if(z==-0.5){
			pFila=0;
		}
		if(z==-1.5){
			pFila=1;
		}
		if(z==-2.5){
			pFila=2;
		}
		if(z==-3.5){
			pFila=3;
		}
		if(z==-4.5){
			pFila=4;
		}

		cuadro[pColumna,pFila]=_cuadro.GetComponent<CuadCapturadoController>();		
	}

	public void ContarSetsConsecutivos(){
        int cuadrosConsecutivosTotales =0;
        int JugadorActual=this.GetComponent<GameController>().GetJugadorActual();
        cuadrosConsecutivosTotales=GetCuadrosConsecutivos(JugadorActual);
        //traer cuadros consecutivos de ese jugador
        int cuadrosConsecutivosActuales = 0;
        GameObject[] cuadrosCapturados = GameObject.FindGameObjectsWithTag("CuadCapturado");
        foreach(GameObject cuadro in cuadrosCapturados){
            if(cuadro.name.Contains("J"+JugadorActual)){
                bool esContinuo = cuadro.GetComponent<CuadCapturadoController>().GetEsContinuo();
                if(esContinuo){
                    cuadrosConsecutivosActuales++;
                }
            }
        }
        if(cuadrosConsecutivosTotales>0){
            int sumarEnergiaActual = cuadrosConsecutivosActuales/cuadrosConsecutivosTotales;
            for(int x=0;x<sumarEnergiaActual;x++){
                if(JugadorActual==1){
                    this.GetComponent<GameController>().SubirEnergiaJugador1();
                }else{
                    this.GetComponent<GameController>().SubirEnergiaJugador2();
                }
            }
        } 
    }

	int GetCuadrosConsecutivos(int jugador){
        string raza="";
        int consecutivos=0;
        if(jugador==1){
            raza=this.GetComponent<GameController>().GetRazaJugador1();
        }else{
            raza=this.GetComponent<GameController>().GetRazaJugador2();
        }
        if(raza=="Humano"){
            consecutivos=3;
        }
        if(raza=="Mago"){
            consecutivos=2;
        }
        if(raza=="Piedra"){
            consecutivos=4;
        }
        return consecutivos;
    }

    void PintarConsecutivos()
	{
//--------- AQUI ES DONDE SE EMPIEZAN A PINTAR YA LOS CAUDRITOS CUANDO SON CONSECUTIVOS-----------------


//SE INICIA RECORRIENDO LAS X PARA SACAR LOS CONSECUTIVOS HORIZONTALES (SIEMPRE DE DERECHA A IZQ) 
string raza=GetRazaActual();
int jugadorActual =GetJugadorActual();
for(int y=0;y< filas;y++) // recorer las Y (Z)
{
	for(int x=0;x< columnas;x++)//recorrer las X
	{
		//MAGOS
		if (raza == "Mago")
		{
			if(x-1>=0){
			if(cuadro[x,y]!=null&&cuadro[x-1,y]!=null){
				if(cuadro[x,y].GetEsContinuo() == false && cuadro[x-1,y].GetEsContinuo() == false && x-1 <= filas)
			 	// Si no es ya consecutivo y hay espacios (2) hacia el final... entra a la condición
				{
					if(cuadro[x,y].numeroJugador == cuadro[x-1,y].numeroJugador&&jugadorActual==cuadro[x,y].numeroJugador){
					//Si los cuadros son del mismo jugador				
					cuadro[x,y].SetEsContinuo(true);
					cuadro[x-1,y].SetEsContinuo(true);
					}
					else{
						cuadro[x,y].SetEsContinuo(false);
						cuadro[x-1,y].SetEsContinuo(false);
					}
            	}
			}
			}
		}			
		
		//HUMANOS
		if (raza == "Humano")
		{
			if(x-2>=0){
			if(cuadro[x,y]!=null&&cuadro[x-1,y]!=null&&cuadro[x-2,y]!=null){
				if(cuadro[x,y].GetEsContinuo() == false && cuadro[x-1,y].GetEsContinuo() == false && cuadro[x-2,y].GetEsContinuo() == false && x-2 <= filas) 
				// Si no es ya consecutivo y hay espacios (3) hacia el final... entra a la condición
				{
					if(cuadro[x,y].numeroJugador == cuadro[x-1,y].numeroJugador && cuadro[x-2,y].numeroJugador==cuadro[x,y].numeroJugador&&jugadorActual==cuadro[x,y].numeroJugador){
					//Si los cuadros son del mismo jugador				
					cuadro[x,y].SetEsContinuo(true);
					cuadro[x-1,y].SetEsContinuo(true);
					cuadro[x-2,y].SetEsContinuo(true);
					}else{
						cuadro[x,y].SetEsContinuo(false);
						cuadro[x-1,y].SetEsContinuo(false);
						cuadro[x-2,y].SetEsContinuo(false);
					}

				}
			}
			}
		}		
		//PIEDRA
		if (raza == "Piedra")
		{
			if(x-3>=0){
			if(cuadro[x,y]!=null&&cuadro[x-1,y]!=null&&cuadro[x-2,y]!=null&&cuadro[x-3,y]!=null){
				if(cuadro[x,y].GetEsContinuo() == false && cuadro[x-1,y].GetEsContinuo() == false && cuadro[x-2,y].GetEsContinuo() == false && cuadro[x-3,y].GetEsContinuo() == false && x-3 <= filas)
			 	// Si no es ya consecutivo y hay espacios (4) hacia el final... entra a la condición
				{
					if(cuadro[x,y].numeroJugador == cuadro[x-1,y].numeroJugador && cuadro[x-2,y].numeroJugador == cuadro[x-3,y].numeroJugador && cuadro[x,y].numeroJugador==cuadro[x-2,y].numeroJugador&&jugadorActual==cuadro[x,y].numeroJugador){
					//Si los cuadros son del mismo jugador				
					cuadro[x,y].SetEsContinuo(true);
					cuadro[x-1,y].SetEsContinuo(true);
					cuadro[x-2,y].SetEsContinuo(true);
					cuadro[x-3,y].SetEsContinuo(true);
					}
					else{
					cuadro[x,y].SetEsContinuo(false);
					cuadro[x-1,y].SetEsContinuo(false);
					cuadro[x-2,y].SetEsContinuo(false);
					cuadro[x-3,y].SetEsContinuo(false);
					}
				}
			}
			}
		}
		
	}
}

//SE INICIA RECORRIENDO LAS Y PARA SACAR LOS CONSECUTIVOS VERTICALES (SIEMPRE DE ARRIBA HACIA ABAJO)

for(int x=0;x< filas;x++)//recorrer las X
{
	for(int y=0;y< columnas;y++) // recorer las Y
	{
		//MAGOS
		if (raza == "Mago")
		{
			if(y-1>=0){
			if(cuadro[x,y]!=null&&cuadro[x,y-1]!=null){
				if(cuadro[x,y].GetEsContinuo() == false && cuadro[x,y-1].GetEsContinuo() == false && y-1 <= columnas)
			 	// Si no es ya consecutivo y hay espacios (2) hacia el final... entra a la condición
				{
					if(cuadro[x,y].numeroJugador == cuadro[x,y-1].numeroJugador&&jugadorActual==cuadro[x,y].numeroJugador){
					//Si los cuadros son del mismo jugador				
					cuadro[x,y].SetEsContinuo(true);
					cuadro[x,y-1].SetEsContinuo(true);
					}
					else{
					cuadro[x,y].SetEsContinuo(false);
					cuadro[x,y-1].SetEsContinuo(false);
					}
				
				}
			}
			}
		}
		//HUMANOS
		if (raza == "Humano")
		{
			if(y-2>=0){
			if(cuadro[x,y]!=null&&cuadro[x,y-1]!=null&&cuadro[x,y-2]!=null){
				if(cuadro[x,y].GetEsContinuo() == false && cuadro[x,y-1].GetEsContinuo() == false && cuadro[x,y-2].GetEsContinuo() == false && y-2 <= columnas) 
				// Si no es ya consecutivo y hay espacios (3) hacia el final... entra a la condición
				{
					if(cuadro[x,y].numeroJugador == cuadro[x,y-1].numeroJugador && cuadro[x,y-2].numeroJugador==cuadro[x,y].numeroJugador&&jugadorActual==cuadro[x,y].numeroJugador){
					//Si los cuadros son del mismo jugador				
					cuadro[x,y].SetEsContinuo(true);
					cuadro[x,y-1].SetEsContinuo(true);
					cuadro[x,y-2].SetEsContinuo(true);
					}
					else{
					cuadro[x,y].SetEsContinuo(false);
					cuadro[x,y-1].SetEsContinuo(false);
					cuadro[x,y-2].SetEsContinuo(false);						
					}
				
				}
			}
			}


		}
		//PIEDRA
		if (raza == "Piedra")
		{
			if(y-3>=0){
			if(cuadro[x,y]!=null&&cuadro[x,y-1]!=null&&cuadro[x,y-2]!=null&&cuadro[x,y-3]!=null){
				if(cuadro[x,y].GetEsContinuo() == false && cuadro[x,y-1].GetEsContinuo() == false && cuadro[x,y-2].GetEsContinuo() == false && cuadro[x,y-3].GetEsContinuo() == false && y-3 <= columnas)
			 	// Si no es ya consecutivo y hay espacios (4) hacia el final... entra a la condición
				{
					if(cuadro[x,y].numeroJugador == cuadro[x,y-1].numeroJugador && cuadro[x,y-2].numeroJugador == cuadro[x,y-3].numeroJugador && cuadro[x,y].numeroJugador==cuadro[x,y-2].numeroJugador&&jugadorActual==cuadro[x,y].numeroJugador)
					//Si los cuadros son del mismo jugador
					{
					cuadro[x,y].SetEsContinuo(true);
					cuadro[x,y-1].SetEsContinuo(true);
					cuadro[x,y-2].SetEsContinuo(true);
					cuadro[x,y-3].SetEsContinuo(true);
					}else{
					cuadro[x,y].SetEsContinuo(false);
					cuadro[x,y-1].SetEsContinuo(false);
					cuadro[x,y-2].SetEsContinuo(false);
					cuadro[x,y-3].SetEsContinuo(false);				
					}
				}
			}
			}
		}
	}
}
}
}



