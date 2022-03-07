using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class calibrarcamcontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject centerCam;
    public Transform tar;
    public float mover = 2f;

    void Start()
    {
        seguimientorostro();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        seguimientorostro();
    }
    public void seguimientorostro()
    {
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
            f = new Vector3(x, y, -30f);
            tar.position = Vector3.Lerp(tar.position, f, mover * Time.deltaTime);
        }
        else
        {
            
            File.Create(msg).Dispose();
        }
    }
}
