using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class ConsecutivosController : MonoBehaviour
{
    /*
        Toda la lógica de los cuads consecutivos va en su propio archivo porque me está volviendo loco y quiero que sea fácil quitarla.
     */

    string RazaJugador1;
    string RazaJugador2;
    int JugadorActual;

    List<GameObject> listaConsecutivosJugador1;
    List<GameObject> listaConsecutivosJugador2;

    public void TransformarCuadrosConsecutivos(Vector3 cuadroActual, bool cambiarJugador=false){
        int cuadrosConsecutivos =0;
        JugadorActual=GetComponent<GameController>().GetJugadorActual();
        int jugador=JugadorActual;
        if(cambiarJugador){
            if(jugador==1){
                jugador=2;
            }else{
                jugador=1;
            }
        }
        cuadrosConsecutivos=GetCuadrosConsecutivos(jugador);
        float xActual = cuadroActual.x;
        float yActual = cuadroActual.z;
        int contador=1;
        //Buscar a la derecha
        bool esConsecutivoDerecha=false;
        for(int x=1;x<cuadrosConsecutivos;x++){
            if(BuscarCuadroCapturado(xActual-x,yActual,cambiarJugador)){
                esConsecutivoDerecha=true;
            }else{
                esConsecutivoDerecha=false;
                break;
            }
        }

        //Buscar a la izquierda
        bool esConsecutivoIzquierda=false;
        for(int x=1;x<cuadrosConsecutivos;x++){
            if(BuscarCuadroCapturado(xActual+x,yActual,cambiarJugador)){
                esConsecutivoIzquierda=true;
            }else{
                esConsecutivoIzquierda=false;
                break;
            }
        }

        //Buscar para arriba
        bool esConsecutivoArriba=false;
        for(int x=1;x<cuadrosConsecutivos;x++){
            if(BuscarCuadroCapturado(xActual,yActual+x,cambiarJugador)){
                esConsecutivoArriba=true;
            }else{
                esConsecutivoArriba=false;
                break;
            }
        }

        //Buscar para abajo
        bool esConsecutivoAbajo=false;
        for(int x=1;x<cuadrosConsecutivos;x++){
            if(BuscarCuadroCapturado(xActual,yActual-x,cambiarJugador)){
                esConsecutivoAbajo=true;
            }else{
                esConsecutivoAbajo=false;
                break;
            }
        }

        //bool buscar uno arriba y uno abajo
        bool esConsecutivoArribaAbajo=false;
        if(BuscarCuadroCapturado(xActual,yActual-1,cambiarJugador)){
            if(BuscarCuadroCapturado(xActual,yActual+1,cambiarJugador)){
                if(BuscarCuadroCapturado(xActual,yActual,cambiarJugador)){
                    esConsecutivoArribaAbajo=true;
                }
            }
        }

        //Debug.Log("esConsecutivoDerecha: "+esConsecutivoDerecha);
        //Debug.Log("esConsecutivoIzquierda: "+esConsecutivoIzquierda);
        //Debug.Log("esConsecutivoArriba: "+esConsecutivoArriba);
        //Debug.Log("esConsecutivoAbajo: "+esConsecutivoAbajo);
        
        MarcarConsecutivos(cuadrosConsecutivos, esConsecutivoAbajo, esConsecutivoArriba, esConsecutivoDerecha, esConsecutivoIzquierda, esConsecutivoArribaAbajo,xActual,yActual);
    }

    public void LimpiarCapturados(){
        GameObject[] cuadrosCapturados = GameObject.FindGameObjectsWithTag("CuadCapturado");
        foreach(GameObject cuadro in cuadrosCapturados){
            cuadro.GetComponent<CuadCapturadoController>().SetEsContinuo(false);
        }        
    }

    public void ContarContinuos(GameObject cuadro,bool cambiarJugador=false){
        string nombreFicha=cuadro.name;
        int jugador=1;
        if(cuadro.name.Contains("J2")){
            jugador=2;
        }
        if(cambiarJugador==true){
            if(jugador==1){
                jugador=2;
            }else{
                jugador=1;
            }
        }
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

        RevisarVecinos(jugador,x,z);
    }

    void RevisarVecinos(int jugador, float x, float z){
        int cuadrosConsecutivos=GetCuadrosConsecutivos(jugador);
        int consecutivosActuales=0;
        
        //buscar izquierda
        List<GameObject> listaIzquierda = new List<GameObject>();
        bool izquierda=false;
        for(int contador=0;contador<cuadrosConsecutivos;contador++){
            string vecino ="Conquered_J"+jugador+"$x="+(x-contador).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"#z="+z.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"&(Clone)";
            GameObject vecinoObject = GameObject.Find(vecino);
            if(vecinoObject!=null){
                bool esContinuo = vecinoObject.GetComponent<CuadCapturadoController>().GetEsContinuo();
                if(esContinuo==false){
                    consecutivosActuales++;                    
                }
                listaIzquierda.Add(vecinoObject);
            }else{
                consecutivosActuales=0;
                break;
            }
        }
        if(consecutivosActuales==cuadrosConsecutivos){
            foreach(GameObject vecino in listaIzquierda){
                vecino.GetComponent<CuadCapturadoController>().SetEsContinuo(true);
            }
        }else{
             foreach(GameObject vecino in listaIzquierda){
                vecino.GetComponent<CuadCapturadoController>().SetEsContinuo(false);
            }
        }

        consecutivosActuales=0;
        //buscar derecha
        List<GameObject> listaDerecha = new List<GameObject>();
        for(int contador=0;contador<cuadrosConsecutivos;contador++){
            string vecino ="Conquered_J"+jugador+"$x="+(x+contador).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"#z="+z.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"&(Clone)";
            GameObject vecinoObject = GameObject.Find(vecino);
            if(vecinoObject!=null){
                bool esContinuo = vecinoObject.GetComponent<CuadCapturadoController>().GetEsContinuo();
                if(esContinuo==false){
                    consecutivosActuales++;
                    listaDerecha.Add(vecinoObject);
                }
            }else{
                consecutivosActuales=0;
                break;
            }
        }
        if(consecutivosActuales==cuadrosConsecutivos){
            foreach(GameObject vecino in listaDerecha){
                vecino.GetComponent<CuadCapturadoController>().SetEsContinuo(true);
            }
        }

        consecutivosActuales=0;
        //buscar arriba
        List<GameObject> listaArriba = new List<GameObject>();
        for(int contador=0;contador<cuadrosConsecutivos;contador++){
            string vecino ="Conquered_J"+jugador+"$x="+x.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"#z="+(z+contador).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"&(Clone)";
            GameObject vecinoObject = GameObject.Find(vecino);
            if(vecinoObject!=null){
                bool esContinuo = vecinoObject.GetComponent<CuadCapturadoController>().GetEsContinuo();
                if(esContinuo==false){
                    consecutivosActuales++;
                    listaArriba.Add(vecinoObject);
                }
            }else{
                consecutivosActuales=0;
                break;
            }
        }
        if(consecutivosActuales==cuadrosConsecutivos){
            foreach(GameObject vecino in listaArriba){
                vecino.GetComponent<CuadCapturadoController>().SetEsContinuo(true);
            }
        }

        consecutivosActuales=0;
        //buscar abajo
        List<GameObject> listaAbajo = new List<GameObject>();
        for(int contador=0;contador<cuadrosConsecutivos;contador++){
            string vecino ="Conquered_J"+jugador+"$x="+x.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"#z="+(z-contador).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"&(Clone)";
            GameObject vecinoObject = GameObject.Find(vecino);
            if(vecinoObject!=null){
                bool esContinuo = vecinoObject.GetComponent<CuadCapturadoController>().GetEsContinuo();
                if(esContinuo==false){
                    consecutivosActuales++;
                    listaAbajo.Add(vecinoObject);
                }
            }else{
                consecutivosActuales=0;
                break;
            }
        }
        if(consecutivosActuales==cuadrosConsecutivos){
            foreach(GameObject vecino in listaAbajo){
                vecino.GetComponent<CuadCapturadoController>().SetEsContinuo(true);
            }
        }

        //buscar izquierda y derecha / arriba y abajo
        consecutivosActuales=0;
        //TODO: hacer esto con consecutivos=4 para los piedra
        if(cuadrosConsecutivos==3){
        List<GameObject> listaIzquierdaDerecha = new List<GameObject>();
        for(int contador=0;contador<cuadrosConsecutivos;contador++){
            string vecino ="Conquered_J"+jugador+"$x="+(x+contador).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"#z="+z.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"&(Clone)";
            GameObject vecinoObject = GameObject.Find(vecino);
            string vecino2 ="Conquered_J"+jugador+"$x="+(x-contador).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"#z="+z.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"&(Clone)";
            GameObject vecinoObject2 = GameObject.Find(vecino2);
            if(vecinoObject!=null&&vecinoObject2!=null){
                bool esContinuo = vecinoObject.GetComponent<CuadCapturadoController>().GetEsContinuo();
                esContinuo = vecinoObject2.GetComponent<CuadCapturadoController>().GetEsContinuo();
                if(esContinuo==false){
                    listaIzquierdaDerecha.Add(vecinoObject);
                    listaIzquierdaDerecha.Add(vecinoObject2);
                    consecutivosActuales+=2;
                }
            }else{
                //consecutivosActuales=0;
                break;
            }
        }
        if(consecutivosActuales>cuadrosConsecutivos){
            foreach(GameObject vecino in listaIzquierdaDerecha){
                vecino.GetComponent<CuadCapturadoController>().SetEsContinuo(true);
            }
        }
        consecutivosActuales=0;
        List<GameObject> listaArribaAbajo = new List<GameObject>();
        for(int contador=0;contador<cuadrosConsecutivos;contador++){
            string vecino ="Conquered_J"+jugador+"$x="+x.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"#z="+(z+contador).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"&(Clone)";
            GameObject vecinoObject = GameObject.Find(vecino);
            string vecino2 ="Conquered_J"+jugador+"$x="+x.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"#z="+(z-contador).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"&(Clone)";
            GameObject vecinoObject2 = GameObject.Find(vecino2);
            if(vecinoObject!=null&&vecinoObject2!=null){
                bool esContinuo = vecinoObject.GetComponent<CuadCapturadoController>().GetEsContinuo();
                esContinuo = vecinoObject2.GetComponent<CuadCapturadoController>().GetEsContinuo();
                if(esContinuo==false){
                    listaArribaAbajo.Add(vecinoObject);
                    listaArribaAbajo.Add(vecinoObject2);
                    consecutivosActuales+=2;
                }
            }else{
                //consecutivosActuales=0;
                break;
            }
        }
        if(consecutivosActuales>cuadrosConsecutivos){
            foreach(GameObject vecino in listaArribaAbajo){
                vecino.GetComponent<CuadCapturadoController>().SetEsContinuo(true);
            }
        }
        }
    }

    void MarcarConsecutivos(int cuadrosConsecutivos, bool esConsecutivoAbajo, bool esConsecutivoArriba, bool esConsecutivoDerecha, bool esConsecutivoIzquierda, bool esConsecutivoArribaAbajo, float xActual, float yActual){
        //Buscar a la derecha
        if(esConsecutivoDerecha){
            for(int x=0;x<cuadrosConsecutivos;x++){
            CambiarColorConsecutivo(xActual-x,yActual);
            }
        }
        //Buscar a la izquierda
        if(esConsecutivoIzquierda){
            for(int x=0;x<cuadrosConsecutivos;x++){
            CambiarColorConsecutivo(xActual+x,yActual);
            }
        }
      
        //Buscar para arriba
         if(esConsecutivoArriba){
            for(int x=0;x<cuadrosConsecutivos;x++){
            CambiarColorConsecutivo(xActual,yActual+x);
            }
        }
        //Buscar para abajo
         if(esConsecutivoAbajo){
            for(int x=0;x<cuadrosConsecutivos;x++){
            CambiarColorConsecutivo(xActual,yActual-x);
            }
        }
        //Buscar arriba abajo
        if(esConsecutivoArribaAbajo){
            if(JugadorActual==1&&RazaJugador1=="Humano"||JugadorActual==2&&RazaJugador2=="Humano"){
                CambiarColorConsecutivo(xActual,yActual+1);
                CambiarColorConsecutivo(xActual,yActual);
                CambiarColorConsecutivo(xActual,yActual-1);
            }
        }
        
    }

        public bool BuscarCuadroCapturado(float x,float z, bool cambiarJugador=false){
        GameObject[] cuadrosCapturados = GameObject.FindGameObjectsWithTag("CuadCapturado");
        int cuadroActual=0;
        int jugador=JugadorActual;
            if(cambiarJugador){
                if(jugador==1){
                    jugador=2;
                }else{
                    jugador=1;
                }
            }
        int cuadrosConsecutivos=GetCuadrosConsecutivos(jugador);
        bool encontrado=false;
        foreach(GameObject cuadro in cuadrosCapturados){
            if(cuadroActual<=cuadrosConsecutivos){
            if(cuadro.transform.position.x==x &&cuadro.transform.position.z==z){
                if(cuadro.name.Contains("J"+jugador)){
                    encontrado= true;
                }else{
                    //cuadro.GetComponent<CuadCapturadoController>().SetEsContinuo(false);
                }
            }
            }
            cuadroActual++;
        }
        return encontrado;
    }

    void CambiarColorConsecutivo(float x,float z){
        GameObject[] cuadrosCapturados = GameObject.FindGameObjectsWithTag("CuadCapturado");
        foreach(GameObject cuadro in cuadrosCapturados){
            if(cuadro.transform.position.x==x &&cuadro.transform.position.z==z){
                //if(cuadro.name.Contains("J"+JugadorActual)){
                    //bool esContinuo = cuadro.GetComponent<CuadCapturadoController>().GetEsContinuo();
                    //if(!esContinuo){
                        cuadro.GetComponent<CuadCapturadoController>().SetEsContinuo(true);       
                    //} 
                //}
            }           

        }
    }

    int GetCuadrosConsecutivos(int jugador){
        string raza="";
        int consecutivos=0;
        if(jugador==1){
            raza=GetComponent<GameController>().GetRazaJugador1();
        }else{
            raza=GetComponent<GameController>().GetRazaJugador2();
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

    public void ContarSetsConsecutivos(){
        int cuadrosConsecutivosTotales =0;
        int JugadorActual=GetComponent<GameController>().GetJugadorActual();
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
                    GetComponent<GameController>().SubirEnergiaJugador1();
                }else{
                    GetComponent<GameController>().SubirEnergiaJugador2();
                }
            }
        } 
    }
}
