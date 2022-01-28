using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class llenardatosusuarios : MonoBehaviour
{
    public GameObject id;
    public GameObject nombres;
    public GameObject contrase;
    public Text idte;
    private void Start()
    {
        idte.enabled = false;
    }
    public void setdatos(int id, string nombres, string contrase)
    {
        this.id.GetComponent<Text>().text = id.ToString();
        this.nombres.GetComponent<Text>().text = nombres;
        this.contrase.GetComponent<Text>().text = contrase;
    }
}
