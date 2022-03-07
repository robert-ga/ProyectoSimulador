using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using TMPro;
using System.IO;
using System;

public class menuestadisitcajugadores : MonoBehaviour
{
    IDbConnection dbcon;
    public GameObject cell;
    public Transform ce;
    public InputField nombre;
    bool lleno;
    public Button reu;
    public Button ret;
    public Button rep;
    // Start is called before the first frame update
    void Start()
    {
        //llenardatos();
        //lleno=false;
        // lleno = false;

        reu.gameObject.SetActive(false);
        ret.gameObject.SetActive(false);
        rep.gameObject.SetActive(false);

    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void conexiondb()
    {
        string conn = "URI=file:" + Application.dataPath + "/BD/" + "simulador.db"; //Path to database.
        dbcon = new SqliteConnection(conn);
        dbcon.Open();
    }
    public void desconexiondb()
    {
        dbcon.Close();
    }
    public class Datospuntuacion
    {
        public string name;
        public int kills;
        public int municion;
        public string precision;
        public string tiempo;
        public string demora;
        public string fecha;
        public string hora;
        public string distancia;
        public Datospuntuacion(string name, int kills, int municion, string precision, string tiempo, string demora, string fecha, string hora, string distancia)
        {

            this.name = name;
            this.kills = int.Parse(kills.ToString());
            this.municion = int.Parse(municion.ToString());
            this.precision = precision;
            this.tiempo = tiempo;
            this.demora = demora;
            this.fecha = fecha;
            this.hora = hora;
            this.distancia = distancia;
        }
    }
    public List<Datospuntuacion> getLeaderBoard()
    {
        conexiondb();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM examen ORDER BY id_usuario,id_examen DESC";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<Datospuntuacion> datospuntuacion = new List<Datospuntuacion>();
        while (reader.Read())
        {
            datospuntuacion.Add(new Datospuntuacion(reader[1].ToString(), int.Parse(reader[2].ToString()), int.Parse(reader[3].ToString()), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString()));
        }

        return datospuntuacion;
    }
    public void llenardatos()
    {
        //limpiardatos();
        
        List<Datospuntuacion> datospuntuacion = getLeaderBoard();
        //print(datospuntuacion);
        for (int i = 0; i < datospuntuacion.Count; i++)
        {
            
            GameObject da = Instantiate(cell);
            
            Datospuntuacion datosexamens = datospuntuacion[i];
            
            da.GetComponent<llenarpuntuacionusuarios>().setdatospuntuacion(datosexamens.name, datosexamens.kills, datosexamens.municion, datosexamens.precision, datosexamens.tiempo, datosexamens.demora, datosexamens.fecha, datosexamens.hora, datosexamens.distancia);
            da.transform.SetParent(ce);
            da.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //print(da);

        }
    }
    
    public List<Datospuntuacion> buscar(string Search_by_name)
    {
        
        conexiondb();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM examen where usuario LIKE\'" + Search_by_name + "\' ORDER BY id_examen DESC";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<Datospuntuacion> datospuntuacion = new List<Datospuntuacion>();
        while (reader.Read())
        {
            

            datospuntuacion.Add(new Datospuntuacion(reader[1].ToString(), int.Parse(reader[2].ToString()), int.Parse(reader[3].ToString()), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString()));
            //print(reader[1].ToString());
        }
       // print("soly fiunal" + pp + " de "+ c);

        return datospuntuacion;
    }
    public void llenardatosbuscados()
    {
        //limpiardatos();
        List<Datospuntuacion> datospuntuacion = buscar(nombre.text);
        for (int i = 0; i < datospuntuacion.Count; i++)
        {
            GameObject da = Instantiate(cell);
            
            Datospuntuacion datosexamens = datospuntuacion[i];
            da.GetComponent<llenarpuntuacionusuarios>().setdatospuntuacion(datosexamens.name, datosexamens.kills, datosexamens.municion, datosexamens.precision, datosexamens.tiempo, datosexamens.demora, datosexamens.fecha, datosexamens.hora, datosexamens.distancia);
            da.transform.SetParent(ce);
            da.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        }
    }


    public List<Datospuntuacion> getLeaderBoardpro()
    {
        int pp = 0, c = 0;
        conexiondb();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT id_examen,usuario,kills,municiones,round(avg(precision),2),tiempo,demora,fecha,hora,distancia,id_usuario FROM examen GROUP BY id_usuario ORDER BY avg(precision) DESC";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<Datospuntuacion> datospuntuacion = new List<Datospuntuacion>();
        while (reader.Read())
        {
            pp = int.Parse(reader[10].ToString());
            
            datospuntuacion.Add(new Datospuntuacion(reader[1].ToString(), int.Parse(reader[2].ToString()), int.Parse(reader[3].ToString()), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString()));
        }
        return datospuntuacion;
    }
    public void llenardatospro()
    {
        //limpiardatos();

        List<Datospuntuacion> datospuntuacion = getLeaderBoardpro();
        //print(datospuntuacion);
        for (int i = 0; i < datospuntuacion.Count; i++)
        {

            GameObject da = Instantiate(cell);

            Datospuntuacion datosexamens = datospuntuacion[i];

            da.GetComponent<llenarpuntuacionusuarios>().setdatospuntuacion(datosexamens.name, datosexamens.kills, datosexamens.municion, datosexamens.precision, datosexamens.tiempo, datosexamens.demora, datosexamens.fecha, datosexamens.hora, datosexamens.distancia);
            da.transform.SetParent(ce);
            da.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //print(da);

        }
    }


    public void limpiardatos()
    {
        /*List<Datospuntuacion> datospuntuacion = getLeaderBoard();
        GameObject da = Instantiate(cell);
        da.transform.SetParent(ce);
        Destroy(da);*/
        /*if (lleno == true)
        {
            lleno = false;
        }
        else
        {
            lleno= true;
        }
        
        print("1"+lleno);*/
        //scene escena = "EstadisticasUsuarios";
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
        //Debug.Log("La escena activa es '" + scene.name + "'.");
        SceneManager.LoadScene("EstadisticasUsuarios");


    }
    public void mostrartodo()
    {
        //print(limpiardatos());


        /*if(lleno == true)
        {
            print("entro");
            
            lleno = false;
            SceneManager.LoadScene("EstadisticasUsuarios");
            print("se acrtualizo");
            
            if(lleno == false)
            {
                print("ingreso");
                llenardatos();
                
                print("se lleno");
                lleno = false;
            }
            
        }
        
        else if(lleno==false)
        {
            llenardatos();
            lleno=(true);
            print("es true");
        }*/
        ret.gameObject.SetActive(true);
        reu.gameObject.SetActive(false);
        rep.gameObject.SetActive(false);
        llenardatos();
        

    }
    public void mostrartodopro()
    {
        ret.gameObject.SetActive(false);
        reu.gameObject.SetActive(false);
        rep.gameObject.SetActive(true);
        llenardatospro();


    }
    public void usuariopuntuacion()
    {
        //List<Datospuntuacion> datospuntuacion = buscar(nombre.text);
        //llenardatos();
        //limpiardatos();
        //limpiardatos();
        reu.gameObject.SetActive(true);
        ret.gameObject.SetActive(false);
        rep.gameObject.SetActive(false);
        llenardatosbuscados();
    }
    public void CrearArchivoCSVusu(string Search_by_name)
    {
        string ruta = @"C:/Users/USUARIO/Desktop/Simulador De Tiro/Reportes/" + "reporte de " + Search_by_name + ".csv";
        //El archivo existe? lo BORRAMOS
        if (File.Exists(ruta))
        {
            File.Delete(ruta);
        }
        TextWriter tw = new StreamWriter(ruta, false);
        tw.WriteLine("Escuela Militar De Ingenieria U.A.Cochabamba");
        tw.WriteLine("Departamento de Operaciones");
        tw.WriteLine("Reporte de usuario " + Search_by_name+ " de examenes");
        tw.WriteLine("\n" + "\n");
        tw.WriteLine("Usuario;Aciertos;Municion;Puntuacion;Tiempo;Demora;Fecha;Hora;Distancia");
        tw.Close();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM examen where usuario LIKE\'" + Search_by_name + "\' ORDER BY id_examen DESC";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<Datospuntuacion> datospuntuacion = new List<Datospuntuacion>();
        while (reader.Read())
        {
            datospuntuacion.Add(new Datospuntuacion(reader[1].ToString(), int.Parse(reader[2].ToString()), int.Parse(reader[3].ToString()), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString()));
        }
        tw = new StreamWriter(ruta, true);
        foreach (var i in datospuntuacion)
        {
            tw.WriteLine(i.name.ToString() + ";" + int.Parse(i.kills.ToString()) + ";" + int.Parse(i.municion.ToString()) + ";" + i.precision.ToString() + ";" + i.tiempo.ToString() + ";" + i.demora.ToString() + ";" + i.fecha.ToString() + ";" + i.hora.ToString() + ";" + i.distancia.ToString());


        }
        tw.Close();
        Application.OpenURL(ruta);
    }
    public void CrearArchivoCSVtodo()
    {
        string ruta = @"C:/Users/USUARIO/Desktop/Simulador De Tiro/Reportes/" + "reporte de todas las puntuaciones.csv";
        //El archivo existe? lo BORRAMOS
        if (File.Exists(ruta))
        {
            File.Delete(ruta);
        }
        TextWriter tw = new StreamWriter(ruta, false);
        tw.WriteLine("Escuela Militar De Ingenieria U.A.Cochabamba");
        tw.WriteLine("Departamento de Operaciones");
        tw.WriteLine("Reporte de Todos los Examenes");
        tw.WriteLine("\n" + "\n");
        tw.WriteLine("Usuario;Aciertos;Municion;Puntuacion;Tiempo;Demora;Fecha;Hora;Distancia");
        tw.Close();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM examen ORDER BY id_usuario,id_examen DESC";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<Datospuntuacion> datospuntuacion = new List<Datospuntuacion>();
        while (reader.Read())
        {
            datospuntuacion.Add(new Datospuntuacion(reader[1].ToString(), int.Parse(reader[2].ToString()), int.Parse(reader[3].ToString()), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString()));
        }
        tw = new StreamWriter(ruta, true);
        foreach (var i in datospuntuacion)
        {
            tw.WriteLine(i.name.ToString() + ";" + int.Parse(i.kills.ToString()) + ";" + int.Parse(i.municion.ToString()) + ";" + i.precision.ToString() + ";" + i.tiempo.ToString() + ";" + i.demora.ToString() + ";" + i.fecha.ToString() + ";" + i.hora.ToString() + ";" + i.distancia.ToString());
        }
        tw.Close();
        Application.OpenURL(ruta);
    }
    public void CrearArchivoCSVpromedio()
    {
        string ruta = @"C:/Users/USUARIO/Desktop/Simulador De Tiro/Reportes/" + "reporte de ranking de examenes.csv";
        //El archivo existe? lo BORRAMOS
        if (File.Exists(ruta))
        {
            File.Delete(ruta);
        }
        TextWriter tw = new StreamWriter(ruta, false);
        tw.WriteLine("Escuela Militar De Ingenieria U.A.Cochabamba");
        tw.WriteLine("Departamento de Operaciones");
        tw.WriteLine("Reporte de Ranking");
        tw.WriteLine("\n" + "\n");
        tw.WriteLine("Usuario;Aciertos;Municion;Puntuacion;Tiempo;Demora;Fecha;Hora;Distancia");
        tw.Close();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT id_examen,usuario,kills,municiones,round(avg(precision),2),tiempo,demora,fecha,hora,distancia,id_usuario FROM examen GROUP BY id_usuario ORDER BY avg(precision) DESC";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<Datospuntuacion> datospuntuacion = new List<Datospuntuacion>();
        while (reader.Read())
        {
            datospuntuacion.Add(new Datospuntuacion(reader[1].ToString(), int.Parse(reader[2].ToString()), int.Parse(reader[3].ToString()), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString()));
        }
        tw = new StreamWriter(ruta, true);
        foreach (var i in datospuntuacion)
        {
            tw.WriteLine(i.name.ToString() + ";" + int.Parse(i.kills.ToString()) + ";" + int.Parse(i.municion.ToString()) + ";" + i.precision.ToString() + ";" + i.tiempo.ToString() + ";" + i.demora.ToString() + ";" + i.fecha.ToString() + ";" + i.hora.ToString() + ";" + i.distancia.ToString());
        }
        tw.Close();
        Application.OpenURL(ruta);
    }
    public void reporteusu()
    {
        CrearArchivoCSVusu(nombre.text);
    }
    public void reportetodo()
    {
        CrearArchivoCSVtodo();
    }
    public void reportepromedio()
    {
        CrearArchivoCSVpromedio();
    }
    public void atras()
    {
        
        SceneManager.LoadScene("MenuAdministrador");
    }
}
