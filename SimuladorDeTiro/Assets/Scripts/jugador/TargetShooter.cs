using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class TargetShooter : MonoBehaviour
{
    [SerializeField] Camera cam;
    int canti = 0;
    int dis = 0;
    private float timeRemaining;
    private float timeRemainingcon;
    public Text timeText;
    public Text timeTextcon;
    public Text aciertos;
    public Text preci;
    public Text distancia;
    public Text municiones;
    int muni;
    int pre = 0;
    int p;
    int t;
    DateTime fecha = DateTime.Today;
    string hora = DateTime.Now.ToShortTimeString();
    IDbConnection dbcon;
    private GameObject cab;
    //private Collider collider;
    int m;
    public AudioSource audiodisparo;
    public GameObject target;

    void Start()
    {
        timeTextcon.enabled = false;
        t = recuperar.tipo;
        entrenaexame();
        /*distancia.text = recuperar.distancia;
        timeText.text = recuperar.tiempo;
        municiones.text = recuperar.municion.ToString();
        timeRemaining = int.Parse(recuperar.tiempo) * 60;*/
    }
    public void entrenaexame()
    {
        if (t == 1)
        {
            distancia.text = recuperar.distancia;
            timeText.text = recuperar.tiempo;
            municiones.text = recuperar.municion.ToString();
            timeRemaining = int.Parse(recuperar.tiempo) * 60;
        }
        if (t == 0)
        {
            recuperar.distancia = 50.ToString();
            recuperar.municion=15;
            distancia.text = recuperar.distancia;
            timeText.text = 2.ToString();
            municiones.text = recuperar.municion.ToString();
            timeRemaining = 2 * 60;
        }
    }
    void Update()
    {
        //kills();
        timer();
        //persona();
        //precision();
        timerconta();
        //compara();
        puntuacion();
        //regresar();
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    void DisplayTimecon(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeTextcon.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public float timer()
    {
        string xx;
        xx = municio(dis);
        if (timeRemaining > 0 )
        {
            timeRemaining -= Time.deltaTime;

        }
        else
        {
            timeRemaining = 0;
            
            if (timeRemaining == 0)
            {
                timeRemaining = 1;
                tipo();
                //print( timeRemaining+ " " + canti + " " + precision());
            }
            //print("1:30    " + kills() +"   "+ p+"%");
            //SceneManager.LoadScene("MenuEntrenador");
        }
        //print("holaaaa" + xx);
        if (xx == "0" || timeTextcon.text == recuperar.tiempo)
        {
            tipo();
        }
        DisplayTime(timeRemaining);
        return timeRemaining;
        //return p.ToString();
    }
    public float timerconta()
    {
        
        if (timeRemainingcon > 0)
        {
            
            timeRemainingcon += Time.deltaTime;

        }
        else
        {
            timeRemainingcon = 0;

            if (timeRemainingcon == 0)
            {
                timeRemainingcon = 1;
                
            }
            
        }
        DisplayTimecon(timeRemainingcon);
        return timeRemainingcon;
        //return p.ToString();
    }
    public void tipo()
    { 
        if (t == 1)
        {
            //entrenamiento
            //timerconta();
            //timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            conexiondb();
            IDbCommand cmnd = dbcon.CreateCommand();
            cmnd.CommandText = "INSERT INTO puntuacion (usuario,kills,municiones,precision,tiempo,demora,fecha,hora,distancia,id_usuario) VALUES(\'" + recuperar.name + "\',\'" + canti + "\',\'" + recuperar.municion + "\',\'" + pre + "\',\'" + recuperar.tiempo+ "\',\'" + timeTextcon.text + "\',\'" + fechaa() + "\',\'" + hora + "\',\'" + distancia.text + "\',\'" + recuperar.idilogin + "\')";
            cmnd.ExecuteNonQuery();
            desconexiondb();
            SceneManager.LoadScene("Estadisticas");
        }
        if (t == 0)
        {
            //examen
            recuperar.tiempo = 2.ToString();
            conexiondb();
            IDbCommand cmnd = dbcon.CreateCommand();
            cmnd.CommandText = "INSERT INTO examen (usuario,kills,municiones,precision,tiempo,demora,fecha,hora,distancia,id_usuario) VALUES(\'" + recuperar.name + "\',\'" + canti + "\',\'" + recuperar.municion + "\',\'" + pre + "\',\'" + recuperar.tiempo + "\',\'" + timeTextcon.text + "\',\'" + fechaa() + "\',\'" + hora + "\',\'" + distancia.text + "\',\'" + recuperar.idilogin + "\')";
            cmnd.ExecuteNonQuery();
            desconexiondb();
            SceneManager.LoadScene("Estadisticas");
        }
    }
    public string kills()
    {
        //m = int.Parse(municiones.text);
        // print(m);
        if (Input.GetMouseButtonDown(0))
        {
            dis++;
            municio(dis);
            //m = muni - dis;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Target target = hit.collider.gameObject.GetComponent<Target>();
                if (target != null)
                {
                    target.Hit();
                    canti++;
                    aciertos.text =canti.ToString();
                   
                    //regresar();
                    //print(canti);
                }
            }
            //municiones.text = m.ToString();

        }
        //print("dis" + m);
        return canti.ToString();
        // 
    }
    public string persona()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dis++;
            municio(dis);
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Target target = GameObject.FindGameObjectWithTag("completo").GetComponent<Target>();
                switch (hit.collider.gameObject.tag)
                {

                    case "cabeza":
                        target.Hit();
                        canti++;
                        pre = pre + 10;
                        break;
                    case "piernas":
                        target.Hit();
                        canti++;
                        pre = pre + 9;
                        break;
                    case "cuerpo":
                        target.Hit();
                        canti++;
                        pre = pre + 8;
                        break;
                    
                    default:
                        print("error");
                        break;
                }
                aciertos.text = canti.ToString();
                preci.text = pre.ToString();
            }
        }
        return canti.ToString();
    }
    
    public string compara()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audiodisparo.Play();
            dis++;
            municio(dis);
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                switch (hit.collider.gameObject.tag)
                {
                    case "10":
                        canti++; 
                        pre = pre + 10;
                        break;
                    case "9":
                        canti++;
                        pre = pre + 9;
                        break;
                    case "8":
                        canti++;
                        pre = pre + 8;
                        break;
                    case "7":
                        canti++;
                        pre = pre + 7;
                        break;
                    case "6":
                        canti++;
                        pre = pre + 6;
                        break;
                    case "5":
                        canti++;
                        pre = pre + 5;
                        break;
                    default:
                        print("error");
                        break;
                }
                aciertos.text = canti.ToString();
                preci.text = pre.ToString();
            }
        }
        return canti.ToString();
    }
    public string puntuacion()
    {
        float x, y;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        x = Input.mousePosition.x ;
        y = Input.mousePosition.y ;

        target.transform.position = new Vector3(x, y, 0f);
        if (Input.GetMouseButtonDown(0))
        {
            audiodisparo.Play();
            dis++;
            municio(dis);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //print("entri");
                if (hit.transform != null)
                {
                    // ptrin(hit.transform.gameObject);
                    switch (hit.collider.gameObject.tag)
                    {

                        case "10":
                            //target.Hit();
                            canti++;
                            pre = pre + 10;
                            break;
                        case "9":
                            //target.Hit();
                            canti++;
                            pre = pre + 9;
                            break;
                        case "8":
                            //target.Hit();
                            canti++;
                            pre = pre + 8;
                            break;
                        case "7":
                            //target.Hit();
                            canti++;
                            pre = pre + 7;
                            break;
                        case "6":
                            //target.Hit();
                            canti++;
                            pre = pre + 6;
                            break;
                        case "5":
                            //target.Hit();
                            canti++;
                            pre = pre + 5;
                            break;
                        default:
                            print("error");
                            break;
                    }

                    aciertos.text = canti.ToString();
                    preci.text = pre.ToString();
                }
            }
        }
        return canti.ToString();
    }

    public string municio(int d)
    {
        muni = recuperar.municion;
        m = muni - d;
        municiones.text = m.ToString();
        return m.ToString();
    }
    /*private void OnTriggerEnter(Collider other)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (other.gameObject.tag == "cabeza")
        {
            Debug.Log(other.gameObject.tag);
        }
    }*/

    public string precision()
    {
        pre = canti * 100;
        p = pre / 30;
        preci.text = p.ToString()+"%";
        return p.ToString();
    }
    public void regresar()
    {

        /*if (canti == 30)
        {
            float t = (60 - (int)Math.Round(timeRemaining));
            conexiondb();
            IDbCommand cmnd = dbcon.CreateCommand();
           // cmnd.CommandText = "INSERT INTO puntuacion (usuario,kills,precision,tiempo,fecha,hora,distancia,id_usuario) VALUES(\'" + recuperar.name + "\',\'" + canti + "\',\'" + precision()+"%" + "\',\'" + t+"seg" + "\',\'" + fechaa() + "\',\'" + horaa() + "\',\'" + distancia.text + "\',\'" + recuperar.idilogin + "\')";

            cmnd.ExecuteNonQuery();
            desconexiondb();
            Cursor.visible = true;
            SceneManager.LoadScene("Estadisticas");

            
            //print(recuperar.name+" "+t + " " + canti + " " + precision()+" "+ fechaa()+" "+recuperar.idilogin);
        }*/
        /*else if (timer() == 0)
        {
            
            //print(recuperar.name + " " + timeRemaining + " " + canti + " " + precision()+" " + fechaa() + " " + recuperar.idilogin);
        }*/
        Cursor.visible = true;
        SceneManager.LoadScene("Estadisticas");
    }
    public string fechaa()
    {
        return fecha.ToShortDateString();
    }
    
    public void conexiondb()
    {
        string conn = "URI=file:" + Application.dataPath + "/BD/" + "simulador.db"; //Path to database.
        dbcon = new SqliteConnection(conn);
        dbcon.Open();
        print("se conecto");
    }
    public void desconexiondb()
    {
        dbcon.Close();
    }
    /*private void OnCollisionEnter(Collision other)

    {

        //Debug.Log("Me choco");

        //cambindo de color

        if ("Player" == other.gameObject.tag)

        {
            Destroy(other.gameObject);

        }

    }*/
}
