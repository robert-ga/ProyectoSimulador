using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class CamController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject centerCam;
    public Transform tar;
    public float mover = 2f;
    public Text distancia;

    void Start()
    {
        seguimientorostro();
        distancia.text = recuperar.distancia;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        seguimientorostro();
    }
    public void seguimientorostro()
    {
        distancia.text = recuperar.distancia;
        //print(distancia.text + "hollaaaaaa");
        string msg = File.ReadAllText(@"C:/Users/USUARIO/Desktop/Simulador De Tiro/DetectorDeRostro/DatosSalida.json");
        //print(msg);

        if (!File.Exists(msg))
        {
            string[] coordenadas = msg.Split(';');
            float x = float.Parse(coordenadas[0]) / 10;
            float y = float.Parse(coordenadas[1]) / 10;
            float z = float.Parse(coordenadas[2]) / 10;
            Vector3 f = centerCam.transform.position;
            //print(f);
            if (int.Parse(distancia.text)==50)
            {
                //f = new Vector3(x, y, -29f);
                f = new Vector3(x, y, -30f);
                //f = new Vector3(x, y, z);
                /*if (f.x < -11.4)
                {
                    f.x = -11.4f;
                }
                /*if (f.x > 10.9)
                {
                    f.x = 10.9f;
                }*/
                /*if (f.x > 12)
                {
                    f.x = 12f;
                }
                if (f.z < -11.3)
                {
                    f.z = -11.3f;
                }*/
                //print(f);
                tar.position = Vector3.Lerp(tar.position, f, mover * Time.deltaTime);
            }
            if (int.Parse(distancia.text) == 25)
            {
                f = new Vector3(x, y, -10f);
                tar.position = Vector3.Lerp(tar.position, f, mover * Time.deltaTime);
            }
            if (int.Parse(distancia.text) == 15)
            {
                f = new Vector3(x, y, 25f);
                tar.position = Vector3.Lerp(tar.position, f, mover * Time.deltaTime);
            }
            if (int.Parse(distancia.text) == 5)
            {
                f = new Vector3(x, y, 40f);
                tar.position = Vector3.Lerp(tar.position, f, mover * Time.deltaTime);
            }
        }
        else
        {
            //string msg = File.ReadAllText(@"C:/Users/User/Desktop/Simulador de Tiro/DetectorDeRostro/DatosSalida.json");
            File.Create(msg).Dispose();
        }
    }
}
