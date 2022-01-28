using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;

public class manuestadisticajugador : MonoBehaviour
{
    public Text nombrere;
    public GameObject cell;
    public Transform ce;
    IDbConnection dbcon;
    DateTime fecha = DateTime.Today;

    public Button entre;
    public Button exa;
    // Start is called before the first frame update
    void Start()
    {
        nombrere.text = recuperar.name;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //llenaresadisticarusuariosexamen();
        entre.gameObject.SetActive(false);
        exa.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void generarreporteusuario()
    {
        CrearArchivoCSV(nombrere.text);
    }
    public void generarreporteusuarioexamen()
    {
        CrearArchivoCSVExamen(nombrere.text);
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
        public Datospuntuacion(string name, int kills,int municion, string precision, string tiempo,string demora, string fecha, string hora, string distancia)
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
    public List<Datospuntuacion> buscar(string Search_by_name)
    {
        conexiondb();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM puntuacion where usuario LIKE\'" + Search_by_name + "\' ORDER BY id_puntuacion DESC";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<Datospuntuacion> datospuntuacion = new List<Datospuntuacion>();
        while (reader.Read())
        {
            datospuntuacion.Add(new Datospuntuacion(reader[1].ToString(), int.Parse(reader[2].ToString()), int.Parse(reader[3].ToString()), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString()));
            //print(reader[1].ToString());
        }

        return datospuntuacion;
    }
    public void llenaresadisticarusuarios()
    {
        List<Datospuntuacion> datospuntuacion = buscar(nombrere.text);
        for (int i = 0; i < datospuntuacion.Count; i++)
        {
            GameObject da = Instantiate(cell);
            Datospuntuacion datosentrenmiento = datospuntuacion[i];
            da.GetComponent<llenarusuariopuntuacion>().setdatospuntuacion(datosentrenmiento.name, datosentrenmiento.kills, datosentrenmiento.municion, datosentrenmiento.precision, datosentrenmiento.tiempo, datosentrenmiento.demora, datosentrenmiento.fecha, datosentrenmiento.hora, datosentrenmiento.distancia);
            da.transform.SetParent(ce);
            da.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //print(da);

        }
    }



    public List<Datospuntuacion> buscarexamen(string Search_by_name)
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

        return datospuntuacion;
    }
    public void llenaresadisticarusuariosexamen()
    {
        List<Datospuntuacion> datospuntuacion = buscarexamen(nombrere.text);
        for (int i = 0; i < datospuntuacion.Count; i++)
        {
            GameObject da = Instantiate(cell);
            Datospuntuacion datosentrenmiento = datospuntuacion[i];
            da.GetComponent<llenarusuariopuntuacion>().setdatospuntuacion(datosentrenmiento.name, datosentrenmiento.kills, datosentrenmiento.municion, datosentrenmiento.precision, datosentrenmiento.tiempo, datosentrenmiento.demora, datosentrenmiento.fecha, datosentrenmiento.hora, datosentrenmiento.distancia);
            da.transform.SetParent(ce);
            da.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //print(da);

        }
    }
    public void mostrarentrenamiento()
    {
        
        entre.gameObject.SetActive(true);
        exa.gameObject.SetActive(false);
        llenaresadisticarusuarios();


    }
    public void mostrarexamen()
    {
        exa.gameObject.SetActive(true);
        entre.gameObject.SetActive(false);
        llenaresadisticarusuariosexamen();
    }
    public void limpiardatos()
    {
        SceneManager.LoadScene("Estadisticas");

    }



    public void CrearArchivoCSV(string Search_by_name)
    {
        string ruta = @"C:/Users/USUARIO/Desktop/Simulador De Tiro/Reportes/" + "reporte de "+Search_by_name+".csv";
        //El archivo existe? lo BORRAMOS
        if (File.Exists(ruta))
        {
             File.Delete(ruta);
        }
        TextWriter tw = new StreamWriter(ruta, false);
        tw.WriteLine("Usuario;Aciertos;Municion;Puntuacion;Tiempo;Demora;Fecha;Hora;Distancia");
        tw.Close();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM puntuacion where usuario LIKE\'" + Search_by_name + "\' ORDER BY id_puntuacion DESC";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<Datospuntuacion> datospuntuacion = new List<Datospuntuacion>();
        while (reader.Read())
        {


            datospuntuacion.Add(new Datospuntuacion(reader[1].ToString(), int.Parse(reader[2].ToString()), int.Parse(reader[3].ToString()), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString()));

        }
        /*var sr = File.CreateText(ruta);
        string datosCSV = "";
        //datosCSV += puntuaciones[0].userId.ToString() + ",\n";
        //datosCSV += "\n," + "\n," + "\n" + "\n," + "\n";*/

        tw = new StreamWriter(ruta,true);
        //datosCSV += "usuario," + "kills," + "precision," + "tiempo," + "fecha," + "\n";
        foreach (var i in datospuntuacion)
        {
            //datosCSV += i.name.ToString() + "," +int.Parse(i.kills.ToString()) + "," + i.precision.ToString() + "," + i.tiempo.ToString() + "," +i.fecha.ToString() + ",\n";
            tw.WriteLine(i.name.ToString() + ";" + int.Parse(i.kills.ToString()) + ";"+ int.Parse(i.municion.ToString()) + ";" + i.precision.ToString() + ";" + i.tiempo.ToString() + ";"+ i.demora.ToString() + ";" + i.fecha.ToString() + ";" + i.hora.ToString() + ";" + i.distancia.ToString());

        }
        //Crear el archivo
        /*sr.WriteLine(datosCSV);
        //Dejar como sólo de lectura
        FileInfo fInfo = new FileInfo(ruta);
        fInfo.IsReadOnly = true;*/
        //Cerrar
        tw.Close();
        //Abrimos archivo recién creado
        Application.OpenURL(ruta);
    }
    public void CrearArchivoCSVExamen(string Search_by_name)
    {
        string ruta = @"C:/Users/USUARIO/Desktop/Simulador De Tiro/Reportes/" + "reporte de " + Search_by_name + ".csv";
        //El archivo existe? lo BORRAMOS
        if (File.Exists(ruta))
        {
            File.Delete(ruta);
        }
        TextWriter tw = new StreamWriter(ruta, false);
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
        /*var sr = File.CreateText(ruta);
        string datosCSV = "";
        //datosCSV += puntuaciones[0].userId.ToString() + ",\n";
        //datosCSV += "\n," + "\n," + "\n" + "\n," + "\n";*/

        tw = new StreamWriter(ruta, true);
        //datosCSV += "usuario," + "kills," + "precision," + "tiempo," + "fecha," + "\n";
        foreach (var i in datospuntuacion)
        {
            //datosCSV += i.name.ToString() + "," +int.Parse(i.kills.ToString()) + "," + i.precision.ToString() + "," + i.tiempo.ToString() + "," +i.fecha.ToString() + ",\n";
            tw.WriteLine(i.name.ToString() + ";" + int.Parse(i.kills.ToString()) + ";" + int.Parse(i.municion.ToString()) + ";" + i.precision.ToString() + ";" + i.tiempo.ToString() + ";" + i.demora.ToString() + ";" + i.fecha.ToString() + ";" + i.hora.ToString() + ";" + i.distancia.ToString());

        }
        //Crear el archivo
        /*sr.WriteLine(datosCSV);
        //Dejar como sólo de lectura
        FileInfo fInfo = new FileInfo(ruta);
        fInfo.IsReadOnly = true;*/
        //Cerrar
        tw.Close();
        //Abrimos archivo recién creado
        Application.OpenURL(ruta);
    }
    public void atras()
    {
        SceneManager.LoadScene("MenuEntrenador");
    }
}
