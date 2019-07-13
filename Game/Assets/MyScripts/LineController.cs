using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    void OnMouseDown(){
       ActivarLinea();
    }

    void ActivarLinea(){
        if(this.gameObject.tag!="Line"){
            this.gameObject.tag="Line";
            GameObject objGC = GameObject.Find("GameController");
            if(objGC!=null){
                if(GetJugador()==1){
                //raya azul
                    this.gameObject.GetComponent<SpriteRenderer>().color=Color.blue;
                }else{
                //raya roja
                    this.gameObject.GetComponent<SpriteRenderer>().color=Color.red;
                }
                objGC.GetComponent<GameController>().EvaluarTablero();
            }
        }
    }

    int GetJugador(){
        GameObject objGC = GameObject.Find("GameController");
        if(objGC!=null){
            return objGC.GetComponent<GameController>().GetJugadorActual();
        }
        return -1;
    }
}
