using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ControlesJugador : MonoBehaviour
{
    [SerializeField] Transform camara;
    [SerializeField] float mousesensi = 0.5f;
    
    public float mover = 2f;
    private Quaternion a, b;
    public GameObject centerCam;
    public Transform tar;
    float verticalrotation;
   // public GameObject centerCam;


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
      // puntero();

    }
    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mousesensi);
        verticalrotation -= Input.GetAxisRaw("Mouse Y") * mousesensi;
        verticalrotation = Mathf.Clamp(verticalrotation, -90f, 90f);
        camara.localEulerAngles = new Vector3(verticalrotation, 0, 0);
        //puntero();

    }
    private void puntero()
    {
        /*string msg = File.ReadAllText(@"C:/Users/USUARIO/Desktop/Simulador De Tiro/DetectorDeArma/DatosPuntero.json");
        print(msg);


        if (!File.Exists(msg))
        {

            string[] coordenadas = msg.Split(';');
            float x = float.Parse(coordenadas[0]) / 10;
            float y = float.Parse(coordenadas[1]) / 10;

            x += Input.GetAxis("Mouse X");
            y += Input.GetAxis("Mouse Y");
            //transform.localRotation = Quaternion.Euler(y, x, 0);
            a = centerCam.transform.localRotation;
            b = Quaternion.Euler(y, x, 0);
            
            tar.localRotation = Quaternion.Lerp(a, b, Time.deltaTime * mover);
            

        }
        else
        {
            //string msg = File.ReadAllText(@"C:/Users/User/Desktop/Simulador de Tiro/DetectorDeRostro/DatosSalida.json");
            File.Create(msg).Dispose();
        }*/
        
    }
}
