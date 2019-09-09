using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Globalization;
using System.Collections;

public class Cuadro{
    public GameObject primeraLinea{get;set;}
    public GameObject segundaLinea{get;set;}
    public GameObject terceraLinea{get;set;}
    public GameObject cuartaLinea{get;set;}
}

public class GameController : MonoBehaviour
{   
    List<Cuadro> listaCuadros = new List<Cuadro>();
    public GameObject txtPuntajeJugador1;
    public GameObject txtPuntajeJugador2;
    public GameObject txtContadorLineas;
    public GameObject txtMensajeFinal;

    public Text txtPuntajeExtra1;
    public Text txtPuntajeExtra2;

    string RazaJugador1;
    string RazaJugador2;

    int puntosJugador1;
    int puntosJugador2;
    int puntosExtraJugador1;
    int puntosExtraJugador2;
    int JugadorActual=1;

    bool acabaDeHacerUnPunto=false;

    int TotalMaximoLineas;
    int LineasActuales;
    string TextoFinal = "Empate";

    public int energiaMaxima;
    public float energiaActualJugador1;
    public float energiaActualJugador2;

    bool bloqueadoJugador1;
    bool bloqueadoJugador2;

    bool robandoJugador1;
    bool robandoJugador2;

    bool bloqueandoJugador1;
    bool bloqueandoJugador2;

    bool blindandoJugador1;
    bool blindandoJugador2;

    bool esMismoTurno;

    public Sprite Humano_Portrait;
    public Sprite Piedra_Portrait;
    public Sprite Mago_Portrait;

    public Sprite Humano_FullBody;
    public Sprite Piedra_FullBody;
    public Sprite Mago_FullBody;

    public Sprite Humano_Background;
    public Sprite Piedra_Background;
    public Sprite Mago_Background;

    public GameObject CanvasSkill;
    public GameObject ContenedorSkill;
    public GameObject SkillBaground;
    public GameObject SkillCharacter;
    public Text txtNombreHabilidad;
    
    public GameObject BlackScreen_P1;
    public GameObject BlackScreen_P2;

    string habilidadJugador1;
    string habilidadJugador2;

    public GameObject barraEnergiaJugador1;
    public GameObject barraEnergiaJugador2;

    public GameObject barraConquistaJugador1;
    public GameObject barraConquistaJugador2;

    public Sprite Robar_Icon;
    public Sprite Bloquear_Icon;
    public Sprite Blindar_icon;
    public Sprite Bloqueado_Icon;

    public GameObject P1_Estado_1;
    public GameObject P1_Estado_2;
    public GameObject P2_Estado_1;
    public GameObject P2_Estado_2;

    int columnas;
    int filas;

    bool GameOver;

    void Start(){
        CalcularTotalMaximoDeLineas();
        txtMensajeFinal.SetActive(false);
        RazaJugador1=ValoresEntreEscenas.RazaJugador1;
        RazaJugador2=ValoresEntreEscenas.RazaJugador2;
        ElegirRazaConValores();
        AclararJugador(BlackScreen_P1);
        OscurecerJugador(BlackScreen_P2);
    }

    void ElegirRazaConValores(){
        if(RazaJugador1=="Humano"){
            ElegirHumano();
        }
        if(RazaJugador1=="Mago"){
            ElegirMago();
        }
        if(RazaJugador1=="Piedra"){
            ElegirPiedra();
        }
        if(RazaJugador2=="Humano"){
            ElegirHumano();
        }
        if(RazaJugador2=="Mago"){
            ElegirMago();
        }
        if(RazaJugador2=="Piedra"){
            ElegirPiedra();
        }
        if(RazaJugador1==""||RazaJugador1==null){
            ElegirHumano();
        }
        if(RazaJugador2==""||RazaJugador2==null){
            ElegirHumano();
        }
    }

    void Update()
    {
        txtPuntajeJugador1.GetComponent<Text>().text=""+puntosJugador1;
        txtPuntajeJugador2.GetComponent<Text>().text=""+puntosJugador2;
        txtPuntajeExtra1.text="+"+puntosExtraJugador1;
        txtPuntajeExtra2.text="+"+puntosExtraJugador2;

        txtContadorLineas.GetComponent<Text>().text="El numero maximo de lineas es: "+TotalMaximoLineas+ " Lineas actuales: "+LineasActuales;
        txtMensajeFinal.GetComponent<Text>().text=TextoFinal;

        ChecarEstados();
        ChecarEnergia();
        ChecarConquista();
        
    }

