using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class menuopciones : MonoBehaviour
{
    // Start is called before the first frame update
    /*public InputField municiones;
    public InputField tiempo;
    public InputField distancia;*/
    public GameObject panelre;
    int dis, mun, tiem;
    void Start()
    {
        //panelre.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ingre()
    {
        /*if (municiones.text=="" || tiempo.text=="" || distancia.text =="")
        {
            panelre.SetActive(true);
        }
        else
        {
            ingresardatos();
        }*/
        if (dis==0 || mun==0 || tiem == 0)
        {
            panelre.SetActive(true);
        }
        else
        {
            ingresardatos();
        }

    }
    public void ingresardatos()
    {
        /*if (int.Parse(distancia.text) == 50 || int.Parse(distancia.text) == 30 || int.Parse(distancia.text) == 10)
        {
            print(int.Parse(distancia.text) + "");
            distancia.text = distancia.text;
            municiones.text = municiones.text;
            tiempo.text = tiempo.text;
            recuperar.distancia = distancia.text;
            recuperar.tiempo = tiempo.text;
            recuperar.municion = municiones.text;
        }
        else
        {
            panelre.SetActive(true);
            distancia.text = "";
            municiones.text = "";
            tiempo.text = "";
        }*/
        recuperar.distancia = dis.ToString();
        recuperar.tiempo = tiem.ToString();
        recuperar.municion = mun;
        print(recuperar.distancia);

    }
    public void opciondistacion(int val)
    {
        if (val == 0)
        {
            dis = 0;
        }
        if (val == 1)
        {
            dis = 10;
        }
        if (val == 2)
        {
            dis = 30;
        }
        if (val == 3)
        {
            dis = 50;
        }
 
    }
    public void opcionmuni(int val)
    {
        if (val == 0)
        {
            mun = 0;
        }
        if (val == 1)
        {
            mun = 15;
        }
        if (val == 2)
        {
            mun = 16;
        }
        if (val == 3)
        {
            mun = 17;
        }
        if (val == 4)
        {
            mun = 18;
        }
        if (val == 5)
        {
            mun = 19;
        }
        if (val == 6)
        {
            mun = 20;
        }
        if (val == 7)
        {
            mun = 21;
        }
        if (val == 8)
        {
            mun = 22;
        }
        if (val == 9)
        {
            mun = 23;
        }
        if (val == 10)
        {
            mun = 24;
        }
        if (val == 11)
        {
            mun = 25;
        }

    }
    public void opciontiempo(int val)
    {

        if (val == 0)
        {
            tiem = 0;
        }
        if (val == 1)
        {
            tiem = 1;
        }
        if (val == 2)
        {
            tiem = 2;
        }
        if (val == 3)
        {
            tiem = 3;
        }
        if (val == 4)
        {
            tiem = 4;
        }
        if (val == 5)
        {
            tiem = 5;
        }
        if (val == 6)
        {
            tiem = 6;
        }
        if (val == 7)
        {
            tiem = 7;
        }
        if (val == 8)
        {
            tiem = 8;
        }
        if (val == 9)
        {
            tiem = 9;
        }
        if (val == 10)
        {
            tiem = 10;
        }
        if (val == 11)
        {
            tiem = 11;
        }
 
    }
    public void aceptar()
    {
        panelre.SetActive(false);
    }
    public void atras()
    {
        SceneManager.LoadScene("MenuEntrenador");
    }
}
