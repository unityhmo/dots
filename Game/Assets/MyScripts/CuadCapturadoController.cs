using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuadCapturadoController : MonoBehaviour
{   
    public bool esContinuo;

    void CambiarColorContinuo(){
        GameObject objGC = GameObject.Find("GameController");
        if(objGC!=null){
            int jugador= objGC.GetComponent<GameController>().GetJugadorActual();
            if(jugador==1){
                this.GetComponent<SpriteRenderer>().color=Color.yellow;
            }else{
                this.GetComponent<SpriteRenderer>().color=Color.green;
            }
        }
    }

    public void SetEsContinuo(bool continuo){
        esContinuo=continuo;
        if(esContinuo){
            CambiarColorContinuo();
        }
    }

    public bool GetEsContinuo(){
        return esContinuo;
    }


}
