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
    public GameObject target;
    float verticalrotation;
   // public GameObject centerCam;

    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        //puntero();

    }
    void Update()
    {
        mouse2d();
        //puntero();

    }
    public void mouse()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mousesensi);
        verticalrotation -= Input.GetAxisRaw("Mouse Y") * mousesensi;
        verticalrotation = Mathf.Clamp(verticalrotation, -90f, 90f);
        camara.localEulerAngles = new Vector3(verticalrotation, 0, 0);
    }
    private void puntero()
    {
        string msg = File.ReadAllText(@"C:/Users/USUARIO/Desktop/Simulador De Tiro/DetectorDeArma/DatosPuntero.json");
        if (!File.Exists(msg))
        {
            string[] coordenadas = msg.Split(';');
            float x = float.Parse(coordenadas[0]) / 10;
            float y = float.Parse(coordenadas[1]) / 10;
            x += Input.GetAxis("Mouse X");
            y += Input.GetAxis("Mouse Y");
            a = centerCam.transform.localRotation;
            b = Quaternion.Euler(y, x, 0);
            tar.localRotation = Quaternion.Lerp(a, b, Time.deltaTime * mover);
        }
        else
        {
            File.Create(msg).Dispose();
        }
        
    }
    public void mouse2d()
    {
        float x, y;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        x = Input.mousePosition.x;
        y = Input.mousePosition.y;

        target.transform.position = new Vector3(x, y, 0f);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform != null)
            {
                ptrin(hit.transform.gameObject);
            }
        }
    }
    public void ptrin(GameObject go)
    {
        print(go.name);
    }
}