    void ChecarEstados(){
        LimpiarEstados();
        if(bloqueadoJugador1){
            //estado1="Bloqueado";
            SetEstado(Bloqueado_Icon,1,false);
        }
        if(bloqueadoJugador2){
            //estado2="Bloqueado";
             SetEstado(Bloqueado_Icon,2,false);
        }

        if(robandoJugador1){
            //estado1+=" Robando";
             SetEstado(Robar_Icon,1,true);
        }
        if(robandoJugador2){
            //estado2+=" Robando";
             SetEstado(Robar_Icon,2,true);
        }
        if(bloqueandoJugador1){
            //estado1+=" Bloqueando";
             SetEstado(Bloquear_Icon,1,true);
        }
        if(bloqueandoJugador2){
            //estado2+= "Bloqueando";
            SetEstado(Bloquear_Icon,2,true);
        }
        if(blindandoJugador1){
            //estado1+=" Blindando";
            SetEstado(Blindar_icon,1,true);
        }
        if(blindandoJugador2){
            //estado2+= "Blindando";
            SetEstado(Blindar_icon,2,true);
        }
        
    }

    void LimpiarEstados(){
        P1_Estado_1.SetActive(false);
        P1_Estado_2.SetActive(false);
        P2_Estado_1.SetActive(false);
        P2_Estado_2.SetActive(false);
    }

    void SetEstado(Sprite icon,int jugador, bool estado2){
        if(jugador==1){
            if(estado2){
                P1_Estado_2.SetActive(true);
                P1_Estado_2.GetComponent<Image>().sprite = icon;
            }else{
                P1_Estado_1.SetActive(true);
                P1_Estado_1.GetComponent<Image>().sprite = icon;
            }
        } else{
            if(estado2){
                P2_Estado_2.SetActive(true);
                P2_Estado_2.GetComponent<Image>().sprite = icon;
            }else{
                P2_Estado_1.SetActive(true);
                P2_Estado_1.GetComponent<Image>().sprite = icon;
            }
        }    
    }

    void ChecarConquista(){
        float CuadrosTotales = (columnas-1)*(filas-1);
        float totalJ1= puntosJugador1+puntosExtraJugador1;
        float totalJ2 = puntosJugador2+puntosExtraJugador2;
        float cuadrosLibres= CuadrosTotales-totalJ1-totalJ2;
        float j1 = (totalJ1*100/CuadrosTotales)+(cuadrosLibres*100/CuadrosTotales)/2;
        float j2 = (totalJ2*100/CuadrosTotales)+(cuadrosLibres*100/CuadrosTotales)/2;
        ActualizarBarraEnergia(j1/100,barraConquistaJugador1);
        ActualizarBarraEnergia(j2/100,barraConquistaJugador2);

    }

    void ChecarEnergia()
	{
		float energia =  energiaActualJugador1/ energiaMaxima;
		ActualizarBarraEnergia (energia,barraEnergiaJugador1);
        energia =  energiaActualJugador2/ energiaMaxima;
		ActualizarBarraEnergia (energia,barraEnergiaJugador2);
	}

   	public void ActualizarBarraEnergia(float valorEnergia,GameObject barraEnergia){
		barraEnergia.transform.localScale = new Vector3(Mathf.Clamp(valorEnergia,0f ,1f), barraEnergia.transform.localScale.y, barraEnergia.transform.localScale.z);
	}

