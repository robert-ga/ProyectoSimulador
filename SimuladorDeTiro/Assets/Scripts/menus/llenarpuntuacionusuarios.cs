using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class llenarpuntuacionusuarios : MonoBehaviour
{
    public GameObject nombres;
    public GameObject kills;
    public GameObject municion;
    public GameObject precision;
    public GameObject tiempo;
    public GameObject demora;
    public GameObject fecha;
    public GameObject hora;
    public GameObject distancia;

    public void setdatospuntuacion(string nombres, int kills, int municion, string precision, string tiempo, string demora, string fecha, string hora, string distancia)
    {
        this.nombres.GetComponent<Text>().text = nombres;
        this.kills.GetComponent<Text>().text = kills.ToString();
        this.municion.GetComponent<Text>().text = municion.ToString();
        this.precision.GetComponent<Text>().text = precision;
        this.tiempo.GetComponent<Text>().text = tiempo;
        this.demora.GetComponent<Text>().text = demora;
        this.fecha.GetComponent<Text>().text = fecha;
        this.hora.GetComponent<Text>().text = hora;
        this.distancia.GetComponent<Text>().text = distancia;
    }
}