    void CalcularTotalMaximoDeLineas(){
        columnas = this.GetComponent<GridGenerator>().GetTotalColumnas();
        filas = this.GetComponent<GridGenerator>().GetTotalFilas();

        int colXFilas = columnas*filas;
        colXFilas=colXFilas+((columnas-1)*(filas-1))-1;

        TotalMaximoLineas=colXFilas;
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ElegirHumano(){
        string raza = "Humano";
        setRaza(raza);
    }

    public void ElegirMago(){
        string raza = "Mago";
        setRaza(raza);
    }

    public void ElegirPiedra(){
        string raza = "Piedra";
        setRaza(raza);
    }

    void SetHabilidad(string raza){
        string habilidad="Habilidad";
        if(raza=="Humano"){
            habilidad="Robar";
        }
        if(raza=="Mago"){
            habilidad="Bloquear";
        }
        if(raza=="Piedra"){
            habilidad="Blindar";
        }
        if(JugadorActual==1){
            habilidadJugador1=habilidad;
        }else{
            habilidadJugador2=habilidad;
        }
    }

    void setRaza(string raza){
        SetHabilidad(raza);
        if(JugadorActual==1){
            RazaJugador1=raza;
            GameObject portrait = GameObject.Find("Portrait_P1");
            SetPortrait(portrait,raza);
        }else{
            RazaJugador2=raza;
            GameObject portrait = GameObject.Find("Portrait_P2");
            SetPortrait(portrait,raza);
        }
        CambioDeTurno(JugadorActual);    
    }

    void SetPortrait(GameObject portrait,string raza){
        if(raza=="Humano"){
            portrait.GetComponent<Image>().sprite = Humano_Portrait;
        }
        if(raza=="Mago"){
            portrait.GetComponent<Image>().sprite = Mago_Portrait;
        }
        if(raza=="Piedra"){
           portrait.GetComponent<Image>().sprite = Piedra_Portrait;
        }       
    }

    public string GetRazaJugador1(){
        return RazaJugador1;
    }

    public string GetRazaJugador2(){
        return RazaJugador2;
    }

    public void SubirEnergiaJugador1(){
        if(energiaActualJugador1<energiaMaxima){
                energiaActualJugador1++;
            
        }
    }

    public void BajarEnergiaJugador1(){
        if(energiaActualJugador1>0){
            energiaActualJugador1--;
        }
    }

      public void BajarEnergiaJugador2(){
        if(energiaActualJugador2>0){
            energiaActualJugador2--;
        }
    }


    public void SubirEnergiaJugador2(){
        if(energiaActualJugador2<energiaMaxima){
                energiaActualJugador2++;
            
        }
    }

    public void BloquearAJugador1(){
        if(energiaActualJugador2==energiaMaxima){
            bloqueadoJugador1=true;
            energiaActualJugador2=0;
            ActivarAnimacionHabilidad("Bloquear",2);
        }
    }

    public void BloquearAJugador2(){
        if(energiaActualJugador1==energiaMaxima){
            bloqueadoJugador2=true;
            energiaActualJugador1=0;
            ActivarAnimacionHabilidad("Bloquear",1);
        }
    }

    public void UsarHabilidad(){
        if(JugadorActual==1){
            if(energiaActualJugador1==energiaMaxima){
                //Si puede usar la habilidad
                energiaActualJugador1=0;
                string habilidad = habilidadJugador1;
                if(habilidad=="Robar"){
                    SetPuedeRobarJugador1();
                }
                if(habilidad=="Bloquear"){
                    SetPuedeBloquearJugador1();
                    habilidad="Sellar";
                }
                if(habilidad=="Blindar"){
                    SetPuedeBlindarJugador1();
                }
                ActivarAnimacionHabilidad(habilidad,1);
            }
        }else{
             if(energiaActualJugador2==energiaMaxima){
                //Si puede usar la habilidad
                energiaActualJugador2=0;
                string habilidad = habilidadJugador2;
                if(habilidad=="Robar"){
                    SetPuedeRobarJugador2();
                }
                if(habilidad=="Bloquear"){
                    SetPuedeBloquearJugador2();
                    habilidad="Sellar";
                }
                if(habilidad=="Blindar"){
                    SetPuedeBlindarJugador2();
                }
                ActivarAnimacionHabilidad(habilidad,2);
            }
        }
    }

    void ActivarAnimacionHabilidad(string textoHabilidad,int jugador){
        txtNombreHabilidad.GetComponent<Text>().text=textoHabilidad;
        CanvasSkill.SetActive(true);
        string raza="";
        if(jugador==1){
            raza=GetRazaJugador1();
        }
        else{
            raza=GetRazaJugador2();
        }
        if(raza=="Humano"){
            StartCoroutine(CambiarBackgroundDespuesDeTiempo(Humano_Background));
            SkillCharacter.GetComponent<Image>().sprite=Humano_FullBody;
        }
        if(raza=="Mago"){
            StartCoroutine(CambiarBackgroundDespuesDeTiempo(Mago_Background));
            SkillCharacter.GetComponent<Image>().sprite=Mago_FullBody;
        }
        if(raza=="Piedra"){
            StartCoroutine(CambiarBackgroundDespuesDeTiempo(Piedra_Background));
            SkillCharacter.GetComponent<Image>().sprite=Piedra_FullBody;
        }
        if(jugador==2){
            ContenedorSkill.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            CanvasSkill.GetComponent<Animator>().Play("SkillAnimation_P2");
        }else{
            ContenedorSkill.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        StartCoroutine(DesactivarCanvasSkill());
    }

    IEnumerator DesactivarCanvasSkill()
    {
        yield return new WaitForSeconds(1);
        CanvasSkill.SetActive(false);
    }

    IEnumerator CambiarBackgroundDespuesDeTiempo(Sprite _pSprite)
    {
        SkillBaground.GetComponent<Image>().sprite=null;
        yield return new WaitForSeconds(0.2f);
        SkillBaground.GetComponent<Image>().sprite=_pSprite;
    }

    public void SetPuedeRobarJugador1(){
        robandoJugador1=true;
    }
    public void SetPuedeRobarJugador2(){
        robandoJugador2=true;
    }

    public void FinRoboJugador1(){
        robandoJugador1=false;
    }
    public void FinRoboJugador2(){
        robandoJugador2=false;
    }

    public void SetPuedeBloquearJugador1(){
        bloqueandoJugador1=true;
    }
    public void SetPuedeBloquearJugador2(){
        bloqueandoJugador2=true;
    }
    public void FinBloquearJugador1(){
        bloqueandoJugador1=false;
    }
    public void FinBloquearJugador2(){
        bloqueandoJugador2=false;
    }

    public void SetPuedeBlindarJugador1(){
        blindandoJugador1=true;
    }
    public void SetPuedeBlindarJugador2(){
        blindandoJugador2=true;
    }
    public void FinBlindarJugador1(){
        blindandoJugador1=false;
    }
    public void FinBlindarJugador2(){
        blindandoJugador2=false;
    }

    public int GetJugadorActual(){
        return JugadorActual;
    }

    public bool PuedeRobar(int jugador){
        bool respuesta=false;
        if(jugador==1){
            respuesta=robandoJugador1;
        }else{
            respuesta=robandoJugador2;
        }
        return respuesta;
    }

    public bool PuedeBloquear(int jugador){
        bool respuesta=false;
        if(jugador==1){
            respuesta=bloqueandoJugador1;
        }else{
            respuesta=bloqueandoJugador2;
        }
        return respuesta;
    }

    public bool PuedeBlindar(int jugador){
        bool respuesta=false;
        if(jugador==1){
            respuesta=blindandoJugador1;
        }else{
            respuesta=blindandoJugador2;
        }
        return respuesta;
    }

    public void SubirPuntajeJugador1(){
        puntosJugador1++;
    }
    public void SubirPuntajeJugador2(){
        puntosJugador2++;
    }
    public void BajarPuntajeJugador1(){
        puntosJugador1--;
    }
    public void BajarPuntajeJugador2(){
        puntosJugador2--;
    }

    public void SubirPuntajeExtraJugador1(int val){
        puntosExtraJugador1+=val;
    }
    public void SubirPuntajeExtraJugador2(int val){
        puntosExtraJugador2+=val;
    }
    public void BajarPuntajeExtraJugador1(){
        puntosExtraJugador1--;
    }
    public void BajarPuntajeExtraJugador2(){
        puntosExtraJugador2--;
    }

    public void VolverAlMenu(){
       SceneManager.LoadScene("02-main-menu");
    }

    public void EvaluarTablero(){
        //Traer todas las lineas
        //Traer primera coordenada de la linea
        //Revisar si existen las lineas con las coordenadas que corresponden a cerrar un cuadro, en el sentido del reloj y contra el sentido.
        //Si existen 4 lineas que correspondan con el set de coordenadas, marcar un cuadro.
        esMismoTurno=false;
        GameObject[] lineArray;
        lineArray = GameObject.FindGameObjectsWithTag("Line");
        LineasActuales=lineArray.Length;

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
                EvaluarCuadro(col,fil);
            }
            }
                if(!acabaDeHacerUnPunto){
                    if(JugadorActual==1){
                        FinBlindarJugador1();
                    } else{                    
                    FinBlindarJugador2();
                    }
                    CambioDeTurno(JugadorActual);
                }else{
                     //Cambiar turno si esta bloqueado
                     int siguienteJugador=JugadorActual;
                    if(bloqueadoJugador1&&JugadorActual==1){
                        siguienteJugador=2;
                        bloqueadoJugador1=false;
                        FinBlindarJugador1();
                        AclararJugador(BlackScreen_P2);
                        OscurecerJugador(BlackScreen_P1);
                    }
                    if(bloqueadoJugador2&&JugadorActual==2){
                        siguienteJugador=1;
                        bloqueadoJugador2=false;
                        FinBlindarJugador2();
                        AclararJugador(BlackScreen_P1);
                        OscurecerJugador(BlackScreen_P2);
                    }
                    if(siguienteJugador==JugadorActual){
                         esMismoTurno=true;
                    }
                    JugadorActual=siguienteJugador;
                   
                   
                }
            acabaDeHacerUnPunto=false;
            
               

       if(LineasActuales>=TotalMaximoLineas){
            //Todas las lineas posibles han sido llenadas determinar quien es el ganador/a
            if(puntosJugador1+puntosExtraJugador1>puntosJugador2+puntosExtraJugador2){
                TextoFinal="Gana el Jugador 1";
            }
            if(puntosJugador2+puntosExtraJugador2>puntosJugador1+puntosExtraJugador1){
                TextoFinal="Gana el Jugador 2";
            }
            txtMensajeFinal.SetActive(true);
            GameOver=true;            
        }

        TransformarCuadrosConsecutivos();
        ContarSetsConsecutivos();
    }

    public bool GetGameOver(){
        return GameOver;
    }

    void ContarSetsConsecutivos(){
        this.GetComponent<SimpleConsecutivoController>().ContarSetsConsecutivos();
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
                        
                        
                        Cuadro cuadro = new Cuadro();
                        var algo = primeraLinea1!=null ? cuadro.primeraLinea=primeraLinea1 : cuadro.primeraLinea=primeraLinea2;
                        var a2= segundaLinea1!=null ? cuadro.segundaLinea=segundaLinea1 : cuadro.segundaLinea=segundaLinea2;
                        var a3= terceraLinea1!=null ? cuadro.terceraLinea=terceraLinea1 : cuadro.terceraLinea=terceraLinea2;
                        var a4= cuartaLinea1!=null ? cuadro.cuartaLinea=cuartaLinea1 : cuadro.cuartaLinea=cuartaLinea2;

                        if(cuadro.primeraLinea.gameObject.tag=="Line"
                            &&cuadro.segundaLinea.gameObject.tag=="Line"
                            &&cuadro.terceraLinea.gameObject.tag=="Line"
                            &&cuadro.cuartaLinea.gameObject.tag=="Line"){
                                 hayUnCuadro=true;
                        }
                       
                        if(hayUnCuadro){
                            if(listaCuadros.Find(n=>   n.primeraLinea.name == cuadro.primeraLinea.name
                                                && n.segundaLinea.name==cuadro.segundaLinea.name
                                                && n.terceraLinea.name==cuadro.terceraLinea.name
                                                && n.cuartaLinea.name==cuadro.cuartaLinea.name)==null)
                            {
                                //Debug.Log("Hay un cuadro en "+col+","+fil);
                                listaCuadros.Add(cuadro);
                                //Llenar area del cuadro.
                                LlenarAreaDelCuadro(cuadro.primeraLinea,cuadro.terceraLinea);
                                acabaDeHacerUnPunto=true;
                                if(JugadorActual==1){
                                    puntosJugador1++;
                                }else{
                                    puntosJugador2++;
                                }
                            }else{
                                //Debug.Log("Cuadro ya existe es: "+cuadro.primeraLinea.name+ "/"+cuadro.segundaLinea.name+"/"+cuadro.terceraLinea.name+"/"+cuadro.cuartaLinea.name);
                                int contador=0;
                                foreach( Cuadro cuad in listaCuadros){
                                    //Debug.Log("Cuadro "+contador+" : "+cuad.primeraLinea.name+ "/"+cuad.segundaLinea.name+"/"+cuad.terceraLinea.name+"/"+cuad.cuartaLinea.name);
                                    contador++;
                            }
                            }
                        }
                    }
                }
            }

        }

        return hayUnCuadro;
    }

    void LlenarAreaDelCuadro(GameObject primeraLinea, GameObject segundaLinea){
        Vector3 posicionArea = Vector3.Lerp(primeraLinea.transform.position , segundaLinea.transform.position, 0.5f);
        GameObject areaObject = (GameObject)Resources.Load ("ConqueredArea");
        areaObject.name="Conquered_J"+JugadorActual+"$x="+posicionArea.x.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"#z="+posicionArea.z.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)+"&";
        areaObject.GetComponent<CuadCapturadoController>().SetNumeroJugador(JugadorActual);
        int numBlindaje=SetBlindaje();
        areaObject.GetComponent<CuadCapturadoController>().SetContadorBlindaje(numBlindaje);
        if(JugadorActual==1){
           areaObject.GetComponent<SpriteRenderer>().color=Color.blue;
        }else{
           areaObject.GetComponent<SpriteRenderer>().color=Color.red;
        }
        if(numBlindaje>0){
            if(JugadorActual==1){
                areaObject.GetComponent<SpriteRenderer>().color=new Color(21, 46, 94);
            }else{
                areaObject.GetComponent<SpriteRenderer>().color=new Color(94, 21, 21);
            }
        } 
        
        Instantiate(areaObject, posicionArea, Quaternion.Euler(-90,0,0));
        posicionArea.y=posicionArea.y+0.1f;

        string raza="";
        if(JugadorActual==1){
            raza=GetRazaJugador1();
        }
        else{
            raza=GetRazaJugador2();
        }
        if(raza=="Humano"){
            this.gameObject.GetComponent<PropsManager>().InstantiateHumans(posicionArea);
        }
        if(raza=="Mago"){
            this.gameObject.GetComponent<PropsManager>().InstantiateSages(posicionArea);
        }
        if(raza=="Piedra"){
            this.gameObject.GetComponent<PropsManager>().InstantiateDemons(posicionArea);
        }

        //this.gameObject.GetComponent<PropsManager>().InstantiateCapturedQuad(posicionArea);
        TransformarCuadrosConsecutivos();       
    }

    int SetBlindaje(){
        int numBlindaje=0;
        if(PuedeBlindar(JugadorActual)){
            numBlindaje=1;
        }
        return numBlindaje;
    }

    void TransformarCuadrosConsecutivos(){
        //this.gameObject.GetComponent<ConsecutivosController>().TransformarCuadrosConsecutivos(posicionArea);
        //this.gameObject.GetComponent<ConsecutivosController>().ContarContinuos(areaObject);
        this.gameObject.GetComponent<SimpleConsecutivoController>().ActualizarConsecutivos();
    }

    void CambioDeTurno(int _pJugadorActual){
        if(_pJugadorActual==1){
            JugadorActual=2;
            AclararJugador(BlackScreen_P2);
            OscurecerJugador(BlackScreen_P1);            
        }else{
            JugadorActual=1;
            AclararJugador(BlackScreen_P1);
            OscurecerJugador(BlackScreen_P2);
        }
    }

    void AclararJugador(GameObject BlackScreen_Jugador){
        Color tmp = BlackScreen_Jugador.GetComponent<Image>().color;
        tmp.a = 0f; //0 es transparente, 1 es opaco
        BlackScreen_Jugador.GetComponent<Image>().color = tmp;
    }

     void OscurecerJugador(GameObject BlackScreen_Jugador){
        Color tmp = BlackScreen_Jugador.GetComponent<Image>().color;
        tmp.a = 0.7f;
        BlackScreen_Jugador.GetComponent<Image>().color = tmp;
    }

}

